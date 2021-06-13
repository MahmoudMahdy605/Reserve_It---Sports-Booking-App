using SportsBookingApp.Models;
using SportsBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace SportsBookingApp.ViewModels
{
    public class CenterDataViewModel : BaseViewModel
    {
        public CenterDataViewModel()
        {

            var cname = Preferences.Get("CenterName", String.Empty);
            if (String.IsNullOrEmpty(cname))
                CenterName = "Guest";
            else CenterName = cname;


            PreferredbookingTimes = new ObservableCollection<PreferredBTimes>();

            //GetPreferredbookingTimes(centerName, startingBookingDate, endingBookingDate);

        }


        public CenterDataViewModel( string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {

            CenterName = centerName;

            GetTotalRevenues(centerName, startingBookingDate, endingBookingDate);
            GetTotalNoOfBookings(centerName, startingBookingDate, endingBookingDate);

            PreferredbookingTimes = new ObservableCollection<PreferredBTimes>();

            GetPreferredbookingTimes(centerName, startingBookingDate, endingBookingDate);

        }

        public ObservableCollection<PreferredBTimes> PreferredbookingTimes { get; set; }

        private string _CenterName;
        public string CenterName
        {
            get { return _CenterName; }
            set
            {
                _CenterName = value;
                OnpropertyChanged();

            }
        }

        private double _TotalRevenue;
        public double TotalRevenue
        {
            get { return _TotalRevenue; }
            set
            {
                _TotalRevenue = value;
                OnpropertyChanged();

            }
        }

        private int _TotalNoOfBookings;
        public int TotalNoOfBookings
        {
            get { return _TotalNoOfBookings; }
            set
            {
                _TotalNoOfBookings = value;
                OnpropertyChanged();

            }
        }

        private async void GetTotalRevenues(string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {
            double data = await new BookingDataService().GetTotalRevenuesBetweenDatesAsync(centerName, startingBookingDate, endingBookingDate);

            TotalRevenue = data;

        }

        private async void GetTotalNoOfBookings(string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {
            int data = await new BookingDataService().GetTotalNoOfBookingsBetweenDatesAsync(centerName, startingBookingDate, endingBookingDate);

            TotalNoOfBookings = data;

        }

        private async void GetPreferredbookingTimes(string centerName, DateTime startingBookingDate, DateTime endingBookingDate)
        {
            var SportsData = await new SportDataService().GetSportsAsync();

            PreferredbookingTimes.Clear();

            foreach (var item in SportsData)
            {
                string Bookingstime = await new BookingDataService().GetPreferredBookedSlotsTimesForACenterBetweenTwoDatesAsync(centerName, item.SportName, startingBookingDate, endingBookingDate);

                if ( Bookingstime != "No Bookings") PreferredbookingTimes.Add(new PreferredBTimes { SportName = item.SportName, PreferredTime = Bookingstime });


            }
            int test = 3;

        }

    }

    public class PreferredBTimes
    {
        public string SportName { get; set; }
        public string PreferredTime { get; set; }

    }

}
