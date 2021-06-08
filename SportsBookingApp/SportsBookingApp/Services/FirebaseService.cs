using Firebase.Database;
using Firebase.Database.Query;
using SportsBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBookingApp.Services
{
    public class FirebaseService
    {
        FirebaseClient client;
        public FirebaseService()
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
        }

        public async Task CancelBooking(DateTime bookingDate, string centerName, string username, DateTime startingBookingTime, DateTime endingBookingTime)
        {
            
            var toCancelBooking = (await client
              .Child("Booking")
              .OnceAsync<Booking>()).Where(a => a.Object.BookingDate == bookingDate).Where(a => a.Object.CenterName == centerName)
              .Where(a => a.Object.StartingBookingTime == startingBookingTime).Where(a => a.Object.EndingBookingTime == endingBookingTime)
              .Where(a => a.Object.Username == username).FirstOrDefault();
            

            await client.Child("Persons").Child(toCancelBooking.Key).DeleteAsync();

        }
    }
}
