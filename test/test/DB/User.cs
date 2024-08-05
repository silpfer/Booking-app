﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace test.DB
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
