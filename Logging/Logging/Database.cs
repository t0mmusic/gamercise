using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        /**
         * @desc    Constructs a Database object
         * @param   {string} dbPath: Path to the database
        */
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        /**
        * @desc     Returns list of Person objects from database
        * @returns  {Task<List<Person>>}: List of Person objects
        */
        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        /**
        * @desc     Adds new Person to database
        * @returns  {Task<int>}: Number of rows added to table
        */
        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
    }
}
