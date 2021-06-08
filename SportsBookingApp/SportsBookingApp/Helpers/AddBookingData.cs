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
    public class AddBookingData
    {
        FirebaseClient client;
        public List<Booking> Bookings { get; set; }


        
        public AddBookingData(string sportName, string courtName, string userName, string centerName, DateTime startingBookingTime, DateTime endingBookingTime, DateTime bookingDate, double totalPaymentAmount, string bookingStripeID)
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            Bookings = new List<Booking>()
            {
                new Booking
                {
                    SportName = sportName,
                    CourtName = courtName,
                    Username = userName,
                    CenterName = centerName,
                    StartingBookingTime = startingBookingTime,
                    EndingBookingTime = endingBookingTime,
                    BookingDate = bookingDate,
                    TotalPaymentAmount = totalPaymentAmount,
                    BookingStripeID = bookingStripeID

                }
            };
        }
        

        public AddBookingData()
        {
            client = new FirebaseClient("https://demooo-fa47d-default-rtdb.firebaseio.com/");
            Bookings = new List<Booking>()
            {
                new Booking
                {
                    SportName = "Ping Pong",
                    CourtName = "Table 2",
                    Username = "Moustafa",
                    CenterName = "Stars Center",
                    StartingBookingTime = DateTime.Now.AddHours(16),
                    EndingBookingTime =  DateTime.Now.AddHours(17),
                    BookingDate = System.DateTime.Now.AddDays(1).Date,
                    TotalPaymentAmount = 35

                    
                    //BookingStripeID

                },new Booking
                {
                    SportName = "Ping Pong",
                    CourtName = "Table 2",
                    Username = "Fadel",
                    CenterName = "Stars Center",
                    StartingBookingTime = DateTime.Now.AddHours(19.5),
                    EndingBookingTime =  DateTime.Now.AddHours(21),
                    BookingDate = System.DateTime.Now.AddDays(1).Date,
                    TotalPaymentAmount = 52.5,

                    //BookingStripeID

                }

                /*
                new Booking
                {
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    Username = "Moustafa",
                    CenterName = "BBC Futsal Center",
                    StartingBookingTime = DateTime.Now,
                    EndingBookingTime =  DateTime.Now.AddHours(1),
                    BookingDate = System.DateTime.Now.Date,
                    TotalPaymentAmount = 110

                },new Booking
                {
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    Username = "Fadel",
                    CenterName = "BBC Futsal Center",
                    StartingBookingTime = DateTime.Now.AddHours(3),
                    EndingBookingTime =  DateTime.Now.AddHours(5),
                    BookingDate = System.DateTime.Now.Date,
                    TotalPaymentAmount = 220

                },new Booking
                {
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    Username = "Moustafa",
                    CenterName = "BBC Futsal Center",
                    StartingBookingTime = DateTime.Now.AddHours(8),
                    EndingBookingTime =  DateTime.Now.AddHours(9),
                    BookingDate = System.DateTime.Now.Date,
                    TotalPaymentAmount = 110

                },new Booking
                {
                    SportName = "Futsal",
                    CourtName = "Court 1",
                    Username = "Fadel",
                    CenterName = "Champions Center",
                    //StartingBookingTime = DateTime.Now,
                    StartingBookingTime = DateTime.Now.AddHours(2),
                    EndingBookingTime =  DateTime.Now.AddHours(3),
                    BookingDate = System.DateTime.Now.Date,
                    TotalPaymentAmount = 110

                },new Booking
                {
                    SportName = "Futsal",
                    CourtName = "Court 2",
                    Username = "Moustafa",
                    CenterName = "Champions Center",
                    //StartingBookingTime = DateTime.Now,
                    StartingBookingTime = DateTime.Now.AddHours(2),
                    EndingBookingTime =  DateTime.Now.AddHours(3),
                    BookingDate = System.DateTime.Now.Date,
                    TotalPaymentAmount = 110

                }
                */
            };
        }


        public async Task AddBookingDataAsync()
        {
            try
            {
                foreach (var booking in Bookings)
                {
                    await client.Child("Bookings").PostAsync(new Booking()
                    {
                        SportName = booking.SportName,
                        CourtName = booking.CourtName,
                        Username = booking.Username,
                        CenterName = booking.CenterName,
                        StartingBookingTime = booking.StartingBookingTime,
                        EndingBookingTime = booking.EndingBookingTime,
                        BookingDate = booking.BookingDate,
                        TotalPaymentAmount = booking.TotalPaymentAmount,
                        BookingStripeID = booking.BookingStripeID
                    });
                }

                await Application.Current.MainPage.DisplayAlert("Successful booking", " Enjoy your time ", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}
