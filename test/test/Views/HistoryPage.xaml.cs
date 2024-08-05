using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }
        // При відображенні сторінки очищає форму та завантажує дані про бронювання користувача
        protected override void OnAppearing()
        {
            ClearForm();
            base.OnAppearing();
            int idUser = UserStateManager.CurrentUserId;
            if (idUser != -1) { LoadBookings(idUser); }
            else ClearForm();
        }
        // Метод завантаження даних про бронювання користувача
        private void LoadBookings(int idUser)
        {
            List<object> bookedData = new List<object>();
            var bookedRooms = App.DataBase.GetBook().Where(book => book.ID_User == idUser).ToList();
            var suites = this.FindByName<CollectionView>("suites");
            foreach (var book in bookedRooms)
            {
                var suite = App.DataBase.GetSuiteByID(book.ID_Suite);
                var user = App.DataBase.GetUserById(book.ID_User);

                double totalPrice = 0;

                if (suite != null)
                {
                    TimeSpan duration = book.EndDate - book.StartDate;
                    int numberOfDays = (int)duration.TotalDays;

                    if (numberOfDays > 0)
                    {
                        double pricePerDay = 0.0;
                        string trimmedPrice = suite.Price.Substring(1);

                        if (double.TryParse(trimmedPrice, out pricePerDay))
                        {
                            totalPrice = pricePerDay * numberOfDays;
                        }
                        else
                        {
                            totalPrice = 0.0;
                        }
                    }
                }
                if (book.EndDate < DateTime.Today)
                {
                    var bookingObject = new
                    {
                        NameUser = user?.Name,
                        Secondname = user?.Secondname,
                        Name = suite?.Name,
                        StartDate = book.StartDate,
                        EndDate = book.EndDate,
                        Address = suite?.City,
                        Price = totalPrice
                    };
                    bookedData.Add(bookingObject);
                }
            }

            BindingContext = bookedData;
        }
        // Метод очищення контексту даних форми
        private void ClearForm()
        {
            BindingContext = null;
        }
    }
}