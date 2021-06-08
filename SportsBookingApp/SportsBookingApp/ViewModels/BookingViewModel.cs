using SportsBookingApp.Models;
using SportsBookingApp.Services;
using SportsBookingApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SportsBookingApp.ViewModels
{
    public class BookingViewModel : BaseViewModel
    {

        public BookingViewModel(Center center, string sportname)
        {

            //Application.Current.MainPage.DisplayAlert("1", "inside 2 par. constructor bookingView", "OK");

            CourtsNames = new ObservableCollection<string>();
            //SelectedCourt = new ObservableCollection<Court>();
            
            SelectedCenter = center;
            GetCourts(center.CenterName, sportname);


            OpenGoogleMapLocationCommand = new Command(async () => await OpenGoogleMapLocationCommandAsync(SelectedCenter));
            //BookingCommand = new Command(async () => await BookingCommandAsync());
            GoBack = new Command(async () => await GoBackAsync());


        }

        /*
        public BookingViewModel(Center center, string sportname, string courtname)
        {

            Application.Current.MainPage.DisplayAlert("3", "inside 3 par. constructor bookingView", "OK");

            CourtsNames = new ObservableCollection<string>();
            //SelectedCourt = new ObservableCollection<Court>();

            SelectedCenter = center;
            GetCourts(center.CenterName, sportname);

            GetSelectedCourtData(center.CenterName, courtname);

            //BookedSlots = new ObservableCollection<Booking>();
            //GetBookedSlots(center.CenterName, courtname, BookingDate);

            BookingCommand = new Command(async () => await BookingCommandAsync());
            GoBack = new Command(async () => await GoBackAsync());


        }
        */

        public BookingViewModel( Center center, string sportname, string courtname, DateTime bookingdate)
        {
            
            //Application.Current.MainPage.DisplayAlert("4", "inside 4 par. constructor bookingView", "OK");

            CourtsNames = new ObservableCollection<string>();
            //SelectedCourt = new ObservableCollection<Court>();


            SelectedCenter = center;
            GetCourts(center.CenterName, sportname);

            GetSelectedCourtData(center.CenterName, courtname);

            BookedSlots = new ObservableCollection<Booking>();
            GetBookedSlots(center.CenterName, courtname,  bookingdate);


            //Application.Current.MainPage.DisplayAlert("oooo", "in 4 after data", "OK");


            OpenGoogleMapLocationCommand = new Command(async () => await OpenGoogleMapLocationCommandAsync(SelectedCenter));
            //BookingCommand = new Command(async () => await BookingCommandAsync());
            GoBack = new Command(async () => await GoBackAsync());


        }

        public Command OpenGoogleMapLocationCommand { get; set; }

        // write the function inside the constructor , or pass to it the center object
        private async Task OpenGoogleMapLocationCommandAsync(Center selectedCenter)
        {
            if (!double.TryParse(selectedCenter.CenterLatitude.ToString(), out double lat))
                return;
            if (!double.TryParse(selectedCenter.CenterLongitude.ToString(), out double lng))
                return;
            
            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = selectedCenter.CenterName,
                NavigationMode = NavigationMode.None

            });
            



            //await Application.Current.MainPage.DisplayAlert("location ", " view location", "OK");
            //await Application.Current.MainPage.Navigation.PopModalAsync();
            // await _navigationService.GoBackAsync();
        }
        public ObservableCollection<Booking> BookedSlots { get; set; }


        private async void GetBookedSlots(string centerName, string courtName, DateTime bookingdate)
        {
            var data = await new BookingDataService().GetBookedSlotsItemsByCenterAndCourtAndDateAsync( centerName,  courtName, bookingdate);
            BookedSlots.Clear();
            foreach (var item in data)
            {
                BookedSlots.Add(item);
            }

            var d = BookedSlots;
        }

        /*
        private DayOfWeek bookingDate;
        public DayOfWeek BookingDate
        {
            get { return bookingDate; }
            set
            {
                bookingDate = value;
                OnpropertyChanged("BookingDate");   //Call INPC Interface when property changes, so the view will know it has to update
            }
        }

        private void ChangeDate(DateTime newDate)
        {
            BookingDate = newDate;  //Assing your new date to your property
        }
        */

        private Center _SelectedCenter;
        public Center SelectedCenter
        {
            get { return _SelectedCenter; }
            set
            {
                _SelectedCenter = value;
                OnpropertyChanged();

            }
        }


        public ObservableCollection<string> CourtsNames { get; set; }

        /*
        private string _SelectedCourtInViewModel;
        public string SelectedCourtInViewModel
        {
            get { return _SelectedCourtInViewModel; }
            set
            {
                _SelectedCourtInViewModel = value;
                OnpropertyChanged();

            }
        }
        */


        /*
        private string _CourtsNames;
        public string CourtsNames
        {
            get { return _CourtsNames; }
            set
            {
                _CourtsNames = value;
                OnpropertyChanged();

            }
        }
        */
        /*
        void INavigatedAware.OnNavigatedTo(INavigationParameters parameters)
        {
            SelectedCenter = parameters.GetValue<Center>("SelectedCenter");
            SelectedSportName = parameters.GetValue<string>("SelectedSportName");
            GetCourts(SelectedCenter.CenterName, SelectedSportName);
        }
        */

        private async void GetCourts(string selectedCenterName, string selectedSportName)
        {
            var data = await new CourtDataService().GetCourtsNamesBySportAndCenterAsync(selectedCenterName, selectedSportName);

            CourtsNames.Clear();
            foreach (var item in data)
            {
                CourtsNames.Add(item);
            }

        }



        //public ObservableCollection<Court> SelectedCourt { get; set; }
        
        private Court _SelectedCourt;
        public Court SelectedCourt
        {
            get { return _SelectedCourt; }
            set
            {
                _SelectedCourt = value;
                OnpropertyChanged();

            }
        }
        /*
        private void SelectedBookingDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            Picker p = new Picker();
            p.SelectedItem = sender;
            BookedSlots = new ObservableCollection<Booking>();
            GetBookedSlots(SelectedCenter.CenterName, p.SelectedItem.ToString(), BookingDate);

            GetSelectedCourtData(SelectedCenter.CenterName, p.SelectedItem.ToString());
        }
        */
        /*
        private Court _PickedCourt;
        public Court PickedCourt
        {
            get { return _PickedCourt; }
            set
            {
                if(_PickedCourt != value)
                {
                    _PickedCourt = value;
                    BookedSlots = new ObservableCollection<Booking>();
                    GetBookedSlots(SelectedCenter.CenterName, _PickedCourt.CourtName, BookingDate);

                    GetSelectedCourtData(SelectedCenter.CenterName, _PickedCourt.CourtName);
                }

            }
        }
        */
        private async void GetSelectedCourtData(string selectedCenterName, string selectedCourtName)
        {
            var data = await new CourtDataService().GetCourtDataByCenterAndCourtNamesAsync(selectedCenterName, selectedCourtName);

            SelectedCourt = data[0];
            /*SelectedCourt.Clear();
            foreach (var item in data)
            {
                SelectedCourt.Add(item);
            }
            */
            var s = SelectedCourt;
            
        }
    


        //public Command BookingCommand { get; set; }
        public Command GoBack { get; set; }
        /*
        private DelegateCommand _bookingCommand;

        public DelegateCommand BookingCommand =>
            _bookingCommand ?? (_bookingCommand = new DelegateCommand(ExecuteBookingCommand));
        */

        //async void ExecuteBookingCommand();

        /*
        private async Task BookingCommandAsync()
        {

            //await Application.Current.MainPage.DisplayAlert("Well done", "Well done", "OK");

            //await Application.Current.MainPage.Navigation.PushModalAsync(new PaymentView() );
            //await Navigation.PushModalAsync(new PaymentView(SelectedBookingDate.Date, c.CenterName, s, SelectedCourt.SelectedItem.ToString(), SelectedStartingBookingTime.Time, SelectedEndingBookingTime.Time, TotalPaymentAmount));


            // await _navigationService.NavigateAsync("PaymentView");
            
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.LoginUser(Username, Password);
                if (Result)
                {
                    Preferences.Set("UserName", Username);
                    await _navigationService.NavigateAsync("/MainShell");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Username or Password", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
            
        
        }
    */
        /*
        private DelegateCommand _goBack;

        public DelegateCommand GoBack =>
            _goBack ?? (_goBack = new DelegateCommand(ExecuteGoBackCommand));
        */
        // async void ExecuteGoBackCommand();
        private async Task GoBackAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

    }
}
