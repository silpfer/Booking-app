using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuitePage : ContentPage
    {
        public int idUser, idSuite;
        DateTime start, end;
        public SuitePage(int id_Suite, DateTime fromDateValue, DateTime toDateValue)
        {
            InitializeComponent();

            idSuite = id_Suite;
            BindingContext = App.DataBase.GetSuiteByID(id_Suite);

            DatePicker fromDate = this.FindByName<DatePicker>("fromDate");
            DatePicker toDate = this.FindByName<DatePicker>("toDate");

            fromDate.Date = fromDateValue;
            toDate.Date = toDateValue;
        }
        //Завантаження сторінки
        protected override void OnAppearing()
        {
            base.OnAppearing();
            idUser = UserStateManager.CurrentUserId;
            LoadComments();
        }
        //Завантаження коментарів
        private void LoadComments()
        {
            var comm = App.DataBase.GetCommentBySuite(idSuite);
            comments.ItemsSource = comm;
        }
        //Додавання коментарів в базу даних
        private async void AddComment(object sender, EventArgs e)
        {
            if (idUser == -1)
            {
                await DisplayAlert("Помилка", "Ви не увійшли у свій акаунт. Австоризуйтеся, щоб залишати коментарі.", "OK");
            }
            else
            {
                var comment = new Comment
                {
                    Name_User = App.DataBase.GetLoginById(idUser),
                    ID_Suite = idSuite,
                    Text = comm.Text
                };

                App.DataBase.SaveComment(comment);

                var updatedComments = App.DataBase.GetCommentBySuite(idSuite);
                comments.ItemsSource = updatedComments;
            }
            comm.Text = "";
        }
        //Додавання бронювання
        private async void Book(object sender, EventArgs e)
        {
            if(idUser == -1)
            {
                await DisplayAlert("Помилка", "Ви не увійшли у свій акаунт. Австоризуйтеся, щоб забронювати апартаменти.", "OK");
            }
            else
            {
                DatePicker fromDate = this.FindByName<DatePicker>("fromDate");
                DatePicker toDate = this.FindByName<DatePicker>("toDate");

                start = fromDate.Date.AddHours(12);
                end = toDate.Date.AddHours(12);

                // Отримання замовлень користувача для конкретних апартаментів з бази даних
                var userBookings = App.DataBase.GetBook().Where(book => book.ID_User == idUser && book.ID_Suite == idSuite).ToList();

                // Перевірка наявності замовлення з такими ж самими датами користувача
                bool hasSameUserBooking = userBookings.Any(book => book.StartDate == start && book.EndDate == end);

                if (hasSameUserBooking)
                {
                    await DisplayAlert("Помилка", "Ви вже зробили таке ж саме замовлення раніше.", "OK");
                }
                else
                {
                    // Отримання всіх замовлень для цих апартаментів з бази даних
                    var allBookingsForSuite = App.DataBase.GetBook().Where(book => book.ID_Suite == idSuite).ToList();

                    // Перевірка наявності замовлень для цих апартаментів на вказані дати іншими користувачами
                    bool hasBookingForTheseDates = allBookingsForSuite.Any(book => (start >= book.StartDate && start < book.EndDate) || (end > book.StartDate && end <= book.EndDate));

                    if (hasBookingForTheseDates)
                    {
                        await DisplayAlert("Помилка", "Ці апартаменти вже заброньовані на ці дати іншим користувачем.", "OK");
                    }
                    else
                    {
                        var newBook = new Book
                        {
                            ID_User = idUser,
                            ID_Suite = idSuite,
                            StartDate = start,
                            EndDate = end
                        };

                        int insertedId = App.DataBase.SaveBook(newBook);
                        await DisplayAlert("Успішно!", "Заброньовані апартаменти можна переглянути на вкладці Бронювання.", "OK");
                    }
                }
            }
        }

    }
}