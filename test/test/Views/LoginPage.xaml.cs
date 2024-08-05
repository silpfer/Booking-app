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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Перехід на сторінку реєстрації
        private async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        // Перевірка введеного логіну та паролю для входу
        private async void CheckLogin(object sender, EventArgs e)
        {
            string loginUser = login.Text;
            string passwordUser = password.Text;

            if (string.IsNullOrEmpty(loginUser) || string.IsNullOrEmpty(passwordUser))
            {
                await DisplayAlert("Помилка", "Дані не введено!", "OK");
            }
            else
            {
                loginUser.Trim();
                passwordUser.Trim();
                List<User> users = App.DataBase.GetUser();
                User foundUser = users.FirstOrDefault(u => u.Login == loginUser);

                if (foundUser != null)
                {
                    if (foundUser.Password == passwordUser)
                    {
                        // Перехід на сторінку користувача при успішній автентифікації
                        await Navigation.PushAsync(new UserPage(foundUser.ID));
                        UserStateManager.SetCurrentUser(foundUser.ID);
                    }
                    else
                    {
                        await DisplayAlert("Помилка", "Неправильний пароль", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Помилка", "Користувача з таким логіном не знайдено", "OK");
                }
            }
        }


    }
}