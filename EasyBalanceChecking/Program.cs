using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyBalanceChecking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Balance("1000.00!=\n125 Market !=:125.45\n126 Hardware =34.95\n127 Video! 7.45\n128 Book   :14.32\n129 Gasoline ::16.10"));
            Console.ReadKey();
        }

        public static string Balance(string book)
        {
            string[][] mas = book.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(s => new Regex(@"\s+").Replace(new Regex(@"[^a-zA-Z0-9 .]").Replace(s, ""), " ").Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

            double balance = double.Parse(mas[0][0]);
            double expense = 0;

            var mas2 = mas.Skip(1).Select(s => new { number = Convert.ToInt32(s[0]), name = s[1], price = double.Parse(s[2]) }).ToArray();

            string res = $"Original Balance: {mas[0][0]}\n";

            for (int i = 0; i < mas2.Length; i++)
            {
                balance -= mas2[i].price;
                expense += mas2[i].price;

                res += $"{mas2[i].number.ToString("000")} {mas2[i].name} {mas2[i].price.ToString("0.00")} Balance {Math.Round(balance, 2).ToString("0.00")}\n";
            }

            Console.WriteLine("Average expense: " + expense / mas2.Length);
            res += $"Total expense  {expense.ToString("0.00")}\nAverage expense  {Math.Round(expense / mas2.Length, 2).ToString("0.00")}";

            return res;
        }
    }
}
