using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProject.DataModels;

namespace RestSharpProject.Tests.TestData
{
    public class AuthTokenDetails
    {
        public static TokenDetailsModel credentials()
        {
            return new TokenDetailsModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
