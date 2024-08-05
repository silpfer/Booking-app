using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace test.DB
{
    public class DataBase
    {
        private readonly SQLiteConnection connection;

        public DataBase(string path)
        {
            connection = new SQLiteConnection(path);
            connection.CreateTable<User>();
            connection.CreateTable<Suite>();
            connection.CreateTable<Comment>();
            connection.CreateTable<Book>();
        }
        //Опис таблиці заброньовано Book
        public List<Book> GetBook() 
        { 
            return connection.Table<Book>().ToList();
        }
        public int SaveBook(Book book)
        {
            return connection.Insert(book);
        }
        public void BookDeleteById(int bookID)
        {
            var bookToDelete = connection.Table<Book>().FirstOrDefault(b => b.ID == bookID);

            if (bookToDelete != null)
            {
                connection.Delete(bookToDelete);
            }
        }
        //Опис таблиці Comment
        public List<Comment> GetComment()
        {
            return connection.Table<Comment>().ToList();
        }
        public List<Comment> GetCommentBySuite(int suiteID)
        {
            return connection.Table<Comment>().Where(c => c.ID_Suite == suiteID).ToList();
        }
        public int SaveComment(Comment comment)
        {
            return connection.Insert(comment);
        }
        public void ClearComments()
        {
            connection.DeleteAll<Comment>();
        }
        //Опис таблиці Suite
        public List<Suite> GetSuite()
        {
            return connection.Table<Suite>().ToList();
        }
        public int SaveSuite(Suite suite)
        {
            return connection.Insert(suite);
        }
        public Suite GetSuiteByID(int id)
        {
            return connection.Table<Suite>().FirstOrDefault(s => s.ID == id);
        }
        public void ClearSuites()
        {
            connection.DeleteAll<Suite>();
        }
        //Опис таблиці User
        public List<User> GetUser()
        {
            return connection.Table<User>().ToList();
        }

        public int SaveUser(User user)
        {
            return connection.Insert(user);
        }

        public User GetUserById(int id)
        {
            return connection.Table<User>().FirstOrDefault(u => u.ID == id);
        }
        public string GetLoginById(int id)
        {
            var user = connection.Table<User>().FirstOrDefault(u => u.ID == id);
            return user?.Login;
        }

        public bool DeleteUserById(int id)
        {
            var existingUser = connection.Table<User>().FirstOrDefault(u => u.ID == id);
            if (existingUser != null)
            {
                connection.Delete(existingUser);
                return true;
            }
            return false;
        }

    }
}
