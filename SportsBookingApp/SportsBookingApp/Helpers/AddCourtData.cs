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
    public class AddCourtData
    {
        FirebaseClient client;
        public List<Court> Courts { get; set; }

        //string centerName, string sportName, string courtPaymentTimeScale, double courtPaymentCostScale, string courtImage, int sportID = 9, int courtID = 9, string courtName, int maxReservationATime = 1

        public AddCourtData(string centerName, string sportName, string courtName , string courtPaymentTimeScale, double courtPaymentCostScale, string courtImage)
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            Courts = new List<Court>()
            {
                new Court
                {
                    SportName = sportName,
                    CourtName = courtName,
                    //SportID = sportID,
                    CenterName = centerName,
                    //CourtID = courtID,
                    CourtPaymentTimeScale = courtPaymentTimeScale,
                    CourtPaymentCostScale = courtPaymentCostScale,
                    CourtImage = courtImage,
                    //MaxReservationATime = maxReservationATime

                }
            };
        }

        public AddCourtData()
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            Courts = new List<Court>()
            {
                new Court
                {
                    CourtID = 1,
                    SportID = 1,
                    SportName = "Ping Pong",
                    CourtName = "Table 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 35,
                    CenterName = "Stars Center",

                    CourtImage = "ping_pong_table1.jpg"
                },new Court
                {
                    CourtID = 2,
                    SportID = 1,
                    SportName = "Ping Pong",
                    CourtName = "Table 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 35,
                    CenterName = "Stars Center",

                    CourtImage = "ping_pong_table2.jpg"
                },new Court
                {
                    CourtID = 1,
                    SportID = 1,
                    SportName = "Ping Pong",
                    CourtName = "Table 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 30,
                    CenterName = "Champions Center",

                    CourtImage = "ping_pong_table1.jpg"


                    // may need to add bokmemb & totrev for all courts


                },new Court
                {
                    CourtID = 2,
                    SportID = 1,
                    SportName = "Ping Pong",
                    CourtName = "Table 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 30,
                    CenterName = "Champions Center",

                    CourtImage = "ping_pong_table2.jpg "
                },new Court
                {
                    CourtID = 1,
                    SportID = 2,
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 110,
                    CenterName = "BBC Futsal Center",

                    CourtImage = "futsal_court3.jpg"
                },new Court
                {
                    CourtID = 2,
                    SportID = 2,
                    SportName = "Futsal",
                    CourtName = "Court 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 110,
                    CenterName = "BBC Futsal Center",

                    CourtImage = "futsal_court2.jpg"
                },new Court
                {
                    CourtID =3,
                    SportID = 2,
                    SportName = "Futsal",
                    CourtName = "Court 3",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 110,
                    CenterName = "BBC Futsal Center",

                    CourtImage = "futsal_court3.jpg"
                },new Court
                {
                    CourtID = 1,
                    SportID = 2,
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 100,
                    CenterName = "Champions Center",

                    CourtImage = "futsal_court3.jpg "
                },new Court
                {
                    CourtID = 2,
                    SportID = 2,
                    SportName = "Futsal",
                    CourtName = "Court 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 100,
                    CenterName = "Champions Center",

                    CourtImage = "futsal_court2.jpg"
                },new Court
                {
                    CourtID = 1,
                    SportID = 3,
                    SportName = "Badminton",
                    CourtName = "Court 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 40,
                    CenterName = "Stars Center",

                    CourtImage = "badminton_court1.jpg "
                },new Court
                {
                    CourtID = 2,
                    SportID = 3,
                    SportName = "Badminton",
                    CourtName = "Court 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 40,
                    CenterName = "Stars Center",

                    CourtImage = "badminton_court2.jpg "
                },new Court
                {
                    CourtID = 1,
                    SportID = 4,
                    SportName = "Gym",
                    CourtName = "Hall 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =15,
                    CourtPaymentCostScale = 5,
                    CenterName = "Stars Center",

                    CourtImage = "gym_hall1.jpg"
                },new Court
                {
                    CourtID = 1,
                    SportID = 5,
                    SportName = "Basketball",
                    CourtName = "Court 1",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 100,
                    CenterName = "Champions Center",

                    CourtImage = "basketball_court1.jpg"
                },new Court
                {
                    CourtID = 2,
                    SportID = 5,
                    SportName = "Basketball",
                    CourtName = "Court 2",
                    CourtPaymentTimeScale = "Hour",
                    MaxReservationATime =1,
                    CourtPaymentCostScale = 100,
                    CenterName = "Champions Center",

                    CourtImage = "basketball_court1.jpg"
                }
            };
        }

        public async Task AddCourtDataAsync()
        {
            try
            {
                foreach (var court in Courts)
                {
                    await client.Child("Courts").PostAsync(new Court
                    {
                        CourtID = court.CourtID,
                        SportID = court.SportID,
                        SportName = court.SportName,
                        CourtName = court.CourtName,
                        CourtPaymentTimeScale = court.CourtPaymentTimeScale,
                        MaxReservationATime = court.MaxReservationATime,
                        CourtPaymentCostScale = court.CourtPaymentCostScale,
                        CenterName = court.CenterName,

                        CourtImage = court.CourtImage,

                        BookingMember = court.BookingMember,
                        TotalRevenueForTheCourtPerDay = court.TotalRevenueForTheCourtPerDay

                    });
                }

                await Application.Current.MainPage.DisplayAlert("Done", " The new court was added successfully ", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }

}
