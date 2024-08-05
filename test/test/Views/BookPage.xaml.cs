using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {        
        
        public BookPage()
        {
            InitializeComponent();
        }
        // При відображенні сторінки
        protected override void OnAppearing()
        {
            ClearForm();
            base.OnAppearing();
            int idUser = UserStateManager.CurrentUserId;
            if (idUser != -1) { LoadBookings(idUser); }
            else ClearForm();
        }

        // Метод завантаження бронювань для конкретного користувача
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
                    // Обчислення загальної вартості бронювання
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
                // Фільтрація активних бронювань
                if (book.EndDate >= DateTime.Today)
                {
                    var bookingObject = new
                    {
                        ID = book.ID,
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

            BindingContext = bookedData; // Встановлення контексту даних для відображення
        }

        // Метод очищення форми
        private void ClearForm()
        {
            BindingContext = null;
        }

        // Обробник події видалення бронювання
        void Delete(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int bookID)
            {
                App.DataBase.BookDeleteById(bookID);
                int idUser = UserStateManager.CurrentUserId;
                if (idUser != -1) { LoadBookings(idUser); }
                else ClearForm();
            }
        }
    }
}