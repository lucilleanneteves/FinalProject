using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientProject.DataModels;
using HttpClientProject.Resources;
using HttpClientProject.Tests;
using Newtonsoft.Json;

namespace HttpClientProject.Helpers
{
    public class BookingHelpers
    {
        public static async Task<HttpResponseMessage> PostBooking(HttpClient httpClient, BookingModel booking)
        {
            var serialized = JsonConvert.SerializeObject(booking);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(BookingEndpoint.PostBooking(), request);

            return response;
        }

        public static async Task<BookingModel> GetRequestBookingById(HttpClient httpClient, long id)
        {
            var getResponse = await httpClient.GetAsync(BookingEndpoint.GetBookingById(id));
            var deserializedResponse = JsonConvert.DeserializeObject<BookingModel>(getResponse.Content.ReadAsStringAsync().Result);

            return deserializedResponse;
        }

        public static async Task<HttpResponseMessage> GetBookingByInvalidId(HttpClient httpClient, long id)
        {
            var getResponse = await httpClient.GetAsync(BookingEndpoint.GetBookingById(id));

            return getResponse;
        }


        public static async Task<HttpResponseMessage> PutBookingById(HttpClient httpClient, BookingDetails orderDetails)
        {
            var serialized = JsonConvert.SerializeObject(orderDetails.Booking);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(BookingEndpoint.PutBookingById(orderDetails.BookingId), request);

            return response;
        }


        public static async Task<HttpResponseMessage> DeleteBookingById(HttpClient httpClient, long id)
        {
            var response = await httpClient.DeleteAsync(BookingEndpoint.DeleteBookingById(id));

            return response;
        }

    }


    public class UserHelper
    {
        public static async Task<Token> AuthenticateUser(HttpClient httpClient)
        {
            User user = new User();
            var serialized = JsonConvert.SerializeObject(user);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(BookingEndpoint.AuthenticateUser(), request);

            //set retrieved pet to petRetrived variable
            var deserialized = JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);

            Token token = new Token();
            token.TokenAuth = deserialized.TokenAuth;

            return token;
        }
    }
}
