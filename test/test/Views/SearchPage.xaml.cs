using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        public SearchPage ()
		{
            InitializeComponent();
            // Встановлюємо мінімальні дати для вибору заїзду та виїзду
            fromDate.MinimumDate = DateTime.Now;
            toDate.MinimumDate = DateTime.Now;
        }
        protected override void OnAppearing()
        {
            // Оновлення списку апартаментів при повторному відображенні сторінки
            LoadSuites();
        }

        private void FromDate_Changed(object sender, PropertyChangedEventArgs e)
        {
            // Оновлення дати "мінімальної дати виїзду" при зміні дати заїзду
            toDate.MinimumDate = fromDate.Date;
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Оновлення списку апартаментів при виборі міста
            LoadSuites();
        }
        private void PeopleEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Оновлення списку апартаментів при зміні кількості осіб
            LoadSuites();
        }
        private void LoadSuites()
        {
            // Отримання усіх апартаментів з бази даних
            var allSuites = App.DataBase.GetSuite();
            
            // Отримання вибраного міста
            var collectionView = this.FindByName<CollectionView>("suites");

            // Фільтрація за містом, якщо воно обране
            string selectedCity = picker.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCity) && selectedCity != "Всі міста")
            {
                allSuites = allSuites.Where(s => s.City == selectedCity).ToList();
            }

            // Фільтрація за кількістю осіб, якщо вона вказана
            if (!string.IsNullOrEmpty(peopleEntry.Text))
            {
                int numberOfPeople;
                if (int.TryParse(peopleEntry.Text, out numberOfPeople))
                {
                    allSuites = allSuites.Where(s => s.Number_guest >= numberOfPeople).ToList();
                }
            }

            // Відображення доступних апартаментів у колекції
            if (allSuites != null)
            {
                if (collectionView != null)
                {
                    collectionView.ItemsSource = allSuites;
                }
            }
            // Отримання списку заброньованих апартаментів
            List<Book> bookedRooms = App.DataBase.GetBook();
            DateTime selectedFromDate = fromDate.Date.AddHours(12);
            DateTime selectedToDate = toDate.Date.AddHours(12);
            // Фільтрація доступних апартаментів за датами
            var availableSuites = allSuites.Where(suite =>
            {
                return !bookedRooms.Any(book => book.ID_Suite == suite.ID &&
                                            (selectedFromDate >= book.StartDate &&
                                            selectedToDate <= book.EndDate));
            }).ToList();
            // Відображення тільки доступних апартаментів у колекції
            if (collectionView != null)
            {
                collectionView.ItemsSource = availableSuites;
            }
        }
        // Відкриття сторінки апартаменту при кліку на кнопці
        private void OpenSuite_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int suiteID)
            {
                DateTime fromDateValue = fromDate.Date;
                DateTime toDateValue = toDate.Date;
                Navigation.PushAsync(new SuitePage(suiteID, fromDateValue, toDateValue));
            }
        }
        // Обробники зміни дати заїзду
        private void FromDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadSuites();
        }

        private void ToDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadSuites();
        }
    }
}