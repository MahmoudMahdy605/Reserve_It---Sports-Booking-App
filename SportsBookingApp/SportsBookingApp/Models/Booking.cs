using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBookingApp.Models
{
    public class Booking
    {
        public string SportName { get; set; }
        public string CourtName { get; set; }
        public string Username { get; set; }
        public string CenterName { get; set; }
        public DateTime StartingBookingTime { get; set; }
        public DateTime EndingBookingTime { get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalPaymentAmount { get; set; }
        public string BookingStripeID { get; set; }

    }

}
