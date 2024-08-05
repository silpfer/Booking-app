using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.DB
{
    public class Suite
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public int Number_guest { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Price { get; set; }
    }
}
