﻿using System;

namespace bangazonCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInterface db = new DatabaseInterface();
            
            CustomerManager cManager = new CustomerManager();

            db.CheckDatabase();
        }
    }
}
