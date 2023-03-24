using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharpProject.DataModels;
using RestSharpProject.Tests.TestData;
using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Resources;
using RestSharpProject.Tests;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests
{
    [TestClass]
    public class RestSharpTests:ApiBaseTests
    {
        private readonly List<BookingDetails> cleanUpList = new List<BookingDetails>();

        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.DeleteBooking(restClient, data.BookingId, token);

            }

        }

        [TestMethod]
        public async Task Validate_CreateBooking()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingDetails>(response.Content);

            cleanUpList.Add(newBooking);

            //Act
            var getBooking = await bookingHelper.GetBookingById(restClient, newBooking.BookingId);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(newBooking.Booking.FirstName, getBooking.FirstName);
            Assert.AreEqual(newBooking.Booking.LastName, getBooking.LastName);
            Assert.AreEqual(newBooking.Booking.TotalPrice, getBooking.TotalPrice);
            Assert.AreEqual(newBooking.Booking.DepositPaid, getBooking.DepositPaid);
            Assert.AreEqual(newBooking.Booking.BookingDates.CheckOut, getBooking.BookingDates.CheckOut);
            Assert.AreEqual(newBooking.Booking.BookingDates.CheckIn, getBooking.BookingDates.CheckIn);


        }

        [TestMethod]
        public async Task Validate_UpdateBooking()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingDetails>(response.Content);
            cleanUpList.Add(newBooking);

            var updateBooking = new BookingModel()
            {
                FirstName = "Michael",
                LastName = "Jordan",
                TotalPrice = 5099,
                DepositPaid = true,
                BookingDates = new BookingDates { CheckIn = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")), CheckOut = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) },
                AdditionalNeeds = "Breakfast"

            };


            //Act
            var updateResponse = await bookingHelper.UpdateBookingById(restClient, newBooking.BookingId, updateBooking, token);
            var updateData = JsonConvert.DeserializeObject<BookingModel>(updateResponse.Content);


            var getBooking = await bookingHelper.GetBookingById(restClient, newBooking.BookingId);

            //Assert
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(updateData.FirstName, getBooking.FirstName);
            Assert.AreEqual(updateData.LastName, getBooking.LastName);
            Assert.AreEqual(updateData.TotalPrice, getBooking.TotalPrice);
            Assert.AreEqual(updateData.DepositPaid, getBooking.DepositPaid);
            Assert.AreEqual(updateData.BookingDates.CheckOut, getBooking.BookingDates.CheckOut);
            Assert.AreEqual(updateData.BookingDates.CheckIn, getBooking.BookingDates.CheckIn);
        }


        [TestMethod]
        public async Task Validate_DeleteBooking()
        {

            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<BookingDetails>(response.Content);

            //Act
            var deleteData = await bookingHelper.DeleteBooking(restClient, newBooking.BookingId, token);

            //Assert
            Assert.AreEqual(deleteData.StatusCode, HttpStatusCode.Created);





        }

        [TestMethod]
        public async Task Validate_GetBooking()
        {
            //Arrange
            var request = new RestRequest(Endpoints.GetBookingById(-31233))
               .AddHeader("Accept", "application/json");

            //Act
            var response = await restClient.ExecuteGetAsync<BookingModel>(request);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

    }
}