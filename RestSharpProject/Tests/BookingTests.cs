using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Resource;
using RestSharpProject.Helpers;
using System.Net;
using RestSharpProject.Tests.TestData;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharpProject.Tests
{
    [TestClass]
    public class BookingTests : APIBaseTest
    {
        private readonly List<BookingDetails> bookingCleanupList = new List<BookingDetails>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            var restRes = await Helper.CreateBooking(RestClient);
            BookingDetails = restRes.Data;

            Assert.AreEqual(restRes.StatusCode, HttpStatusCode.OK);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in bookingCleanupList)
            {
                var deleteBookingResponse = await Helper.DeleteBooking(RestClient, data.BookingId);
            }
        }
        [TestMethod]
        public async Task CreateBooking()
        {
      
            var getBooking = await Helper.GetBook(RestClient, BookingDetails.BookingId);

            bookingCleanupList.Add(BookingDetails);

            var postBooking = GenerateBookingDetails.bookingDetails();

            Assert.AreEqual(postBooking.Firstname, getBooking.Data.Firstname, "First name does not match");
            Assert.AreEqual(postBooking.Lastname, getBooking.Data.Lastname, "Last name does not match");
            Assert.AreEqual(postBooking.Totalprice, getBooking.Data.Totalprice, "Total price does not match");
            Assert.AreEqual(postBooking.Depositpaid, getBooking.Data.Depositpaid, "Deposit paid does not match");
            Assert.AreEqual(postBooking.Bookingdates.Checkin.Date, getBooking.Data.Bookingdates.Checkin.Date, "Check in does not match");
            Assert.AreEqual(postBooking.Bookingdates.Checkout.Date, getBooking.Data.Bookingdates.Checkout.Date, "Check out does not match");
            Assert.AreEqual(postBooking.Additionalneeds, getBooking.Data.Additionalneeds, "Additional needs does not match");
        }


        [TestMethod]
        public async Task UpdateBooking()
        {
       
            var getBooking = await Helper.GetBook(RestClient, BookingDetails.BookingId);

   
            bookingCleanupList.Add(BookingDetails);

      
            var updateBookingInfo = new BookingDetailsModel()
            {
                Firstname = "LucilleUpdated",
                Lastname = "TevesUpdated",
                Totalprice = getBooking.Data.Totalprice,
                Depositpaid = getBooking.Data.Depositpaid,
                Bookingdates = getBooking.Data.Bookingdates,
                Additionalneeds = getBooking.Data.Additionalneeds
            };
            var updateBooking = await Helper.UpdateBooking(RestClient, updateBookingInfo, BookingDetails.BookingId);

            Assert.AreEqual(HttpStatusCode.OK, updateBooking.StatusCode);

            var getUpdatedBooking = await Helper.GetBook(RestClient, BookingDetails.BookingId);

       
            Assert.AreEqual(updateBookingInfo.Firstname, getUpdatedBooking.Data.Firstname, "First name does not match");
            Assert.AreEqual(updateBookingInfo.Lastname, getUpdatedBooking.Data.Lastname, "Last name does not match");
            Assert.AreEqual(updateBookingInfo.Totalprice, getUpdatedBooking.Data.Totalprice, "Total price does not match");
            Assert.AreEqual(updateBookingInfo.Depositpaid, getUpdatedBooking.Data.Depositpaid, "Deposit paid does not match");
            Assert.AreEqual(updateBookingInfo.Bookingdates.Checkin.Date, getUpdatedBooking.Data.Bookingdates.Checkin.Date, "Check in does not match");
            Assert.AreEqual(updateBookingInfo.Bookingdates.Checkout.Date, getUpdatedBooking.Data.Bookingdates.Checkout.Date, "Check out does not match");
            Assert.AreEqual(updateBookingInfo.Additionalneeds, getUpdatedBooking.Data.Additionalneeds, "Additional needs does not match");
        }

        [TestMethod]
        public async Task DeleteBooking()
        {
    
            var deleteBooking = await Helper.DeleteBooking(RestClient, BookingDetails.BookingId);

      
            Assert.AreEqual(HttpStatusCode.Created, deleteBooking.StatusCode);
        }

        [TestMethod]
        public async Task InvalidBooking()
        {
            var getCreatedBooking = await Helper.GetBook(RestClient, 2343235);

            bookingCleanupList.Add(BookingDetails);

            Assert.AreEqual(HttpStatusCode.NotFound, getCreatedBooking.StatusCode);

        }
    }
}
