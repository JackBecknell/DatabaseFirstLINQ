﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            ProblemOne();
            ProblemTwo();
            ProblemThree();
            ProblemFour();
            ProblemFive();
            ProblemSix();
            ProblemSeven();
            ProblemEight();
            ProblemNine();
            ProblemTen();
            //Completed V
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            ////ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            //Bonus three is unfinished.
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            int userNum = _context.Users.ToList().Count;
            Console.WriteLine(userNum);
            // HINT: .ToList().Count
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var expensiveProducts = _context.Products.Where(p => p.Price > 150);

            foreach (Product product in expensiveProducts)
            {
                Console.WriteLine(product.Name + " $" + product.Price);
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var sProducts = _context.Products.Where(p => p.Name.Contains("s"));

            foreach (Product product in sProducts)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016)
            DateTime d1 = new DateTime(2016, 01, 01);
            var earlyUsers = _context.Users.Where(u => u.RegistrationDate < d1);

            foreach (User user in earlyUsers)
            {
                Console.WriteLine("Early Users:" + user.Email + ' ' + user.RegistrationDate);
            }
            // Then print each user's email and registration date to the console.

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            DateTime d1 = new DateTime(2016, 01, 01);
            DateTime d2 = new DateTime(2018, 01, 01);
            var lateUsers = _context.Users.Where(u => u.RegistrationDate > d1 && u.RegistrationDate < d2);

            foreach (User user in lateUsers)
            {
                Console.WriteLine("Late Users:" + user.Email + ' ' + user.RegistrationDate);
            }
            // Then print each user's email and registration date to the console.
        }

        //// <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            //Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            var Afton = _context.ShoppingCarts.Include(u => u.Product).Include(u => u.User).Where(u => u.User.Email == "afton@gmail.com");

            foreach (ShoppingCart item in Afton)
            {
                Console.WriteLine($"Product: {item.Product.Name} Price: ${item.Product.Price} Quantity: {item.Quantity}");
            }
        }

        private void ProblemNine()
        {
            //    // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            //    // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            //    // Then print the total of the shopping cart to the console.
            var OdasTotal = _context.ShoppingCarts.Include(u => u.Product).Include(u => u.User).Where(u => u.User.Email == "oda@gmail.com").Select(u => u.Product.Price).Sum();

            Console.WriteLine("Oda is spending ${0}", OdasTotal);
        }

        private void ProblemTen()
        {
            //    // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            //    // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employeeId = _context.Roles.Include(u => u.UserRoles).Where(u => u.RoleName == "Employee").Select(u => u.Id).First();
            var usersWithEmpId = _context.UserRoles.Include(u => u.User).Where(u => u.RoleId == employeeId).Select(u => u.User.Email);
            var empShoppingCarts = _context.ShoppingCarts.Include(u => u.Product).Include(u => u.User).Where(u => usersWithEmpId.Contains(u.User.Email));
            foreach (ShoppingCart cart in empShoppingCarts)
            {
                Console.WriteLine("Employee email: {0}, Product Name: {1}, Product Price: ${2}, Quantity: {3}", cart.User.Email, cart.Product.Name, cart.Product.Price, cart.Quantity);
            }
        }

        //// <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        //// <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Game Controller",
                Description = "The biggest, the best, the shiniest, the awesomestest controller of ALL TIME!",
                Price = 1000
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }//shhhh

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Game Controller").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 5
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        //// <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Game Controller").SingleOrDefault();
            product.Price = 999;

            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        //// <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        //// <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.Write("Enter User Email: ");
            string userEmail = Console.ReadLine();
            Console.Write("Enter User Password: ");
            string userPassword = Console.ReadLine();
            var user = _context.Users.Where(u => u.Email == userEmail).Where(u => u.Password == userPassword).SingleOrDefault();
            if (user != null)
            {
                Console.WriteLine("Signed In!");
            }
            else
            {
                Console.WriteLine("Invalid Email or Password.");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.

            List<string> instructors = new List<string>();
            instructors.Add("Mike");
            instructors.Add("Nevin");
            instructors.Add("Cash");
            instructors.Add("JJ");


            var users = _context.Users.ToList();
            int userTotal = 0;
            int finalTotal = 0;

            foreach (User user in users)
            {
                userTotal = 0;
                var scTotal = _context.ShoppingCarts.Include(u => u.Product).Include(u => u.User).Where(u => u.UserId == user.Id);
                foreach (ShoppingCart cart in scTotal)
                {
                    userTotal += (int)cart.Product.Price * (int)cart.Quantity;
                    finalTotal += (int)cart.Product.Price * (int)cart.Quantity;
                }
                Console.WriteLine($"Email: {user.Email} | Total: {userTotal}");
            }
            Console.WriteLine($"Final total: {finalTotal}");
        }

        //// BIG ONE
        /////afton@gmail.com
        //AftonsPass123
        // 1. Create functionality for a user to sign in via the console
        // 2. If the user succesfully signs in
        //Completed^^^


        // a. Give them a menu where they perform the following actions within the console
        // View the products in their shopping cart
        // View all products in the Products table
        // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
        // Remove a product from their shopping cart
        // 3. If the user does not succesfully sing in
        // a. Display "Invalid Email or Password"
        // b. Re-prompt the user for credentials
        private void BonusThree()
        {
            //User sign in
            var userContinues = SignIn();
            if (userContinues.ToList().Count() == 2)
            {
                Console.WriteLine("signed in");
                //more function calls
            }



            
                //Console.WriteLine("Hello user please choose one of the following commands to continue.\n" +
                //    "Type '1' : To View all products in your shopping cart.\n" +
                //    "Type '2' : To View all products available for purchase.\n" +
                //    "Type '3' : To Exit.");
                //string userResponse1 = Console.ReadLine();

                //switch (userResponse1)
                //{
                //    case "1":
                //        break;
                //    case "2":
                //        break;
                //    case "3":
                //        break;
                //    default:
                //        break;
            

        }

        private static string SignIn()
        {
            string userEmail = "";
            string userPassword = "";
            bool userSignedIn = false;
            while (userSignedIn == false)
            {
                Console.Write("Enter User Email: ");
                userEmail = Console.ReadLine();
                Console.Write("Enter User Password: ");
                userPassword = Console.ReadLine();
                var user = _context.Users.Where(u => u.Email == userEmail).Where(u => u.Password == userPassword).SingleOrDefault();
                if (user != null)
                {
                    List<string> returnValue = new List<string>();
                    returnValue.Add(userEmail);
                    returnValue.Add(userPassword);
                    return "true";
                }
                else
                {
                    bool correctInput = false;
                    while (correctInput == false)
                    {
                        Console.WriteLine("Invalid Email or Password. Re-enter (Y/N): ");
                        string retry = Console.ReadLine().ToLower();
                        if (retry == "y" || retry == "yes")
                        {
                            correctInput = true;
                        }
                        else if (retry == "n" || retry == "no")
                        {
                            return "false";
                        }
                        else
                        {
                            Console.WriteLine("Please check your spelling and try again.");
                        }
                    }
                }
            }
            return null;
        }
    }
}



