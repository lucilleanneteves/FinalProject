using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.DataModels
{
    public class BookingModel
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("totalprice")]
        public long TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public bool DepositPaid { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDates BookingDates { get; set; }

        [JsonProperty("additionalneeds")]
        public string AdditionalNeeds { get; set; }
    }

    public partial class BookingDates
    {
        [JsonProperty("checkin")]
        public DateTime CheckIn { get; set; }

        [JsonProperty("checkout")]
        public DateTime CheckOut { get; set; }
    }

    public partial class BookingDetails
    {
        [JsonProperty("bookingid")]
        public long BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingModel Booking { get; set; }

    }

    public partial class Token
    {
        [JsonProperty("token")]
        public string TokenAuth { get; set; }
    }

    public class User
    {
        public User()
        {
            Username = "admin";
            Password = "password123";
        }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}