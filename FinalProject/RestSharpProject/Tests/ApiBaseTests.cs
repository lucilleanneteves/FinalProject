using RestSharpProject.DataModels;
using RestSharp;
using RestSharpProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests
{
    public class ApiBaseTests
    {
        public RestClient restClient { get; set; }
        public BookingModel bookingModel { get; set; }

        public BookingHelper bookingHelper { get; set; }

        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            restClient = new RestClient();
            bookingHelper = new BookingHelper();
            token = await bookingHelper.GetToken(restClient);
        }

    }
}
