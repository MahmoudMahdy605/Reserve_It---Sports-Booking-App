using SportsBookingApp.Models;
using SportsBookingApp.Services;
using SportsBookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsBookingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingView : ContentPage
    {
        //public ObservableCollection<Booked> CollectDetails { get; set; }


        BookingViewModel asm;

        Center c;
        string s;


        public BookingView(Center center, string sportname)
        {
            InitializeComponent();


            c = center;
            s = sportname;

            SelectedStartingBookingTime.Time = System.DateTime.Now.AddHours(8).TimeOfDay;
            //SelectedEndingBookingTime.Time = SelectedStartingBookingTime.Time;
            SelectedEndingBookingTime.Time = System.DateTime.Now.AddHours(9).TimeOfDay;

            asm = new BookingViewModel(center, sportname);
            this.BindingContext = asm;


            /*
            this.BindingContext = this;
            
            CollectDetails = new ObservableCollection<Booked>
            {
                new Booked {Shomoy="4:00 PM-5:00 PM"},
                new Booked {Shomoy="7:00 PM-8:00 PM"},
                new Booked {Shomoy="10:00 AM-11:30 AM"},
            };
            */

        }
        
        
        private async void SelectedBookingDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            
            asm = new BookingViewModel(c, s);
            
            this.BindingContext = asm;
            
        }
        

        /*
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;


            try
            {
                if (selectedIndex != -1)
                {
                    Application.Current.MainPage.DisplayAlert("ok", "index not negative", "OK");

                    asm = new BookingViewModel(c, s, (string)picker.ItemsSource[selectedIndex] , SelectedBookingDate.Date);
                    this.BindingContext = asm;

                    //SelectedCourt.SelectedItem = (string)picker.ItemsSource[selectedIndex];

                }

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
            }
        }
        */

        private void CheckAvailability_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (SelectedCourt.SelectedItem != null)
                {
                    asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString(), SelectedBookingDate.Date);

                    //SelectedCourt.SelectedItem = SelectedCourt.SelectedItem;
                    this.BindingContext = asm;
                }
                else Application.Current.MainPage.DisplayAlert("Missing information", "Please select your venue", "OK");

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            

        }

        //async Task EndingBookingTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)

        /*
        void EndingBookingTimePickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (SelectedCourt.SelectedItem is "Any")
            {
                _ = goo();
            }


        }
        */

        private async Task goo ()
        {
            string NameOfCourtHavingSpaceAtThisTime = await new BookingDataService().FindNameOfCourtHavingSpaceAtThisTimeAsync(c.CenterName, SelectedBookingDate.Date, SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time, s);

            if (NameOfCourtHavingSpaceAtThisTime != "Conflicting")
            {
                SelectedCourt.SelectedItem = NameOfCourtHavingSpaceAtThisTime;
            }
            else await Application.Current.MainPage.DisplayAlert("Booking time is not available ", " Please select another time", "OK");

        }



        private async void ButtonBook_Clicked(object sender, EventArgs e)
        {
            // var checkconflictbooking = VerifyAvailabilityOfBookingSlotAsync(c.CenterName, SelectedBookingDate.Date.DayOfWeek, SelectedCourt.SelectedItem.ToString(), SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time);


            // Do chack time quarters/halves.
            // Do chack time quarters/halves.
            // Do chack time quarters/halves.
            // Do chack time quarters/halves.
            // Do chack time quarters/halves.
            // Do chack time quarters/halves.

            if (SelectedCourt.SelectedItem is "Any")
            {
                _ = goo();
            }

            if (VerifyTimeSequence (SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time) == true )
            {
                if(VerifyWorkingTime(SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time) == true)
                {

                    try
                    {
                        bool Noconflictexists = await VerifyAvailabilityOfBookingSlotAsync(c.CenterName, SelectedBookingDate.Date, SelectedCourt.SelectedItem.ToString(), SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time);
                        
                        // SelectedCourt.SelectedItem is nullllllllllllllllll, why ;
                        if (Noconflictexists == true)
                        {

                            await Application.Current.MainPage.DisplayAlert("Booking time is available ", " Proceed with payment", "OK"); // successful booking time

                            double selectedTimeDuration;
                            double paymentCostScale = double.Parse(CourtPaymentCostScale.Text);
                            if ((int)SelectedEndingBookingTime.Time.TotalMinutes > (int)SelectedStartingBookingTime.Time.TotalMinutes)
                            {
                                selectedTimeDuration = ((int)SelectedEndingBookingTime.Time.TotalMinutes - (int)SelectedStartingBookingTime.Time.TotalMinutes);
                            }
                            else selectedTimeDuration = ((int)SelectedEndingBookingTime.Time.TotalMinutes + ( 1440 - (int)SelectedStartingBookingTime.Time.TotalMinutes) );

                            double TotalPaymentAmount = ( paymentCostScale * selectedTimeDuration ) / 60.0 ;


                            await Navigation.PushModalAsync(new PaymentView(SelectedBookingDate.Date, c.CenterName, s, SelectedCourt.SelectedItem.ToString(), SelectedStartingBookingTime.Time.ToString(), SelectedEndingBookingTime.Time.ToString(), TotalPaymentAmount));
                            
                        }
                        else await Application.Current.MainPage.DisplayAlert("Conflict booking", " booking time is conflicting", "Select another availabile time"); //  booking time already exists;

                    }
                    catch 
                    {
                        //await Application.Current.MainPage.DisplayAlert("Error for conflict booking", "e.Message", "OK");
                        await Application.Current.MainPage.DisplayAlert("Missing information", "Please select your venue", "OK");
                    }
                    
                   
                }
                else await Application.Current.MainPage.DisplayAlert("Sorry", " we're only open from 8AM to 2 AM", "OK");

                
                /*
                Control1Output.Text = string.Format("Thank you. Your appointment is set for {0}.",
                   arrivalTimePicker.Time.ToString());
                */
            }else
            {
                await Application.Current.MainPage.DisplayAlert("Error", " Ending booking time is same / preceding Starting booking time", "OK");
            }
        }

        private bool VerifyWorkingTime(TimeSpan startingtime, TimeSpan endingtime)
        {

            // Set open (8 AM) and close (2 AM) times. 
            TimeSpan openTime = new TimeSpan(8, 0, 0);
            TimeSpan closeTime = new TimeSpan(2, 0, 0);

            /*
            Application.Current.MainPage.DisplayAlert("endingtime ", endingtime.ToString(), "OK");
            Application.Current.MainPage.DisplayAlert("startingtime ", startingtime.ToString(), "OK");
            Application.Current.MainPage.DisplayAlert("openTime", openTime.ToString(), "OK");
            Application.Current.MainPage.DisplayAlert("closeTime", closeTime.ToString(), "OK");
            */
            

            if ((startingtime >= openTime || startingtime <= closeTime) && (endingtime >= openTime || endingtime <= closeTime))
            {
                return true; // Open 
            }
            return false; // Closed 

            // (int)endingtime.TotalHours <= 2 && (int)endingtime.TotalMinutes <= 120

        }
        private bool VerifyTimeSequence(TimeSpan startingtime, TimeSpan endingtime)
        {
            /*
            DateTime sta = Convert.ToDateTime(startingtime.ToString());
            DateTime end = Convert.ToDateTime(endingtime.ToString());
            */

            /*
            if (((startingtime.TotalHours < 12) && (endingtime.TotalHours < 12) && (startingtime < endingtime)) || ((startingtime.TotalHours > 12) && (startingtime.TotalHours <= 23) && (endingtime.TotalHours > 12) && (endingtime.TotalHours <= 23) && (startingtime < endingtime))
                || ((startingtime.TotalHours < 12) && (endingtime.TotalHours > 12)) || ((startingtime.TotalHours <= 23) && (endingtime.TotalHours >= 0)))
            */
            
            if ( (startingtime.TotalMinutes < endingtime.TotalMinutes) || ((int)startingtime.TotalHours <= 23  && (int)endingtime.TotalHours >= 0 ) && (int)endingtime.TotalHours <= 2)
            {
                return true; // startingtime is before endingtime 
            }
            return false; // startingtime is not before endingtime  
        }
        
        private async Task<bool> VerifyAvailabilityOfBookingSlotAsync( string centername, DateTime bookingdate, string courtname, TimeSpan startingtime, TimeSpan endingtime)
        {
            var ConfilctBookingExists = await new BookingDataService().CheckForConflictBookingAsync (centername, bookingdate, courtname, startingtime, endingtime);
            
            if (ConfilctBookingExists == true) return true;
            else return false;
        }

        /*
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;


        try
        {
            if (selectedIndex != -1)
            {
                Application.Current.MainPage.DisplayAlert("ok", "index not negative", "OK");

                asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString(), SelectedBookingDate.Date.DayOfWeek);
                this.BindingContext = asm;
            }

        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
        }

            
        Picker p = new Picker();
        p = (Picker)sender;

        try
        {
            if ((Picker)sender.SelectedIndex >= 0)
            {
                Application.Current.MainPage.DisplayAlert("ok", "index not negative", "OK");
                //asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString(), SelectedBookingDate.Date);

                asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString());
                this.BindingContext = asm;
            }
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("error", ex.Message, "OK");
        }



        //asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString(), SelectedBookingDate.Date);

        //asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString());
        //this.BindingContext = asm;

        var data = await new CourtDataService().GetCourtDataByNameAsync(SelectedCourt.SelectedItem.ToString());
        //var asdf = await new CourtDataService().GetCourtDataByNameAsync.(SelectedCourt.SelectedItem.ToString());
        // await Court asdf = new CourtDataService.(SelectedCourt.SelectedItem.ToString());

        CourtPaymentCostScale.Text = data. ;
        CourtPaymentTimeScale.Text = ;

        asm = new BookingViewModel(center, sportname, SelectedCourt.Title, SelectedBookingDate.Date);
            
        }
    */


    }

    /*
    public class Booked
    {
        public string Shomoy { get; set; }

    }
    */

}