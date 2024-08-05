using System;
using test.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using test.DB;
using System.IO;

namespace test
{
    public partial class App : Application
    {
        private static DataBase dataBase;
        // Властивість, що повертає екземпляр бази даних
        public static DataBase DataBase
        {
            get
            {
                // Якщо база даних ще не ініціалізована, створіть новий екземпляр бази даних
                if (dataBase == null)
                    dataBase = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"db.sqlite"));
                return dataBase;
            }
            
        }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); // Встановлення головної сторінки за замовчуванням як AppShell
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        
    }
}
