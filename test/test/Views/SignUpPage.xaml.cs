using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        // Перехід на сторінку авторизації
        private async void LoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        // Додавання нового користувача
        private async void AddUser(object sender, EventArgs e)
        {
            // Отримання та перевірка введених даних
            string nameUser = name.Text;
            string secondName = secondname.Text;
            string loginUser = login.Text;
            string passwordUser = password.Text;

            
            if (string.IsNullOrEmpty(loginUser) || string.IsNullOrEmpty(passwordUser) || string.IsNullOrEmpty(nameUser) || string.IsNullOrEmpty(secondName))
            {
                await DisplayAlert("Помилка", "Дані не введено!", "OK");
            }
            else
            {
                nameUser = nameUser.Trim();
                secondName = secondName.Trim();
                loginUser = loginUser.Trim();
                passwordUser = passwordUser.Trim();
                bool userExists = App.DataBase.GetUser().Any(u => u.Login == loginUser);
                if (nameUser.Length < 2)
                {
                    await DisplayAlert("Помилка", "Ім'я мінімум 2 символи!", "OK");
                }
                else if (secondName.Length < 2)
                {
                    await DisplayAlert("Помилка", "Прізвище мінімум 2 символи!", "OK");
                }
                else if (loginUser.Length < 2)
                {
                    await DisplayAlert("Помилка", "Прізвище мінімум 2 символи!", "OK");
                }
                else if (!IsComplexityValid(passwordUser))
                {
                    await DisplayAlert("Помилка", "Пароль повинен містити цифри, букви верхнього та нижнього регістру та спеціальні символи!", "OK");
                }
                else if (passwordUser.Length < 6)
                {
                    await DisplayAlert("Помилка", "Пароль повинен бути більше 6 символів!", "OK");
                }
                else if (userExists)
                {
                    await DisplayAlert("Помилка", "Користувач з таким логіном вже існує!", "OK");
                }
                else
                {
                    await DisplayAlert("Успішно зареєстровано!", "", "OK");
                    User user = new User()
                    {
                        Name = nameUser,
                        Secondname = secondName,
                        Login = loginUser,
                        Password = passwordUser
                    };
                    App.DataBase.SaveUser(user);
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            
        }

        // Перевірка складності паролю
        public bool IsComplexityValid(string password)
        {
            bool hasDigit = password.Any(char.IsDigit); // Перевірка, чи є цифри
            bool hasUpper = password.Any(char.IsUpper); // Перевірка, чи є букви верхнього регістру
            bool hasLower = password.Any(char.IsLower); // Перевірка, чи є букви нижнього регістру
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch)); // Перевірка, чи є спеціальні символи

            return hasDigit && hasUpper && hasLower && hasSpecial;
        }
    }
}