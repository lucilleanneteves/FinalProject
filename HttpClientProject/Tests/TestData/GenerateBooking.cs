using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpClientProject.DataModels;

namespace HttpClientProject.Tests.TestData
{
    public class GenerateBooking
    {
        public static BookingModel BookingDetails()
        {
            return new BookingModel
            {
                FirstName = "Lucille",
                LastName = "Teves",
                TotalPrice = 2000,
                DepositPaid = true,
                BookingDates = new BookingDates { CheckIn = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")), CheckOut = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")) },
                AdditionalNeeds = "Chair"
            };
        }

        public static BookingDetails NewBookingDetails()
        {
            return new BookingDetails
            {

            };
        }
    }
}
