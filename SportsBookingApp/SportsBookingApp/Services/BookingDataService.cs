using Firebase.Database;
using SportsBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBookingApp.Services
{
    public class BookingDataService
    {

        FirebaseClient client;
        public BookingDataService()
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
        }

        /*
        public async Task<bool> IsCenterExists(string email)
        {
            var center = (await client.Child("Centers").OnceAsync<Center>()).Where(u => u.Object.CenterEmail == email).FirstOrDefault();

            return (center != null);
        }
        */

        public async Task<List<Booking>> GetBookingsItemsAsync()
        {
        var bookings = (await client.Child("Bookings").
            OnceAsync<Booking>()).
            Select(f => new Booking
            {
                SportName = f.Object.SportName,
                CourtName = f.Object.CourtName,
                Username = f.Object.Username,
                StartingBookingTime = f.Object.StartingBookingTime,
                EndingBookingTime = f.Object.EndingBookingTime,
                TotalPaymentAmount = f.Object.TotalPaymentAmount,
                CenterName = f.Object.CenterName,
                BookingDate = f.Object.BookingDate,
                BookingStripeID = f.Object.BookingStripeID
            }).ToList();

        return bookings;
        }

        public async Task<ObservableCollection<Booking>> GetBookedSlotsItemsByCenterAndCourtAndDateAsync(string centerName, string courtName, DateTime bookingDate)
        {

            var BookingItemsByCenterandCourtAndDate = new ObservableCollection<Booking>();
            var items = (await GetBookingsItemsAsync()).Where(p => p.BookingDate.Date == bookingDate.Date).Where(p => p.CenterName == centerName).Where(p => p.CourtName == courtName).ToList();

            foreach (var item in items)
            {
                BookingItemsByCenterandCourtAndDate.Add(item);
            }

            return BookingItemsByCenterandCourtAndDate;
        }

        public async Task<bool> CheckForConflictBookingAsync(string centerName, DateTime bookingDate, string courtName, TimeSpan startingTime, TimeSpan endingTime)
        {   
            var allbookingsof = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date == bookingDate.Date).Where(p => p.CourtName == courtName).ToList(); // Why we write this line!!?
            
            var ConfilctBooking = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date == bookingDate.Date).Where(p => p.CourtName == courtName)
                .Where(p => ((p.StartingBookingTime.TimeOfDay.TotalSeconds < startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds)) ||
                ((p.EndingBookingTime.TimeOfDay.TotalSeconds > endingTime.TotalSeconds) && (p.StartingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds)) ||
                ((p.StartingBookingTime.TimeOfDay.TotalSeconds < startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds > endingTime.TotalSeconds) && ((endingTime.TotalMinutes >= 120 && p.EndingBookingTime.TimeOfDay.TotalMinutes >= 120) || (endingTime.TotalMinutes >= 0 && endingTime.TotalMinutes <= 120 && p.EndingBookingTime.TimeOfDay.TotalMinutes >= 0 && p.EndingBookingTime.TimeOfDay.TotalMinutes <= 120))) ||
                ((p.StartingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds) && ((endingTime.TotalMinutes >= 120 && p.EndingBookingTime.TimeOfDay.TotalMinutes >= 120) || (endingTime.TotalMinutes >= 0 && endingTime.TotalMinutes <= 120 && p.EndingBookingTime.TimeOfDay.TotalMinutes >= 0 && p.EndingBookingTime.TimeOfDay.TotalMinutes <= 120)))).ToList();
            
            /*
            var ConfilctBooking = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date == bookingDate.Date).Where(p => p.CourtName == courtName)
                .Where(p => ((p.StartingBookingTime.TimeOfDay.TotalSeconds < startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds)) ||
                ((p.EndingBookingTime.TimeOfDay.TotalSeconds > endingTime.TotalSeconds) && (p.StartingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds)) ||
                ((p.StartingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds))).ToList();
            */

            // || ((p.StartingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds))

            if (ConfilctBooking.Count >= 1) return false;
            else return true;

        }

        public async Task<ObservableCollection<Booking>> GetBookingsByUserNameAndDateAsync(string userName, DateTime bookingsDate)
        {

            var BookingsByUserNameAndDate = new ObservableCollection<Booking>();
            var items = (await GetBookingsItemsAsync()).Where(p => p.Username == userName).Where(p => p.BookingDate.Date == bookingsDate.Date).ToList();

            foreach (var item in items)
            {
                BookingsByUserNameAndDate.Add(item);
            }

            return BookingsByUserNameAndDate;
        }

        public async Task<double> GetTotalRevenuesBetweenDatesAsync(string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {

            double TotalRevenuesBetweenDates = 0;
            var items = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date >= startingBookingDate.Date)
                .Where(p => p.BookingDate.Date <= endingBookingDate.Date).ToList();

            foreach (var item in items)
            {
                TotalRevenuesBetweenDates += item.TotalPaymentAmount;
            }

            return TotalRevenuesBetweenDates;
        }

        public async Task<int> GetTotalNoOfBookingsBetweenDatesAsync(string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {

            int TotalNoOfBookingsBetweenDates = 0;
            var items = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date >= startingBookingDate.Date)
                .Where(p => p.BookingDate.Date <= endingBookingDate.Date).ToList();

            foreach (var item in items)
            {
                TotalNoOfBookingsBetweenDates += 1;
            }

            return TotalNoOfBookingsBetweenDates;
        }

        public async Task<string> GetPreferredBookedSlotsTimesForACenterBetweenTwoDatesAsync(string centerName, string sportName, DateTime startingBookingDate, DateTime endingBookingDate)
        {

            int Mornings, AfterNoons, Evenings, NumberOfBookings ;
            Mornings = AfterNoons = Evenings = NumberOfBookings = 0;

            var items = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.SportName == sportName).Where(p => p.BookingDate.Date >= startingBookingDate.Date)
                .Where(p => p.BookingDate.Date <= endingBookingDate.Date).ToList();

            foreach (var item in items)
            {
                NumberOfBookings++;

                if (item.StartingBookingTime.TimeOfDay.TotalHours >= 8 && item.EndingBookingTime.TimeOfDay.TotalHours <= 12) Mornings++;
                else if (item.StartingBookingTime.TimeOfDay.TotalHours > 12 && item.EndingBookingTime.TimeOfDay.TotalHours <= 18) AfterNoons++;
                else if ( (item.StartingBookingTime.TimeOfDay.TotalHours > 18 && item.EndingBookingTime.TimeOfDay.TotalHours <= 23) || (item.StartingBookingTime.TimeOfDay.TotalHours >= 0 && item.EndingBookingTime.TimeOfDay.TotalHours <= 2)) Evenings++;
            }

            if (NumberOfBookings > 0)
            {
                if (Mornings > AfterNoons)
                {
                    if (Mornings > Evenings)
                    {
                        return "Mornings";
                    }
                    else
                    {
                        return "Evenings";
                    }
                }
                else if (AfterNoons > Evenings)
                    return "AfterNoons";
                else
                    return "Evenings";

            }
            else return "No Bookings";

        }

        public async Task<string> FindNameOfCourtHavingSpaceAtThisTimeAsync(string centerName, DateTime bookingDate, TimeSpan startingTime, TimeSpan endingTime, string sportName)
        {
            /*
            var allbookingsof = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date == bookingDate.Date)
                .Where(p => p.StartingBookingTime.TimeOfDay.TotalMinutes == startingTime.TotalMinutes).Where(p => p.EndingBookingTime.TimeOfDay.TotalMinutes == endingTime.TotalMinutes).ToList();
            */

            var ConfilctBooking = (await GetBookingsItemsAsync()).Where(p => p.CenterName == centerName).Where(p => p.BookingDate.Date == bookingDate.Date)
                .Where(p => ((p.StartingBookingTime.TimeOfDay.TotalSeconds < startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds)) ||
                ((p.EndingBookingTime.TimeOfDay.TotalSeconds > endingTime.TotalSeconds) && (p.StartingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds)) ||
                ((p.StartingBookingTime.TimeOfDay.TotalSeconds > startingTime.TotalSeconds) && (p.EndingBookingTime.TimeOfDay.TotalSeconds < endingTime.TotalSeconds))).ToList();


            var AllCourtsNames = await new CourtDataService().GetCourtsNamesBySportAndCenterAsync(centerName, sportName);
            
            int n = 1;

            foreach (string courtname in AllCourtsNames)
            {
                foreach (var booking in ConfilctBooking)
                {
                    if (courtname == booking.CourtName) n = 0;
                }

                if (n == 1) return courtname;

                n = 1;
            }

            return "Conflicting";
                
        }


    }

}
