using Firebase.Database;
using Firebase.Database.Query;
using SportsBookingApp.Models;
using SportsBookingApp.Services;
using SportsBookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Collections.Generic;
using Stripe;

namespace SportsBookingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBookingHistoryView : ContentPage
    {
        MyBookingHistoryViewModel cvm;
        string UserName = Preferences.Get("UserName", String.Empty);

        FirebaseClient client;

        string getchargedID;    
        string refundID;

        public MyBookingHistoryView()
        {
            InitializeComponent();

            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            cvm = new MyBookingHistoryViewModel();
            this.BindingContext = cvm;


            StripeConfiguration.SetApiKey("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");

        }

        private async void SelectedBookingDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            cvm = new MyBookingHistoryViewModel(UserName, SelectedBookingDate.Date);

            this.BindingContext = cvm;

        }

        
        async void CancelBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool response = await DisplayAlert("Cancel booking", "Do you want to cancel this booking", "Yes", "No");

            if (response == true)
            {
                //user click ok  

                var booking = e.CurrentSelection.FirstOrDefault() as Booking;
                if (booking == null)
                    return;

                var toCancelBooking = (await client
                  .Child("Bookings")
                  .OnceAsync<Booking>()).Where(a => a.Object.BookingDate == booking.BookingDate).Where(a => a.Object.CenterName == booking.CenterName)
                  .Where(a => a.Object.StartingBookingTime == booking.StartingBookingTime).Where(a => a.Object.EndingBookingTime == booking.EndingBookingTime)
                  .Where(a => a.Object.Username == booking.Username).FirstOrDefault();


                await client.Child("Bookings").Child(toCancelBooking.Key).DeleteAsync();

                cvm = new MyBookingHistoryViewModel(UserName, SelectedBookingDate.Date);

                this.BindingContext = cvm;

                var refundService = new RefundService();
                var refundOptions = new RefundCreateOptions
                {
                    Charge = booking.BookingStripeID,
                };
                Refund refund = refundService.Create(refundOptions);
                refundID = refund.Id;

                await DisplayAlert("Success", "Booking Cancelled Successfully", "OK");

            }
            else
            {
                //user click cancel 

                //((CollectionView)sender).SelectedItem = null;
            }


        }
        
    }
}