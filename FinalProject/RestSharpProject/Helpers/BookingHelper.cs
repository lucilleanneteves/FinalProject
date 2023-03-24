using RestSharpProject.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProject.Resources;
using RestSharp;
using RestSharpProject.Tests.TestData;
namespace RestSharpProject.Helpers
{
    public class BookingHelper
    {


        public async Task<RestResponse> CreateBooking(RestClient client)
        {
            var bookingData = GenerateBooking.BookingDetails();
            var postRequest = new RestRequest(Endpoints.CreateBooking())
                .AddJsonBody(bookingData)
                .AddHeader("Accept", "application/json");

            var response = await client.ExecutePostAsync(postRequest);

            return response;


        }

        public async Task<BookingModel> GetBookingById(RestClient client, long id)
        {

            var request = new RestRequest(Endpoints.GetBookingById(id))
                .AddHeader("Accept", "application/json");
            var response = await client.ExecuteGetAsync<BookingModel>(request);



            return response.Data;


        }

        public async Task<RestResponse> UpdateBookingById(RestClient client, long id, BookingModel updatedBooking, string token)
        {

            var request = new RestRequest(Endpoints.UpdateBookingById(id))
                .AddJsonBody(updatedBooking)
                .AddHeader("Accept", "application/json")
                .AddHeader("Cookie", $"token={token}");
            var response = await client.ExecutePutAsync<BookingModel>(request);

            return response;


        }

        public async Task<RestResponse> DeleteBooking(RestClient client, long id, string token)
        {

            var postRequest = new RestRequest(Endpoints.DeleteBookingById(id))
                .AddHeader("Accept", "application/json")
                .AddHeader("Cookie", $"token={token}");

            var response = await client.DeleteAsync(postRequest);

            return response;


        }

        public async Task<string> GetToken(RestClient client)

        {

            var loginData = GenerateToken.newToken();

            var request = new RestRequest(Endpoints.Authenticate())
                .AddJsonBody(loginData)
                .AddHeader("Accept", "application/json");

            var response = await client.ExecutePostAsync(request);
            var content = JsonConvert.DeserializeObject<TokenModel>(response.Content);

            return content.Token;



        }

    }
}




