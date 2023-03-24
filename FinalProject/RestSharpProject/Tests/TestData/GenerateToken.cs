using RestSharpProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests.TestData
{
    public class GenerateToken
    {
        public static UserLoginModels newToken()
        {
            return new UserLoginModels
            {
                Username = "admin",
                Password = "password123"

            };
        }
    }
}
