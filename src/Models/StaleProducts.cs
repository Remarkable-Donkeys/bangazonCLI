using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace bangazonCLI
{
    public class StaleProducts
    {
            private DatabaseInterface _db;
            private ProductManager _pManager;
            private OrderManager _oManager;
            private Order _order;

        public StaleProducts(string databaseEnvironment){
            _db = new DatabaseInterface(databaseEnvironment);
            _pManager = new ProductManager(databaseEnvironment);
            _oManager = new OrderManager(databaseEnvironment);
            _order = new Order(databaseEnvironment);
        }

        public void Show(){
            Console.Clear();
            Console.WriteLine("To return to main menu enter 0");
            Console.WriteLine("*************************************************");
            Console.WriteLine("Stale Products");
            

            //if user presses 0 go back to main menu, if any other number, stay on stale item menu
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            int exit = int.Parse(enteredKey.KeyChar.ToString());
            if(exit == 0){
                return;
            } else {
                Show();
            }
        }

    }
}