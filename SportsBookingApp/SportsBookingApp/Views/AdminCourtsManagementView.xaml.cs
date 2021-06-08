using SportsBookingApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SportsBookingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCourtsManagementView : ContentPage
    {
        public AdminCourtsManagementView()
        {
            InitializeComponent();

            string c = Preferences.Get("CenterName", String.Empty);

            centerName.Text = c;

            AddCourtImage.Clicked += async (sender, args) =>
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    resultImage.Source = ImageSource.FromStream(() => stream);
                }
            };


            AddCourt.Clicked += async (sender, args) =>
            {
                if( c != null && sportName.Text != null && courtName.Text != null && courtPaymentTimeScale.Text != null && courtPaymentCostScale.Text != null && courtName.Text != null)
                {
                    var acd = new AddCourtData(c, sportName.Text, courtName.Text, courtPaymentTimeScale.Text, double.Parse(courtPaymentCostScale.Text), courtName.Text + "_img");
                    await acd.AddCourtDataAsync();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Warning", " Missing Information ", "OK");
            };
        }
    }
}