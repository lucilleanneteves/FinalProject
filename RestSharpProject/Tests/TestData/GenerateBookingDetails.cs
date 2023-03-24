using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProject.DataModels;

namespace RestSharpProject.Tests.TestData
{
    public class GenerateBookingDetails
    {
        public static BookingDetailsModel bookingDetails()
        {
            DateTime dateTime = DateTime.UtcNow.AddHours(+8).ToUniversalTime();
            Bookingdates bookingDates = new Bookingdates();
            bookingDates.Checkin = dateTime;
            bookingDates.Checkout = dateTime.AddDays(1);

            return new BookingDetailsModel
            {

                Firstname = "Lucille",
                Lastname = "Teves",
                Totalprice = 111,
                Depositpaid = true,
                Bookingdates = bookingDates,
                Additionalneeds = "Parking"
            };
        }
    }
}
