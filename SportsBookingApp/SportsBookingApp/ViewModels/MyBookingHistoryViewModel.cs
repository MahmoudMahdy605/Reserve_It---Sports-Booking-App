using Firebase.Database;
using Firebase.Database.Query;
using SportsBookingApp.Models;
using SportsBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SportsBookingApp.ViewModels
{
    public class MyBookingHistoryViewModel : BaseViewModel
    {
        public MyBookingHistoryViewModel()
        {
            UserName = Preferences.Get("UserName", String.Empty);

            MyBookings = new ObservableCollection<Booking>();

            //CancelBookingCommand = new Command(async () => await CancelBookingCommandAsync());
        }

        public MyBookingHistoryViewModel(string username, DateTime bookingsdate)
        {
            UserName = username;

            MyBookings = new ObservableCollection<Booking>();

            GetBookingsByUserNameAndDate(username, bookingsdate);
        }

        private string _UserName;
        private FirebaseClient client;

        public string UserName
        {
            set
            {
                _UserName = value;
                OnpropertyChanged();
            }
            get
            {
                return _UserName;
            }
        }

        public ObservableCollection<Booking> MyBookings { get; set; }

        private async void GetBookingsByUserNameAndDate(string userName, DateTime bookingsDate)
        {
            var data = await new BookingDataService().GetBookingsByUserNameAndDateAsync(userName, bookingsDate);
            MyBookings.Clear();
            foreach (var item in data)
            {
                MyBookings.Add(item);
            }

        }

        async void CancelBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var booking = e.CurrentSelection.FirstOrDefault() as Booking;
            if (booking == null)
                return;

            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            var toCancelBooking = (await client
              .Child("Booking")
              .OnceAsync<Booking>()).Where(a => a.Object.BookingDate == booking.BookingDate).Where(a => a.Object.CenterName == booking.CenterName)
              .Where(a => a.Object.StartingBookingTime == booking.StartingBookingTime).Where(a => a.Object.EndingBookingTime == booking.EndingBookingTime)
              .Where(a => a.Object.Username == booking.Username).FirstOrDefault();


            await client.Child("Persons").Child(toCancelBooking.Key).DeleteAsync();

            //await FirebaseService.CancelBooking(booking.BookingDate, booking.CenterName, booking.Username, booking.StartingBookingTime, booking.EndingBookingTime);

            //await DisplayAlert("Success", "Booking Cancelled Successfully", "OK");

            /*var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;*/


        }

        /*
        public Command CancelBookingCommand { get; set; }

        private async Task CancelBookingCommandAsync(DateTime bookingDate, string centerName, string username, DateTime startingBookingTime, DateTime endingBookingTime)
        {
            await FirebaseService.CancelBooking(bookingDate, centerName, username, startingBookingTime, endingBookingTime);

            await DisplayAlert("Success", "Booking Cancelled Successfully", "OK");

            /*var allPersons = await firebaseHelper.GetAllPersons();
            lstPersons.ItemsSource = allPersons;
        }
    */
    }
}
