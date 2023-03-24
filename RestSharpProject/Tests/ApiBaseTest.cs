using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharpProject.DataModels;

namespace RestSharpProject.Tests
{
    public class APIBaseTest
    {
        public RestClient RestClient { get; set; }
        public BookingDetails BookingDetails { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            RestClient = new RestClient();
        }
    }
}
