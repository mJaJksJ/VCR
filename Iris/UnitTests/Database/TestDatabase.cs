using Iris.Database;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Database
{
    public class TestDatabase
    {
        private static DatabaseContext _instance;

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseContext("Data Source=TestDatabase.db");
                    _instance.Database.Migrate();
                }

                return _instance;
            }
        }

    }
}
