using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using HttpClientProject.DataModels;
using HttpClientProject.Tests;
using HttpClientProject.Helpers;
using HttpClientProject.Tests.TestData;
using HttpClientProject.Resources;
using System.Net.Http.Headers;

//Parallel Run
[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HttpClientProject.Tests
{

    [TestClass]
    public class BookingTests
    {
        public HttpClient httpClient;
        List<BookingDetails> orderCleanupList = new List<BookingDetails>();

        [TestInitialize]
        public async Task Initialize()
        {
            httpClient = new HttpClient();

            Token token = await UserHelper.AuthenticateUser(httpClient);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cookie", $"token={token.TokenAuth}");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (var data in orderCleanupList)
            {
                await httpClient.DeleteAsync(BookingEndpoint.DeleteBookingById(data.BookingId));
            }
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            //Prepare data
            BookingModel bookingmodel = GenerateBooking.BookingDetails();

            //Post Booking
            var addBooking = await BookingHelpers.PostBooking(httpClient, bookingmodel);
            var deserializedResponse = JsonConvert.DeserializeObject<BookingDetails>(addBooking.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedResponse);

            //Get Booking by ID
            var getBookingID = await BookingHelpers.GetRequestBookingById(httpClient, deserializedResponse.BookingId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.OK, addBooking.StatusCode, "Status Code not matched");
            Assert.AreEqual(bookingmodel.FirstName, getBookingID.FirstName, "Firstname not matched");
            Assert.AreEqual(bookingmodel.LastName, getBookingID.LastName, "Lastname not matched");
            Assert.AreEqual(bookingmodel.TotalPrice, getBookingID.TotalPrice, "TotalPrice not matched");
            Assert.AreEqual(bookingmodel.DepositPaid, getBookingID.DepositPaid, "DepositPaid not matched");
            Assert.AreEqual(bookingmodel.BookingDates.CheckIn, getBookingID.BookingDates.CheckIn, "Checkin date not matched");
            Assert.AreEqual(bookingmodel.BookingDates.CheckOut, getBookingID.BookingDates.CheckOut, "CheckOut date not matched");
            Assert.AreEqual(bookingmodel.AdditionalNeeds, getBookingID.AdditionalNeeds, "AdditionalNeeds not matched");
        }


        [TestMethod]
        public async Task UpdateBooking()
        {

            BookingModel booking = GenerateBooking.BookingDetails();

            var addBookingRequest = await BookingHelpers.PostBooking(httpClient, booking);
            var deserializedResponse = JsonConvert.DeserializeObject<BookingDetails>(addBookingRequest.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedResponse);

            //Update Firstname and Lastname
            deserializedResponse.Booking.FirstName = "Lucille Anne";
            deserializedResponse.Booking.LastName = "Teves2";

            //Update booking
            var updateBooking = await BookingHelpers.PutBookingById(httpClient, deserializedResponse);

            //Get updated booking
            var getUpdatedBooking = await BookingHelpers.GetRequestBookingById(httpClient, deserializedResponse.BookingId);

            //Assertions for the updated fields
            Assert.AreEqual(HttpStatusCode.OK, updateBooking.StatusCode, "Status Code not matched");
            Assert.AreEqual(deserializedResponse.Booking.FirstName, getUpdatedBooking.FirstName, "Firstname not matched");
            Assert.AreEqual(deserializedResponse.Booking.LastName, getUpdatedBooking.LastName, "Lastname not matched");
        }


        [TestMethod]
        public async Task DeleteBookingById()
        {
            //Prepare Data
            BookingModel booking = GenerateBooking.BookingDetails();

            //Post Booking
            var postBooking = await BookingHelpers.PostBooking(httpClient, booking);
            var deserializedPostResponse = JsonConvert.DeserializeObject<BookingDetails>(postBooking.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedPostResponse);

            //Delete Booking
            var deleteResponse = await BookingHelpers.DeleteBookingById(httpClient, deserializedPostResponse.BookingId);

            //Assertion HTTP Code
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status Code not matched");
        }

        [TestMethod]
        public async Task GetInvalidBookingId()
        {
            //Prepare Data
            long invalidId = 781473;

            //Get Booking invalid id
            var getResponse = await BookingHelpers.GetBookingByInvalidId(httpClient, invalidId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "GET method HttpStatus Code mismatched for invalid id");
        }
    }
}