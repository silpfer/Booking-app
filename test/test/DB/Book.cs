using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.DB
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Suite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
