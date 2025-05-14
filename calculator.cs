using System;
using System.Collections.Generic;

namespace ScientificCalculator
{
    class Calculator
    {
        private List<string> history = new List<string>();
        private double memory;

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Scientific Calculator");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Add\t\t2. Subtract\n3. Multiply\t4. Divide");
                Console.WriteLine("5. Power\t6. Square Root\n7. Sin\t\t8. Cos");
                Console.WriteLine("9. Tan\t\t10. Log\n11. Memory Store\t12. Memory Recall");
                Console.WriteLine("13. History\t14. Clear\n15. Exit");
                Console.WriteLine("----------------------");
                Console.Write("Choose operation: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ShowError("Invalid input!");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:  Add(); break;
                        case 2:  Subtract(); break;
                        case 3:  Multiply(); break;
                        case 4:  Divide(); break;
                        case 5:  Power(); break;
                        case 6:  SquareRoot(); break;
                        case 7:  Sin(); break;
                        case 8:  Cos(); break;
                        case 9:  Tan(); break;
                        case 10: Log(); break;
                        case 11: MemoryStore(); break;
                        case 12: MemoryRecall(); break;
                        case 13: ShowHistory(); break;
                        case 14: Clear(); break;
                        case 15: return;
                        default: ShowError("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private double GetNumber(string prompt)
        {
            Console.Write(prompt);
            if (!double.TryParse(Console.ReadLine(), out double num))
                throw new ArgumentException("Invalid number input");
            return num;
        }

        private void AddToHistory(string entry)
        {
            history.Add($"{DateTime.Now:HH:mm:ss} - {entry}");
            if (history.Count > 10) history.RemoveAt(0);
        }

        private void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Calculator Operations
        private void Add()
        {
            double a = GetNumber("Enter first number: ");
            double b = GetNumber("Enter second number: ");
            double result = a + b;
            Console.WriteLine($"Result: {result}");
            AddToHistory($"{a} + {b} = {result}");
        }

        private void Subtract()
        {
            double a = GetNumber("Enter first number: ");
            double b = GetNumber("Enter second number: ");
            double result = a - b;
            Console.WriteLine($"Result: {result}");
            AddToHistory($"{a} - {b} = {result}");
        }

        private void Multiply()
        {
            double a = GetNumber("Enter first number: ");
            double b = GetNumber("Enter second number: ");
            double result = a * b;
            Console.WriteLine($"Result: {result}");
            AddToHistory($"{a} × {b} = {result}");
        }

        private void Divide()
        {
            double a = GetNumber("Enter first number: ");
            double b = GetNumber("Enter second number: ");
            if (b == 0) throw new DivideByZeroException();
            double result = a / b;
            Console.WriteLine($"Result: {result}");
            AddToHistory($"{a} ÷ {b} = {result}");
        }

        private void Power()
        {
            double baseNum = GetNumber("Enter base: ");
            double exponent = GetNumber("Enter exponent: ");
            double result = Math.Pow(baseNum, exponent);
            Console.WriteLine($"Result: {result}");
            AddToHistory($"{baseNum}^{exponent} = {result}");
        }

        private void SquareRoot()
        {
            double num = GetNumber("Enter number: ");
            if (num < 0) throw new ArgumentException("Cannot sqrt negative number");
            double result = Math.Sqrt(num);
            Console.WriteLine($"Result: {result}");
            AddToHistory($"√{num} = {result}");
        }

        private void Sin()
        {
            double angle = GetNumber("Enter angle in degrees: ");
            double radians = angle * Math.PI / 180;
            double result = Math.Sin(radians);
            Console.WriteLine($"sin({angle}°) = {result}");
            AddToHistory($"sin({angle}°) = {result}");
        }

        private void Cos()
        {
            double angle = GetNumber("Enter angle in degrees: ");
            double radians = angle * Math.PI / 180;
            double result = Math.Cos(radians);
            Console.WriteLine($"cos({angle}°) = {result}");
            AddToHistory($"cos({angle}°) = {result}");
        }

        private void Tan()
        {
            double angle = GetNumber("Enter angle in degrees: ");
            double radians = angle * Math.PI / 180;
            if (Math.Cos(radians) == 0) throw new ArgumentException("Invalid angle for tangent");
            double result = Math.Tan(radians);
            Console.WriteLine($"tan({angle}°) = {result}");
            AddToHistory($"tan({angle}°) = {result}");
        }

        private void Log()
        {
            double num = GetNumber("Enter number: ");
            if (num <= 0) throw new ArgumentException("Number must be positive");
            double result = Math.Log10(num);
            Console.WriteLine($"log({num}) = {result}");
            AddToHistory($"log({num}) = {result}");
        }

        private void MemoryStore()
        {
            memory = GetNumber("Enter number to store: ");
            Console.WriteLine("Value stored in memory");
        }

        private void MemoryRecall()
        {
            Console.WriteLine($"Memory value: {memory}");
        }

        private void ShowHistory()
        {
            Console.WriteLine("\nCalculation History:");
            foreach (var entry in history)
                Console.WriteLine(entry);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void Clear()
        {
            history.Clear();
            memory = 0;
            Console.WriteLine("Calculator cleared!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.Run();
        }
    }
}
