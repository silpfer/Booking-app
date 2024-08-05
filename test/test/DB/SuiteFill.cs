using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace test.DB
{
    public class SuiteFill
    {
        public void fillDB()
        {
            var suite1 = new Suite()
            {
                Name = "Люкс 1",
                Type = "Квартира",
                City = "Київ",
                Number_guest = 2,
                Description = "Простора квартира в центрі міста",
                Img = "https://i.pinimg.com/564x/db/56/46/db5646f3e0dc56de39bc46312f605863.jpg",
                Price = "₴5000"
            };

            var suite2 = new Suite()
            {
                Name = "Сімейний номер",
                Type = "Готель",
                City = "Одеса",
                Number_guest = 4,
                Description = "Затишний номер для сім'ї біля моря",
                Img = "https://i.pinimg.com/564x/81/21/dd/8121dd5997b6021e5aa76fc5979f6f5d.jpg",
                Price = "₴3000"
            };

            var suite3 = new Suite()
            {
                Name = "Хостел 'Веселка'",
                Type = "Хостел",
                City = "Львів",
                Number_guest = 6,
                Description = "Зручне житло для молодіжного відпочинку",
                Img = "https://i.pinimg.com/564x/a8/71/e0/a871e0c33fdd21f643752e3bd8aec012.jpg",
                Price = "₴1000"
            };

            var suite4 = new Suite()
            {
                Name = "Апартаменти біля Львівської опери",
                Type = "Квартира",
                City = "Львів",
                Number_guest = 3,
                Description = "Стильні апартаменти поруч із символом міста",
                Img = "https://i.pinimg.com/564x/7b/d1/c2/7bd1c2ab0f736acdcb3a426939034092.jpg",
                Price = "₴4000"
            };

            var suite5 = new Suite()
            {
                Name = "Сучасний будинок біля Дніпра",
                Type = "Дім",
                City = "Київ",
                Number_guest = 6,
                Description = "Затишний будинок для великої сім'ї",
                Img = "https://i.pinimg.com/564x/e2/5f/fb/e25ffb31a40a747aad1aa9470b5e11d5.jpg",
                Price = "₴7000"
            };

            var suite6 = new Suite()
            {
                Name = "Готель 'Морський бриз'",
                Type = "Готель",
                City = "Одеса",
                Number_guest = 2,
                Description = "Відмінний вибір для відпочинку біля моря",
                Img = "https://i.pinimg.com/564x/da/a2/2d/daa22d265bc2ea58a654efc2263ca00b.jpg",
                Price = "₴2500"
            };

            App.DataBase.SaveSuite(suite1);
            App.DataBase.SaveSuite(suite2);
            App.DataBase.SaveSuite(suite3);
            App.DataBase.SaveSuite(suite4);
            App.DataBase.SaveSuite(suite5);
            App.DataBase.SaveSuite(suite6);
        }
    }
}
