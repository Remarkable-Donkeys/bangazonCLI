using System;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class DatabaseInterface
    {



        private string _connectionString;
        private SqliteConnection _connection;


        public DatabaseInterface(string environmentVariable)
        {
            //sets up environment variable connection
            // _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BANGAZONCLI")}";
            _connectionString = $"Data Source={Environment.GetEnvironmentVariable(environmentVariable)}";

            _connection = new SqliteConnection(_connectionString);
        }



        //used to return value of the unique id when new data is entered
        public void Query(string command, Action<SqliteDataReader> handler)
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = command;

                using (SqliteDataReader dataReader = dbcmd.ExecuteReader())
                {
                    handler(dataReader);
                }

                dbcmd.Dispose();
            }
        }

        //Update method can be used to change or delete data in the database
        public void Update(string command)
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = command;
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
            }
        }

        //Insert method can be used to add data to the database and immediately return the unique id of the data entered
        public int Insert(string command)
        {
            int insertedItemId = 0;

            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = command;

                dbcmd.ExecuteNonQuery();

                this.Query("select last_insert_rowid()",
                    (SqliteDataReader reader) =>
                    {
                        while (reader.Read())
                        {
                            insertedItemId = reader.GetInt32(0);
                        }
                    }
                );

                dbcmd.Dispose();
            }

            //returns id of the data entered
            return insertedItemId;
        }

        internal void Query(string v, object p)
        {
            throw new NotImplementedException();
        }

        public void CheckDatabase()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                // Query the Customer table to see if table is created
                dbcmd.CommandText = $"select `Id` from `Customer`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {

                    }
                    dbcmd.Dispose();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE IF NOT EXISTS `Customer` (
                            `Id`	integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `FirstName`	varchar(80) NOT NULL, 
                            `LastName`	varchar(80) NOT NULL, 
                            `DateCreated` string NOT NULL,
                            `LastActive` string,
                            `Address` string,
                            `City` string,
                            `State` string,
                            `PostalCode` string,
                            `Phone` string
                        )";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                    }
                }
                // Query the Product table to see if table is created
                dbcmd.CommandText = $"select `Id` from `Product`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {

                    }
                    dbcmd.Dispose();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE `Product` (
                            `Id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Name` string NOT NULL,
                            `Description` string NOT NULL,
                            `Price` double NOT NULL,
                            `CustomerId` int NOT NULL,
                            `Quantity` int NOT NULL,
                            `DateAdded` string NOT NULL,
                            FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`) 
                        )";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                    }
                }
                // Query the Order table to see if table is created
                dbcmd.CommandText = $"select `Id` from `Order`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {

                    }
                    dbcmd.Dispose();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE `Order` (
                            `Id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `CustomerId` int NOT NULL,
                            `DateCreated` string NOT NULL,
                            `PaymentTypeId` int,
                            `DateOrdered` string,
                            FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`),
                            FOREIGN KEY(`PaymentTypeId`) REFERENCES `PaymentType`(`Id`) 
                        )";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                    }
                }
                // Query the OrderedProduct table to see if table is created
                dbcmd.CommandText = $"select `Id` from `OrderedProduct`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {

                    }
                    dbcmd.Dispose();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE `OrderedProduct` (
                            `Id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `ProductId` int NOT NULL,
                            `OrderId` int NOT NULL,
                            FOREIGN KEY(`ProductId`) REFERENCES `Product`(`Id`),
                            FOREIGN KEY(`OrderId`) REFERENCES `Order`(`Id`) 
                        )";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                    }
                }
                
                // Query the PaymentType table to see if table is created
                dbcmd.CommandText = $"select `Id` from `PaymentType`";

                try
                {
                    // Try to run the query. If it throws an exception, create the table
                    using (SqliteDataReader reader = dbcmd.ExecuteReader())
                    {

                    }
                    dbcmd.Dispose();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("no such table"))
                    {
                        dbcmd.CommandText = $@"CREATE TABLE `PaymentType` (
                            `Id` integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                            `Type` string NOT NULL,
                            `AccountNumber` string NOT NULL,
                            `CustomerId` int NOT NULL,
                            FOREIGN KEY(`CustomerId`) REFERENCES `Customer`(`Id`) 
                        )";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                    }
                }

                _connection.Close();
            }
        }
        public void NukeDB()
        {
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = "DELETE FROM OrderedProduct; DELETE FROM `Order`; DELETE FROM Product; DELETE FROM PaymentType; DELETE FROM Customer;";
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                _connection.Close(); 

            }


        }
    }
}