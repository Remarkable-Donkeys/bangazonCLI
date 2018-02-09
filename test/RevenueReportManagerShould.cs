/*author:   Sean Williams
purpose:    Revenue Report Unit Tests
Tests:    	
 */
 
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Xunit;
using bangazonCLI;

namespace bangazonCLI.Test
{
    public class RevenueReportManagerShould
    {
        private RevenueReportManager _manager;
        private Order _order;
        private DatabaseInterface db;

        public RevenueReportManagerShould()
        {
			db = new DatabaseInterface("BANGAZONTEST");
            _manager = new RevenueReportManager("BANGAZONTEST");
            _orders = OrderManager.GetSingleOrder(1);
        }

        [Fact]

        public void DisplayOrder()
        {

        }
    }
}
