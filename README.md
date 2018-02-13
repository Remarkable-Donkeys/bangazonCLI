#Bangazon CLI
The Bangazon CLI allows Bangazon Employees to access and update information in the database on behalf of the customers
## The following actions can be completed using the BANGAZON CLI:
1. Add a new customer
2. Select a current customer as active
3. For an active customer:
    1. Add a payment type
    2. Add a product to sell
    3. Update that product's information
    4. Delete a product if it isn't in another customer's order
    5. Add a product to their shopping cart
    6. Complete their order by adding a payment type
    7. View their revenue report if their products have sold
4. The user can view stale items
5. The user can view the 3 most popular items.

## System Configuration
1. Clone or download the repository `git clone git@github.com:Remarkable-Donkeys/bangazonCLI.git`
2. Configure an environment variable named `BANGAZONCLI`
3. Restore the project `dotnet restore`
4. To use the application, navigate to the src directory and `dotnet run`
5. To test the application, Configure an environment variable named `BANGAZONTEST`
6. Navigate to the test directory and `dotnet test`
