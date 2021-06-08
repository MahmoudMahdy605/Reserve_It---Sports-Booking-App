using SportsBookingApp.Helpers;
using SportsBookingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Stripe;
using Acr.UserDialogs;

namespace SportsBookingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentView : ContentPage
    {
        PaymentViewModel cvm;

        string mycustomer;
        string getchargedID;
        string refundID;

        string CHARGEDID;

        public PaymentView(DateTime bookingDate, string centerName, string sportName, string courtName, string startingBookingTime, string endingBookingTime, double TotalPaymentAmount)
        {
            InitializeComponent();

            //startingBookingTime AND endingBookingTime  are TimeSpan not DateTime  ...... Done


            var uname = Preferences.Get("UserName", String.Empty);
            if (String.IsNullOrEmpty(uname))
                username.Text = "Guest";
            else username.Text = uname;

            centername.Text = centerName;

            courtname.Text = courtName;

            
            //bookingdate.Text = bookingDate.Date.ToString();
            cvm = new PaymentViewModel(bookingDate);
            this.BindingContext = cvm;

            bookingtime.Text = startingBookingTime.ToString() + " - " + endingBookingTime.ToString() ;

            double RoundedTotalPaymentAmount = Math.Round(TotalPaymentAmount, 1 , MidpointRounding.ToEven);
            totalpaymentamount.Text = "RM " + RoundedTotalPaymentAmount.ToString();

            string totalp = RoundedTotalPaymentAmount.ToString();

            DateTime s = Convert.ToDateTime(startingBookingTime);
            DateTime d = Convert.ToDateTime(endingBookingTime);


            NewEventHandler.Clicked += async (sender, args) =>
            {
                
                // if check payment from Moustafa is true, then add booking to firebase
                try
                {
                    
                    //StripeConfiguration.SetApiKey("sk_test_51IpayhGP2IgUXM55te5JbGRu14MOp6AU6GORVFhqpOilEOp96ERDzKCi1VN9rDLrOmOEwNPqgOvQuIyaNg8YKfkL00Qoq8a7QX");
                    StripeConfiguration.SetApiKey("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");


                    //This are the sample test data use MVVM bindings to send data to the ViewModel

                    Stripe.TokenCardOptions stripcard = new Stripe.TokenCardOptions();

                    stripcard.Number = cardnumber.Text;
                    stripcard.ExpYear = Int64.Parse(expiryYear.Text);
                    stripcard.ExpMonth = Int64.Parse(expirymonth.Text);
                    stripcard.Cvc = cvv.Text;
                    //stripcard.Cvc = Int64.Parse(cvv.Text);

                    

                    //Step 1 : Assign Card to Token Object and create Token

                    Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
                    token.Card = stripcard;
                    Stripe.TokenService serviceToken = new Stripe.TokenService();
                    Stripe.Token newToken = serviceToken.Create(token);

                    // Step 2 : Assign Token to the Source

                    var options = new SourceCreateOptions
                    {
                        Type = SourceType.Card,
                        Currency = "myr",
                        Token = newToken.Id
                    };

                    var service = new SourceService();
                    Source source = service.Create(options);

                    //Step 3 : Now generate the customer who is doing the payment

                    Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
                    {
                        Name = username.Text,
                        Email = "hoda7.kimmo@gmail.com",
                        Description = "Reservation of " + courtname.Text + "from" + startingBookingTime.ToString() + " to " + endingBookingTime.ToString(),
                    };

                    var customerService = new Stripe.CustomerService();
                    Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

                    mycustomer = stripeCustomer.Id; // Not needed

                    //Step 4 : Now Create Charge Options for the customer. 

                    var chargeoptions = new Stripe.ChargeCreateOptions
                    {
                        //Amount = (Int64.Parse(RoundedTotalPaymentAmount)) * 100,
                        //(int(RoundedTotalPaymentAmount))
                        //Amount = Convert.ToInt32(RoundedTotalPaymentAmount) * 100,
                        //Amount = (long?)(double.Parse(RoundedTotalPaymentAmount) * 100),
                        Amount = (long?)(double.Parse(totalp) * 100),
                        Currency = "MYR",
                        ReceiptEmail = "moustafa.mahdy1997@gmail.com",
                        Customer = stripeCustomer.Id,
                        Source = source.Id

                    };

                    //Step 5 : Perform the payment by  Charging the customer with the payment. 
                    var service1 = new Stripe.ChargeService();
                    Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment

                    
                    getchargedID = charge.Id;
                    CHARGEDID = charge.Id;
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Alert(ex.Message, null, "ok");
                    Console.Write("error" + ex.Message);

                    //await Application.Current.MainPage.DisplayAlert("error ", ex.Message, "OK");

                }
                finally
                {
                    //if (getchargedID != null)
                    if (getchargedID != null)
                    {
                        var acd = new AddBookingData(sportName, courtName, username.Text, centerName, s, d, bookingDate, RoundedTotalPaymentAmount, CHARGEDID);
                        await acd.AddBookingDataAsync();


                        UserDialogs.Instance.Alert("Payment Successed", "Success", "Ok");
                        Xamarin.Forms.Application.Current.MainPage = new MainTabbedView();
                        //await Application.Current.MainPage.DisplayAlert("Payment Successed ", "Success", "OK");

                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Something Wrong", "Faild", "OK");
                        //await Application.Current.MainPage.DisplayAlert("Payment Error ", "faild", "OK");
                        
                    }
                }
                

                

                /*
                 * var acd = new AddBookingData(sportName, courtName, username.Text, centerName, s, d, bookingDate, RoundedTotalPaymentAmount);
                await acd.AddBookingDataAsync();
                /*
                

                //Application.Current.MainPage = new MainTabbedView();

                //await Navigation.PopModalAsync();
                //await Navigation.PopToRootAsync();
                /*
                Navigation.InsertPageBefore(new NewPage(), Navigation.NavigationStack[0]);
                await Navigation.PopToRootAsync();
                */

            };

            /*
            public void GetCustomerInformationID(object sender, EventArgs e)
            {
                var service = new CustomerService();
                var customer = service.Get(mycustomer);
                var serializedCustomer = JsonConvert.SerializeObject(customer);
                //  var UserDetails = JsonConvert.DeserializeObject<CustomerRetriveModel>(serializedCustomer);

            }


            public void GetAllCustomerInformation(object sender, EventArgs e)
            {
                var service = new CustomerService();
                var options = new CustomerListOptions
                {
                    Limit = 3,
                };
                var customers = service.List(options);
                var serializedCustomer = JsonConvert.SerializeObject(customers);
            }


            public void GetRefundForSpecificTransaction(object sender, EventArgs e)
            {
                var refundService = new RefundService();
                var refundOptions = new RefundCreateOptions
                {
                    Charge = getchargedID,
                };
                Refund refund = refundService.Create(refundOptions);
                refundID = refund.Id;
            }


            public void GetRefundInformation(object sender, EventArgs e)
            {
                var service = new RefundService();
                var refund = service.Get(refundID);
                var serializedCustomer = JsonConvert.SerializeObject(refund);

            }
            */

            /*

            async Task NewEventHandler(object sender, EventArgs e)
            {
                // if check payment from Moustafa is true, then

                // add booking to firebase

                var acd = new AddBookingData(sportName, courtName, username.Text, centerName, s, d, bookingDate, TotalPaymentAmount);
                await acd.AddBookingDataAsync();
            }
            */
        }




        /*
        //private void NewEventHandlerAsync(object sender, EventArgs e)
        private async Task NewEventHandler(object sender, EventArgs e)
        {
            // if check payment from Moustafa is true, then

            // add booking to firebase

            var acd = await new AddBookingData( sportName, courtname.Text, username.Text, centername.Text,  startingBookingTime,  endingBookingTime,  bookingDate,  totalPaymentAmount;
            
            //await acd.AddCourtDataAsync();

            


            asm = new BookingViewModel(c, s, SelectedCourt.SelectedItem.ToString(), SelectedBookingDate.Date);

            SelectedCourt.SelectedItem = SelectedCourt.SelectedItem;
            this.BindingContext = asm;
            
        }
    */


        /*
        public void GetCustomerInformationID(object sender, EventArgs e)
            {
                var service = new CustomerService();
                var customer = service.Get(mycustomer);
                var serializedCustomer = JsonConvert.SerializeObject(customer);
                //  var UserDetails = JsonConvert.DeserializeObject<CustomerRetriveModel>(serializedCustomer);

            }


            public void GetAllCustomerInformation(object sender, EventArgs e)
            {
                var service = new CustomerService();
                var options = new CustomerListOptions
                {
                    Limit = 3,
                };
                var customers = service.List(options);
                var serializedCustomer = JsonConvert.SerializeObject(customers);
            }


            public void GetRefundForSpecificTransaction(object sender, EventArgs e)
            {
                var refundService = new RefundService();
                var refundOptions = new RefundCreateOptions
                {
                    Charge = getchargedID,
                };
                Refund refund = refundService.Create(refundOptions);
                refundID = refund.Id;
            }


            public void GetRefundInformation(object sender, EventArgs e)
            {
                var service = new RefundService();
                var refund = service.Get(refundID);
                var serializedCustomer = JsonConvert.SerializeObject(refund);

            }
        */



    }
}