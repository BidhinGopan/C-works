using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BankATMSoft
{
    public class Customer_details
    {
        public string Full_Name;
        public string Address;
        public string phone;
        public string Email;
        public string Aadhaar;
        public string account_type;
        public ulong account_number;
        public decimal balance;
        private string pin;
        public Customer_details(string name, string Addr, string ph, string email, string aadhaar,string acc_type ,ulong acc_no, decimal bal)
        {
            Full_Name = name;
            Address = Addr;
            phone = ph;
            Email = email;
            Aadhaar = aadhaar;
            account_type = acc_type;
            account_number = acc_no;
            balance = bal;
        }
        public void Setpin(string newpin)
        {
            pin = newpin;
        }
        public string Getpin()
        {
            return pin;
        }


    }
    class Program
    {
        static ulong accountnumber; //To generate account Number
        static List<Customer_details> customers_List = new List<Customer_details>();//A list to objects of class Customer_details
        //Validations
        static string Valid_Name()
        {
            while (true)
            {
                string name;
                Console.WriteLine("Enter Customer Name: ");
                name = Console.ReadLine();
                if (name.Length > 30)
                    Console.WriteLine("Name must be less than 30 characters long. Try again.");
                else
                {
                    return name;
                }
            }
        }
        static string Valid_Phone()
        {
            while (true)
            {
                string phoneNumber;
                Console.WriteLine("Enter Customer Phone Number: ");
                phoneNumber = Console.ReadLine();
                Regex phone_pattern = new Regex(@"^\d{10}$");
                if (!phone_pattern.IsMatch(phoneNumber))
                    Console.WriteLine("Phone number must contain only 10 digits. Try again");
                else
                    return phoneNumber;

            }
        }
        static string Valid_accno()
        {
            while (true)
            {
                string accountNumber;
                Console.WriteLine("Enter the Account Number: ");
                accountNumber = Console.ReadLine();
                Regex account_pattern = new Regex(@"^\d{9}$");
                if (!account_pattern.IsMatch(accountNumber) || accountNumber.Length != 9)
                    Console.WriteLine("Phone number must contain only 10 digits. Try again");
                else
                    return accountNumber;

            }
        }
        static string Valid_amount()
        {
            while (true)
            {
                string amount_check;
                Console.WriteLine("Enter the Amount: ");
                amount_check = Console.ReadLine();
                Regex bal_pattern = new Regex("[0-9]");
                if (!bal_pattern.IsMatch(amount_check))
                    Console.WriteLine("Account Balance must be entered only on digits. Try again");
                else
                    return amount_check;
            }
        }

        //Admin Module
        static void Admin()
        {
            int flag = 0;
            int attempt = 1;
            while (flag == 0)
            {
                if (attempt == 3)
                {
                    Console.WriteLine("You have done 3 attempts. Login Failed.");
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter your admin pin: ");
                    string adminPin = Console.ReadLine();
                    if (adminPin == "1234")//Admin pin set to 1234
                    {
                        flag = 1;
                        int flag1 = 0;
                        while (flag1 == 0)
                        {
                            Console.WriteLine("Welcome to Admin Dashboard");
                            Console.WriteLine("Please select an option");
                            Console.WriteLine("1. Add New Customer");
                            Console.WriteLine("2. View All Customer Details");
                            Console.WriteLine("3. Search a Customer ");
                            Console.WriteLine("4. Logout");

                            string adminInput = Console.ReadLine();

                            switch (adminInput)
                            {
                                case "1":
                                    AddCustomer();
                                    break;
                                case "2":
                                    ViewCustomerforadmin();
                                    break;
                                case "3":
                                    Searchacustomeroptions();
                                    break;
                                case "4":
                                    return;
                                default:
                                    Console.WriteLine("Invalid input. Please try again");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Attempt {attempt} failed. {3 - attempt} attempts left");
                        attempt++;
                    }
                }
            }
            return;
        }
        static void AddCustomer()
        {
            string name;
            string phoneNumber;
            decimal accountBalance;
            name=Valid_Name();
            Console.WriteLine("Enter Customer Address: ");
            string address = Console.ReadLine();
            phoneNumber = Valid_Phone();
            Console.WriteLine("Enter Customer Email-ID: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Customer's 12-digit Aadhaar Number: ");
            string aadhaarNumber = Console.ReadLine();
            string account_type;
            while (true)
            {
                Console.WriteLine("SELECT ACCOUNT TYPE: ");
                Console.WriteLine("1.Savings   2. Current");
                Console.WriteLine("Enter an option(1-2):");
                string admin_Input = Console.ReadLine();
                if (admin_Input == "1")
                {
                    account_type = "Savings";
                    break;
                }
                else if (admin_Input == "2")
                {
                    account_type = "Current";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Try Again.");
                }
            }
            
            accountBalance = Convert.ToDecimal(Valid_amount());
            accountnumber++;
            Customer_details new_customer=new Customer_details(name, address, phoneNumber, email, aadhaarNumber,account_type,accountnumber, accountBalance);
            Console.WriteLine("Enter Customer's 4-digit Pin number:");
            new_customer.Setpin(Console.ReadLine());
            customers_List.Add(new_customer);
            Console.WriteLine("Customer Added and account created Successfully!!!");
            return;
        }

        static void ViewCustomerforadmin()
        {
            if (customers_List.Count== 0)
            {
                Console.WriteLine("No customer available");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine($"The bank has {customers_List.Count}customers");
                Console.WriteLine("Customers Detail:");
                foreach(Customer_details customer in customers_List)
                {
                    Console.WriteLine($"Full Name: {customer.Full_Name}");
                    Console.WriteLine($"Address: {customer.Address}");
                    Console.WriteLine($"Registered Phone: {customer.phone}");
                    Console.WriteLine($"Registered Email ID: {customer.Email}");
                    Console.WriteLine($"Aadhaar Number: {customer.Aadhaar}");
                    Console.WriteLine($"Account Number: {customer.account_number}");
                    Console.WriteLine($"Account Type: {customer.account_type}");
                    Console.WriteLine($"Account Balance: {customer.balance}");
                    Console.WriteLine();
                }
                
            }
            Console.ReadKey();
            return;
        }
        static void Searchacustomeroptions()
        {
            while (true)
            {
                Console.WriteLine("Search Options:");
                Console.WriteLine("1. Account Number(with update and delete options");
                Console.WriteLine("2. Full Name");
                Console.WriteLine("3. Phone Number");
                Console.WriteLine("4. Aadhaar Number");
                Console.WriteLine("5. Go to Admin Dashboard");
                string adminInput = Console.ReadLine();
                switch (adminInput)
                {
                    case "1":
                        SearchCustomerbyACCnoforAdmin();
                        break;
                    case "2":
                        SearchCustomerbyFullName();
                        break;
                    case "3":
                        SearchCustomerbyPhone();
                        break;
                    case "4":
                        SearchCustomerbyAadhar();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try Again");
                        break;
                }
            }
        }
        static void SearchCustomerbyACCnoforAdmin()
        {
            ulong accno = Convert.ToUInt64(Valid_accno());
            int flag = 0;
            foreach (Customer_details customer in customers_List)

            {
                if (customer.account_number == accno)
                {
                    flag = 1;
                    Console.WriteLine("Customer Found!!!");
                    Console.WriteLine();
                    while (true)
                    {
                        Console.WriteLine("Action Menu:");
                        Console.WriteLine("1. View Customer Details:");
                        Console.WriteLine("2. Update Customer Detail(s)");
                        Console.WriteLine("3. Delete Customer Record");
                        Console.WriteLine("4. Go Back to Search Options");
                        string adminInput = Console.ReadLine();
                        switch (adminInput)
                        {
                            case "1":
                                Console.WriteLine("Customer Details");
                                Console.WriteLine($"Full Name: {customer.Full_Name}");
                                Console.WriteLine($"Address: {customer.Address}");
                                Console.WriteLine($"Registered Phone Number: {customer.phone}");
                                Console.WriteLine($"Registered Email ID: {customer.Email}");
                                Console.WriteLine($"Aadhaar Number: {customer.Aadhaar}");
                                Console.WriteLine($"Account Number: {customer.account_number}");
                                Console.WriteLine($"Account Type: {customer.account_type}");
                                Console.WriteLine($" Account Balance: {customer.balance}");
                                while (true)
                                {
                                    Console.WriteLine("Do you want to view pin of the Customer?");
                                    Console.WriteLine("1.YES   2.NO");
                                    Console.WriteLine("Enter an option(1-2):");
                                    adminInput = Console.ReadLine();
                                    if (adminInput == "1")
                                    {
                                        Console.WriteLine($"Pin Number: {customer.Getpin()}");
                                        Console.ReadLine();
                                        break;
                                    }
                                    else if (adminInput == "2")
                                        break;
                                    else
                                        Console.WriteLine("Invalid Input Try Again");
                                }
                                break;
                            case "2":
                                //Edit Customer
                                int flag3 = 0;
                                while (flag3 == 0)
                                {
                                    Console.WriteLine("Choose an option: ");
                                    Console.WriteLine("1. Update Full name");
                                    Console.WriteLine("2. Update Address");
                                    Console.WriteLine("3. Update Registered Phone Number");
                                    Console.WriteLine("4. Update Registered email-ID");
                                    Console.WriteLine("5. Update Pin number");
                                    Console.WriteLine("6. Go Back to Action Menu ");
                                    adminInput = Console.ReadLine();
                                    switch (adminInput)
                                    {
                                        case "1":
                                            //edit full name
                                            customer.Full_Name = Valid_Name();
                                            Console.WriteLine("Name Updated Successfully");
                                            break;
                                        case "2":
                                            //edit address
                                            Console.WriteLine("Enter New Address: ");
                                            customer.Address = Console.ReadLine();
                                            Console.WriteLine("Address Updated Successfully");
                                            break;
                                        case "3":
                                            //edit phone
                                            Console.WriteLine("Enter New Phone Number: ");
                                            customer.phone = Valid_Phone();
                                            Console.WriteLine("Phone Number Updated Successfully");
                                            break;
                                        case "4":
                                            //edit email id
                                            Console.WriteLine("Enter New Email ID: ");
                                            customer.Email = Console.ReadLine();
                                            Console.WriteLine("Email ID Updated Successfully");
                                            break;
                                        case "5":
                                            //edit pin
                                            int flag4 = 0;
                                            while (flag4 == 0)
                                            {
                                                Console.WriteLine("Do you really want to change pin number?");
                                                Console.WriteLine("1. YES   2. NO");
                                                Console.WriteLine("Enter an option(1-2):");
                                                adminInput = Console.ReadLine();
                                                if (adminInput == "1")
                                                {
                                                    Console.WriteLine("Enter New pin Number:");
                                                    customer.Setpin(Console.ReadLine());
                                                    Console.WriteLine("Pin Number Updated successfully");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                                else if (adminInput == "2")
                                                    break;
                                                else
                                                    Console.WriteLine("Invalid Input Try Again");
                                            }
                                            break;
                                        case "6":
                                            //go back
                                            flag3 = 1;
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Input Try again");
                                            break;
                                    }
                                }
                                break;
                            case "3":
                                int flag5 = 0;
                                while (flag5 == 0)
                                {
                                    Console.WriteLine("Do you really want to change delete the record?");
                                    Console.WriteLine("1. YES   2. NO");
                                    Console.WriteLine("Enter an option(1-2):");
                                    adminInput = Console.ReadLine();
                                    if (adminInput == "1")
                                    {
                                        customers_List.Remove(customer);
                                        Console.WriteLine("Customer Record Deleted");
                                        Console.ReadLine();
                                        break;
                                    }
                                    else if (adminInput == "2")
                                        break;
                                    else
                                        Console.WriteLine("Invalid Input Try Again");
                                }
                                customers_List.Remove(customer);
                                Console.WriteLine("Customer Record Deleted");
                                break;
                            case "4":
                                return;
                            default:
                                Console.WriteLine("Invalid option try again");
                                break;
                        }
                    }


                }
            }
            if(flag==0)
            {
                Console.WriteLine("Customer Not Found");
                Console.ReadLine();
                return;
            }
        }
        static void SearchCustomerbyFullName()
        {
            string name = Valid_Name();
            int flag = 0;
            foreach (Customer_details customer in customers_List)
            {
                if (customer.Full_Name == name)
                {
                    flag = 1;
                    Console.WriteLine("Customer Found!!!");
                    Console.WriteLine("Customer Details");
                    Console.WriteLine($"Full Name: {customer.Full_Name}");
                    Console.WriteLine($"Address: {customer.Address}");
                    Console.WriteLine($"Registered Phone Number: {customer.phone}");
                    Console.WriteLine($"Registered Email ID: {customer.Email}");
                    Console.WriteLine($"Aadhaar Number: {customer.Aadhaar}");
                    Console.WriteLine($"Account Number: {customer.account_number}");
                    Console.WriteLine($"Account Type: {customer.account_type}");
                    Console.WriteLine($" Account Balance: {customer.balance}");
                }
            }
            if (flag == 0)
            {
                Console.WriteLine("Customer Not Found");
                Console.ReadLine();
                return;
            }
        }
        static void SearchCustomerbyPhone()
        {
            string phone = Valid_Phone();
            int flag = 0;
            foreach (Customer_details customer in customers_List)
            {
                if (customer.phone == phone)
                {
                    flag = 1;
                    Console.WriteLine("Customer Found!!!");
                    Console.WriteLine("Customer Details");
                    Console.WriteLine($"Full Name: {customer.Full_Name}");
                    Console.WriteLine($"Address: {customer.Address}");
                    Console.WriteLine($"Registered Phone Number: {customer.phone}");
                    Console.WriteLine($"Registered Email ID: {customer.Email}");
                    Console.WriteLine($"Aadhaar Number: {customer.Aadhaar}");
                    Console.WriteLine($"Account Number: {customer.account_number}");
                    Console.WriteLine($"Account Type: {customer.account_type}");
                    Console.WriteLine($" Account Balance: {customer.balance}");
                }
            }
            if (flag == 0)
            {
                Console.WriteLine("This phone number is not registered with any account");
                Console.ReadLine();
                return;
            }
        }
        static void SearchCustomerbyAadhar()
        {
            Console.WriteLine("Enter the Aadhar Number : ");
            string aadhar = Console.ReadLine();
            int flag = 0;
            foreach (Customer_details customer in customers_List)
            {
                if (customer.Aadhaar == aadhar)
                {
                    flag = 1;
                    Console.WriteLine("Customer Found!!!");
                    Console.WriteLine("Customer Details");
                    Console.WriteLine($"Full Name: {customer.Full_Name}");
                    Console.WriteLine($"Address: {customer.Address}");
                    Console.WriteLine($"Registered Phone Number: {customer.phone}");
                    Console.WriteLine($"Registered Email ID: {customer.Email}");
                    Console.WriteLine($"Aadhaar Number: {customer.Aadhaar}");
                    Console.WriteLine($"Account Number: {customer.account_number}");
                    Console.WriteLine($"Account Number: {customer.account_type}");
                    Console.WriteLine($" Account Balance: {customer.balance}");
                }
            }
            if (flag == 0)
            {
                Console.WriteLine("Customer not Found");
                Console.ReadLine();
                return;
            }
        }



        //Customer Module
        static void Customer()
        {
            int flag = 0;
            int attempt = 1;
            while (flag == 0)
            {
                if (attempt == 3)
                {
                    Console.WriteLine("You have done 3 attempts. Login Failed.");
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter your 4-digit pin: ");
                    string customerPin = Console.ReadLine();
                    int flag2 = 0;
                    ulong acc_number = 0;
                    string name = "";
                    foreach (Customer_details customer in customers_List)
                    {
                        if (customer.Getpin() == customerPin)
                        {
                            flag2 = 1;
                            acc_number = customer.account_number;
                            name = customer.Full_Name;
                        }
                    }
                    if (flag2 == 1)
                    {                        
                        Console.WriteLine($"Welcome to Customer Dashboard {name}");
                        Console.WriteLine("Please select an option");
                        Console.WriteLine("1. View Balance");
                        Console.WriteLine("2. Deposit Money");
                        Console.WriteLine("3. Withdraw Money");
                        Console.WriteLine("4. Transaction by Bank Transfer");
                        Console.WriteLine("5. Logout");

                        string customerInput = Console.ReadLine();

                        switch (customerInput)
                        {
                            case "1":
                                ViewBalance_Customer(acc_number);
                                break;
                            case "2":
                                // TODO: Deposit money
                                Cust_Deposit(acc_number);
                                break;
                            case "3":
                                // TODO: Withdraw money
                                Cust_Withdraw(acc_number);
                                break;
                            case "4":
                                Cust_Transfer(acc_number);
                                break;
                            case "5":
                                return;
                            default:
                                Console.WriteLine("Invalid input. Please try again");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Attempt {attempt} failed. {3-attempt} attempts left");
                        attempt++;
                    }
                }
            }
        }

        static void ViewBalance_Customer(ulong acc_numb)
        {
            
            foreach (Customer_details customer in customers_List)
            {
                if (acc_numb == customer.account_number)
                {
                    Console.WriteLine($"Account Balance= Rs{customer.balance}");
                    Console.WriteLine("Logging out...");
                }
            }
            return;
        }
        static void Cust_Deposit(ulong acc_numb)
        {
            foreach (Customer_details customer in customers_List)
            {
                if (acc_numb == customer.account_number)
                {
                    decimal deposit_amount = Convert.ToDecimal(Valid_amount());
                    customer.balance += deposit_amount;
                    Console.WriteLine($"Rs {deposit_amount} Deposited ");
                    Console.WriteLine($"Account Balance: Rs{customer.balance}");
                }
            }
            return;
        }
        static void Cust_Withdraw(ulong acc_numb)
        {
            while (true)
            {
                Console.WriteLine("Select account Type:");
                Console.WriteLine("1. Savings   2.Current");
                string customer_input = Console.ReadLine();
                if (customer_input == "1")
                {
                    foreach (Customer_details customer in customers_List)
                    {
                        if (acc_numb == customer.account_number && customer.account_type == "Savings")
                        {
                            decimal withdraw_amount = Convert.ToDecimal(Valid_amount());
                            if (customer.balance - withdraw_amount < 0)
                            {
                                Console.WriteLine("Insufficient Balance");
                                Console.WriteLine("Logging out...");
                                Main();
                            }
                            else
                            {
                                customer.balance -= withdraw_amount;
                                Console.WriteLine($"Collect the Cash Rs{withdraw_amount}");
                                Console.WriteLine($"Account Balance: Rs{customer.balance}");
                                Main();
                            }
                        }
                    }
                    break;
                }
                else if (customer_input == "2")
                {
                    foreach (Customer_details customer in customers_List)
                    {
                        if (acc_numb == customer.account_number && customer.account_type == "Current")
                        {
                            decimal withdraw_amount = Convert.ToDecimal(Valid_amount());
                            if (customer.balance - withdraw_amount < -5000)
                            {
                                Console.WriteLine("Exceeding overdraft Limit");
                                Console.WriteLine("Logging out...");
                                Main();
                            }
                            else
                            {
                                customer.balance -= withdraw_amount;
                                Console.WriteLine($"Collect the Cash Rs{withdraw_amount}");
                                Console.WriteLine($"Account Balance: Rs{customer.balance}");
                                Main();
                            }
                        }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Try Again");
                }
            }
            return;
        }
        static void Cust_Transfer(ulong acc_numb)
        {
            int flag = 0;
            decimal transfer_amount;
            while (flag==0)
            {
                Console.WriteLine("Beneficiary Detail:");
                ulong ben_acc_numb = Convert.ToUInt64(Valid_accno());
                foreach (Customer_details beneficiary in customers_List)
                {
                    if (ben_acc_numb == beneficiary.account_number)
                    {
                        flag = 1;
                        while (true)
                        {
                            Console.WriteLine("Select account Type:");
                            Console.WriteLine("1. Savings   2.Current");
                            string customer_input = Console.ReadLine();
                            if (customer_input == "1")
                            {
                                foreach (Customer_details customer in customers_List)
                                {
                                    if (acc_numb == customer.account_number && customer.account_type == "Savings")
                                    {
                                        transfer_amount = Convert.ToDecimal(Valid_amount());
                                        if (customer.balance - transfer_amount < 0)
                                        {
                                            Console.WriteLine("Insufficient Balance");
                                            Console.WriteLine("Logging out...");
                                            Main();
                                        }
                                        else
                                        {
                                            customer.balance -= transfer_amount;
                                            beneficiary.balance += transfer_amount;
                                            Console.WriteLine($"Bank Transfer of Rs {transfer_amount} transfered  successfully to A/C No:{ben_acc_numb}");
                                            Console.WriteLine($"Account Balance: Rs{customer.balance}");
                                            Main();
                                        }
                                    }
                                }
                                break;
                            }
                            else if (customer_input == "2")
                            {
                                foreach (Customer_details customer in customers_List)
                                {
                                    if (acc_numb == customer.account_number && customer.account_type == "Current")
                                    {
                                        transfer_amount = Convert.ToDecimal(Valid_amount());
                                        if (customer.balance - transfer_amount < -5000)
                                        {
                                            Console.WriteLine("Exceeding overdraft Limit");
                                            Console.WriteLine("Logging out...");
                                            Main();
                                        }
                                        else
                                        {
                                            customer.balance -= transfer_amount;
                                            beneficiary.balance += transfer_amount;
                                            Console.WriteLine($"Bank Transfer of Rs {transfer_amount} transfered  successfully to A/C No:{ben_acc_numb}");
                                            Console.WriteLine($"Account Balance: Rs{customer.balance}");
                                            Main();
                                        }
                                    }
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input. Try Again");
                            }
                        }
                    }
                }
                if (flag == 0)
                {
                    Console.WriteLine("Beneficiary not found.Try Again");
                }
            }

            
            return;
        }


        //MAIN SECTION
        static void Main()
        {
            accountnumber = 99999999;
            int flag=0;
            Console.WriteLine("Welcome to Bank ATM Soft");
            while (flag == 0)
            {
                Console.WriteLine("Main DashBoard:");
                Console.WriteLine("Please select an option");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        Admin();
                        break;
                    case "2":
                        Customer();
                        break;
                    case "3":
                        flag = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again");
                        break;
                }
            }
            Console.WriteLine("Exited BankATM Soft");
            Console.ReadLine();
        }
    }
}
