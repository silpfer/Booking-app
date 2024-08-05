using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using test.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private int userId;
        // Конструктор класу UserPage, приймає ідентифікатор користувача
        public UserPage(int i)
        {
            InitializeComponent();
            userId = i;
            // Встановлення контексту даних відповідно до користувача за його ідентифікатором
            BindingContext = App.DataBase.GetUserById(userId);

        }

        // Метод переходу до сторінки історії подорожей
        private async void History(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        // Метод виходу з акаунта користувача
        private async void Out(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
            UserStateManager.SetCurrentUser(-1);
        }

        // Метод видалення користувача
        private async void Delete(object sender, EventArgs e)
        {
            bool n = App.DataBase.DeleteUserById(userId);
            if (n) { await DisplayAlert("Видалення", "Ваш акаунт успішно видалено!", "OK");
                await Navigation.PushAsync(new LoginPage());
                UserStateManager.SetCurrentUser(-1);
            }
        }
    }
}