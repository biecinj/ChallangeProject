namespace OrderLibrary
{
    public static class Order
    {
        /// <summary>
        /// Order identifier
        /// </summary>
        public static int order_id { get; set; }
        /// <summary>
        /// Client identifier
        /// </summary>
        public static string? client_id { get; set; }
        /// <summary>
        /// Order count
        /// </summary>
        public static int order_count { get; set; }
        /// <summary>
        /// Base price
        /// </summary>
        public static double base_price { get; set; }
        /// <summary>
        /// DNA testing kit variant identifier
        /// </summary>
        public static int kit_variant { get; set; }
        /// <summary>
        /// Order full price
        /// </summary>
        public static double full_price { get; set; }
        /// <summary>
        /// Order delivery date
        /// </summary>
        public static DateOnly delivery_date { get; set; }
        /// <summary>
        /// List where all orders are saved
        /// </summary>
        public static List<string> allOrders = new List<string>();
        static int variant = 1;

        /// <summary>
        /// checks if entered value is an int number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isInt(string input)
        {
            try
            {
                if (int.TryParse(input, out int n)) return true;
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Entered value was not a number!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// checks if entered value is a double
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isDouble(string input)
        {
            try
            {
                if (double.TryParse(input, out double n))
                {
                    kit_variant = variant + 1;
                    base_price = Math.Round(double.Parse(input), 2);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Entered value was not in correct format!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// checks if order count in range 1-999 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isIntInRange(string input)
        {
            try
            {
                if (isInt(input))
                {
                    if (int.Parse(input) > 0 && int.Parse(input) <= 999)
                    {
                        order_count = Int32.Parse(input);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"{Environment.NewLine}Order count has to be between 1 and 999!");
                        return false;
                    }
                }
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// checks if entered value is in any date format 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isDate(string input)
        {
            try
            {
                if (DateTime.TryParse(input, out DateTime d))
                    return true;
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Entered value was not a date!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// checks if date is in the future
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isDateInRange(string input)
        {
            try
            {
                if (isDate(input))
                {
                    if (DateTime.Parse(input) > DateTime.Today)
                    {
                        delivery_date = DateOnly.Parse(input);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"{Environment.NewLine}Delivery date has to be in the future!");
                        return false;
                    }
                }
                else return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// calculates order full price
        /// </summary>
        /// <param name="price"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static double calculateFullPrice(double price, double count)
        {
            double fullPrice = price * count;

            if (count >= 10 && count < 50)
                fullPrice = fullPrice - (fullPrice * 5 / 100);

            else if (count >= 50)
                fullPrice = fullPrice - (fullPrice * 15 / 100);

            return Math.Round(fullPrice, 2);
        }
        /// <summary>
        /// saves order to list
        /// </summary>
        /// <returns></returns>
        public static List<string> saveOrder()
        {
            order_id += 1;
            if (base_price == 0)
            {
                base_price = 99.98;
                kit_variant = 1;
            }
            full_price = calculateFullPrice(base_price, order_count);
            if (isDateInRange(delivery_date.ToShortDateString()) && isIntInRange(order_count.ToString()))
            {
                allOrders.Add("Order ID: " + order_id +
                            " | Customer ID: " + client_id +
                            " | DNA testing kit " + kit_variant +
                            " | Base price: " + base_price +
                            " | Order count: " + order_count +
                            " | Full price: " + full_price +
                            " | Delivery date: " + delivery_date);
            }
                return allOrders;
            
        }
    }
}