using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace test.DB
{
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name_User { get; set; }
        public int ID_Suite { get; set; }
        public string Text { get; set; }
    }
}
