using OrderLibrary;

namespace ChallangeProject;

class Program
{
    #region variables
    private static string? input;
    #endregion
    /// <summary>
    /// First input options to get to order placement, order list or to exit 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        do
        {
            Console.WriteLine($"{Environment.NewLine}To place an order for DNA testing kits press O and <Enter>!");
            Console.WriteLine("To see previous orders press A and <Enter>!");
            Console.WriteLine("To exit press any other key");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) break;
            if (input.ToUpper() == "O")
            {
                Console.Clear();
                Console.WriteLine($"{Environment.NewLine}Current DNA testing kit variant base price is 98.99 EUR. To change, press C and <Enter>.");
                Console.WriteLine($"To continue with this kit, press any other key and <Enter>.");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.ToUpper() == "C")
                    ChangeKit();
                else
                    PlaceAnOrder();
            }
            else if (input.ToUpper() == "A") ShowAllOrders();
            else Environment.Exit(0);
        } while (true);
    }
    /// <summary>
    /// If user wants to change kit base price(checks if double and not empty)
    /// </summary>
    private static void ChangeKit()
    {
        Console.Clear();
        do
        {
            Console.WriteLine($"{Environment.NewLine}Enter new base price for DNA testing kit and press <Enter>:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (Order.isDouble(input))
                    break;
            }
            else 
                Console.WriteLine($"{Environment.NewLine}Base price cannot be empty!");
        } while (true);
        Console.WriteLine($"Base price was succesfully changed! Let's continue to your new order!");
        PlaceAnOrder();
    }
    /// <summary>
    /// Method for placing new order - user enters customer ID (checks if not empty), order count (checks if is a positive number and in range 1-999), delivery date (checks if is in date format and if is in the future)
    /// </summary>
    private static void PlaceAnOrder()
    {
        Console.Clear();
        do
        {
            Console.WriteLine($"{Environment.NewLine}Enter customer ID and press <Enter>:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                Order.client_id = input;
                break;
            }
            else 
                Console.WriteLine($"{Environment.NewLine}Customer ID cannot be empty!");
        } while (true);
        do
        {
            Console.WriteLine($"{Environment.NewLine}Enter order count and press <Enter>:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (Order.isIntInRange(input))
                    break;
            }
            else
                Console.WriteLine($"{Environment.NewLine}Order count cannot be empty!");
        } while (true);
        do
        {
            Console.WriteLine($"{Environment.NewLine}Enter delivery date and press <Enter>:");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (Order.isDateInRange(input))
                    break;
            }
            else
                Console.WriteLine($"{Environment.NewLine}Delivery date cannot be empty!");
        } while (true);
        Console.Clear();
        Order.saveOrder();
        Console.WriteLine($"{Environment.NewLine}Your order is succesfully placed! {Environment.NewLine}");
        Console.WriteLine(Order.allOrders.Last());
    }
    /// <summary>
    /// Shows all orders or sorts by client ID. If there are no orders, shows options what to do next
    /// </summary>
    private static void ShowAllOrders()
    {
        Console.Clear();
        if (Order.allOrders.Count > 0)
        {
            Console.WriteLine("To show specific customer orders press S and <Enter>!");
            Console.WriteLine($"{Environment.NewLine}To show all orders press any key");
            input = Console.ReadLine();
            Console.WriteLine();
            if (!string.IsNullOrWhiteSpace(input) && input.ToUpper() == "S")
            {
                do
                {
                    Console.WriteLine("Enter customer ID to search and press <Enter>!");
                    input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (Order.allOrders.Any(w => w.Contains("Customer ID: " + input)))
                        {
                            foreach (var order in Order.allOrders.Where(s => Order.allOrders.Any(w => s.Contains("Customer ID: " + input))))
                            {
                                Console.WriteLine(order);
                                Console.WriteLine();
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("There are no orders for this customer!");
                            break;
                        }
                    }
                    else
                        Console.WriteLine("Customer ID cannot be empty!");
                }while(true);
            }
            else
            {
                foreach (var order in Order.allOrders)
                {
                    Console.WriteLine(order);
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("Currently there are no orders, would you like to place an order? Press O and <Enter>.");
            Console.WriteLine("To exit, press any other key.");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && input.ToUpper() == "O") PlaceAnOrder();
            else Environment.Exit(0);
        }
    }
}