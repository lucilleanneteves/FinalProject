using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HttpClientProject.DataModels;

namespace HttpClientProject.Tests
{
    public class ApiBaseTest
    {
        public HttpClient httpClient { get; set; }

        public BookingModel BookingDetails { get; set; }

        [TestInitialize]
        public void Initilize()

        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }
    }
}