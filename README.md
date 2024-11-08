# Demo - Grocey Store Application
## Special Thank, without your helps, this demo project is not successed:
- Peyton Comer
- Lauren Mayo
- Eldin Salja
- Jacob Walter
- Luke Walsdorf

## How to run the Project:
### Database Setup
- Open up Microsoft Sequel Server and create a new database
- Run the file Updated Database 361.sql
- Create an environment variable in your computer nammed "Conn" with the connection string that will connect to the database
- Example connection string Server=NAME_OF_DESKTOP; Database=NAME_OF_DATABASE; Trusted_Connection=True;
- Restart your computer
### .sln Setup
- Click into the solution and ensure the it is running before running the frontend.
### Frontend Setup
- After running the solution in Visual Studio, click into the Developer Powershell
- cd into the GroceryStoreFrontend folder, then cd into the groceryapp
- Run npm install, npm install react-icons
- After the npm installs complete, run npm start, it'll open the frontend application automatically

## Project requirements:
We will create an Online Shopping App. There are the requirement below:

For users:
-	Can login -> Need password, and username.
-	Can add products to their cart.
-	Can view products by categories.
-	When users log out, their cart is persisting -> Users’ database on cart don’t drop.

For securities:
-	The information and logic must be handled correctly and securely. -> Secure in password, username -> Privacy for users -> Rule to define who can access the database -> Using Interfaces. 
-	Users should not be able to see other users’ information or carts.

For design:
-	The back-end must provide product information to the front-end and handle business logic.
-	The database will provide accurate information about products, sales, and users.

For database:
-	Products: 
    - name, 
    - product images,
    - manufacturer information, 
    - description, 
    - dimensions and weight,
    - a product rating,
    - SKU (Stock-Keeping Units)

-	Users: <Self-design, can use these variables:>
    - user code/ user name,
    - first and last name,
    - email,
    - phone number,
    - address,
    - payment information.
 
## Unused Features
- Address and payment method tables in the database and middlend that don't do anything
- Sales/coupons for categories are outlined but they are not functional in the project
- Invoices are not functional but also outlined in the database


