using Firebase.Database;
using Firebase.Database.Query;
using SportsBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SportsBookingApp.Helpers
{
    public class AddCenterData
    {
        FirebaseClient client;
        public List<Center> Centers { get; set; }

        public AddCenterData()
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            Centers = new List<Center>()
            {
                new Center
                {
                    CenterID = 1,
                    CenterName = "BBC Futsal Center",
                    CenterPhone = "03-123456",
                    CenterEmail = "BBCFutsalCenter@hotmail.com",
                    CenterPassword = "0000",
                    CenterImage = "bbc_futsal_center_img",
                    CenterRating = 4.7,
                    NoOfTotalSportsforCenter = 1,
                    NoOfTotalCourtsforCenter = 3,
                    CenterSports = "Futsal",
                    CenterLatitude = "3.194841680100653",
                    CenterLongitude = "101.71713055588967"

                },new Center
                {
                    CenterID = 2,
                    CenterName = "Stars Center",
                    CenterPhone = "03-223344",
                    CenterEmail = "StarsCenter@hotmail.com",
                    CenterPassword = "0000",
                    CenterImage = "star_center_img",
                    CenterRating = 4.8,
                    NoOfTotalSportsforCenter = 3,
                    NoOfTotalCourtsforCenter = 4,
                    CenterSports = "Ping Pong, Badminton, Gym",
                    CenterLatitude = "3.194841680100653",
                    CenterLongitude = "101.71713055588967"
                },new Center
                {
                    CenterID = 3,
                    CenterName = "Champions Center",
                    CenterPhone = "03-333444",
                    CenterEmail = "ChampionsCenter@hotmail.com",
                    CenterPassword = "0000",
                    CenterImage = "champions_center_img",
                    CenterRating = 4.7,
                    NoOfTotalSportsforCenter = 3,
                    NoOfTotalCourtsforCenter = 6,
                    CenterSports = "Futsal, Ping Pong, Basketball",
                    CenterLatitude = "3.194841680100653",
                    CenterLongitude = "101.71713055588967"
                }
            };

        }

        public async Task AddCenterDataAsync()
        {
            try
            {
                foreach (var center in Centers)
                {
                    await client.Child("Centers").PostAsync(new Center()
                    {
                        CenterID = center.CenterID,
                        CenterRating = center.CenterRating,
                        CenterImage = center.CenterImage,
                        CenterPassword = center.CenterPassword,
                        CenterEmail = center.CenterEmail,
                        CenterPhone = center.CenterPhone,
                        CenterName = center.CenterName,
                        NoOfTotalCourtsforCenter = center.NoOfTotalCourtsforCenter,
                        NoOfTotalSportsforCenter = center.NoOfTotalSportsforCenter,
                        CenterSports = center.CenterSports,
                        CenterLatitude = center.CenterLatitude,
                        CenterLongitude = center.CenterLongitude
                    });
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

    }

}
