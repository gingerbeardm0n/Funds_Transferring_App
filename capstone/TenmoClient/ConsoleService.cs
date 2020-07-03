using System;
using System.Collections.Generic;
using TenmoClient.Data;




namespace TenmoClient
{
    public class ConsoleService
    {
        private static readonly AuthService authService = new AuthService();
        private static readonly AccountServiceAPI accountService = new AccountServiceAPI();

        public void Run()
        {

            while (true)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit");
                Console.Write("Please choose an option: ");

                int loginRegister = -1;

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out loginRegister))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }

                    else if (loginRegister == 1)
                    {
                        LoginUser loginUser = PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                            MenuSelection();
                        }
                    }

                    else if (loginRegister == 2)
                    {
                        LoginUser registerUser = PromptForLogin();
                        bool isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                        }
                    }

                    else if (loginRegister == 3)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }

                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error - " + ex.Message);
                }
            }
        }

        private void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers"); //view details through here
                //Console.WriteLine("3: View your pending requests"); //ability to approve/reject through here
                Console.WriteLine("4: Send TE bucks");
                //Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: View list of users");
                // Console.WriteLine("7: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                try
                {
                    if (!int.TryParse(Console.ReadLine(), out menuSelection))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    else if (menuSelection == 1)
                    {
                        decimal balance = accountService.GetMyBalance();
                        Console.WriteLine();
                        Console.WriteLine("Your current account balance is: $" + balance);
                        Console.ReadLine();

                    }
                    else if (menuSelection == 2)
                    {
                        Console.WriteLine();

                    }
                    else if (menuSelection == 3)
                    {

                    }
                    else if (menuSelection == 4)
                    {
                        int accountIDToIncrease = -1;
                        while(accountIDToIncrease == -1)
                        {
                            accountIDToIncrease = PromptForTransferID();
                        }
                        
                        Console.Write("Enter amount: $");
                        string inputTransferAmount = Console.ReadLine();
                        decimal transferAmount = decimal.Parse(inputTransferAmount);

                        TransferData transferData = new TransferData()
                        {
                            AccountIDToIncrease = accountIDToIncrease,
                            TransferAmount = transferAmount
                        };

                        TransferData transferDataFromServer = accountService.UpdateBalance(transferData);
                        if (transferDataFromServer == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\t Error has occured, balance has not been updated! Please review account details and try again.");
                        }
                        else
                        {
                            Console.WriteLine("\t Balance Updated!");
                        }
                        Console.WriteLine(transferDataFromServer);
                        
                    }
                    else if (menuSelection == 5)
                    {

                    }
                    else if (menuSelection == 6)
                    {

                    }
                    else if (menuSelection == 7)
                    {
                        Console.WriteLine("");
                        UserService.SetLogin(new API_User()); //wipe out previous login info
                        return; //return to register/login menu
                    }
                    else if (menuSelection == 0)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Please try again");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error - " + ex.Message);
                    Console.WriteLine();
                }
            }
        }
        
        public int PromptForTransferID()
        {
            PromptForIDHeaderAndDisplayAvailableUsers();

            Console.WriteLine("");
            Console.Write("Please enter userID of person you want to transfer TE bucks to (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int transferID))
            {
                Console.WriteLine("\t Invalid input. Only input a number.");
                return -1;
            }
            else
            {
                List<API_User> testList = accountService.GetUsers();
                if (transferID > testList.Count)
                {
                    Console.WriteLine("\t Please enter a valid userID!");
                    return -1;
                }
                return transferID;
            }
        }

        public void PromptForIDHeaderAndDisplayAvailableUsers()
        {
            
            List<API_User> userList = accountService.GetUsers();
            Console.WriteLine("Send TE Bucks");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID".PadRight(10) + "Name");
            Console.WriteLine("-------------------------------------------");
            
            foreach (API_User person in userList)
            {
                Console.WriteLine(person.UserId.ToString().PadRight(10) + person.Username);
            }
        }
        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }
        
    }
}
