using System;

namespace chordMethod {
    class Calculatons {
        private static int CountDigitsAfterDecimalPoint(double number) {
            string numberString = number.ToString("0.#############################");

            // Check if there is a decimal point in the string
            int decimalPointIndex = numberString.IndexOf('.');
            int digitsAfterDecimal = 0;

            if (decimalPointIndex != -1) {
                // Count the digits after the decimal point
                digitsAfterDecimal = numberString.Length - decimalPointIndex - 1;
            }
            digitsAfterDecimal++;

            return digitsAfterDecimal;
        }
        private static double F(double x, SortedDictionary<string, double> arguments) {
            double result = arguments["a"] * Math.Pow(x, 3) + arguments["b"] * Math.Pow(x, 2) + arguments["c"] * x + arguments["d"];
            return result;
        }

        private static double ddF(double x, SortedDictionary<string, double> arguments) {
            double result = 6 * arguments["a"] * x + 2 * arguments["b"];
            return result;
        }
        public static string FindRoot(SortedDictionary<string, double> arguments) {
            double x0 = arguments["x0"];
            double x1 = arguments["x1"];
            double fault = arguments["e"];
            int iterationsNumber = 0;
            int digitsAfterDecimal = CountDigitsAfterDecimalPoint(fault);

            //Console.WriteLine($"Number of digits after the decimal point: {digitsAfterDecimal}");

            // Начать с предположения, что корень находится в середине интервала.
            double solution = (x0 + x1) / 2;


            if (F(x0, arguments) * F(x1, arguments) >= 0) {
                return "На интервале нет гарантированного корня";
            }

            Func<double, double> usedFunction;
            //Console.WriteLine("=="+ F(solution, arguments) * ddF(solution, arguments));
            //if (F(solution, arguments) * ddF(solution, arguments) >= 0) {
            //    usedFunction = (double x) => x - F(x, arguments) * (x1 - x) / (F(x1, arguments) - F(x, arguments));
            //}
            //else {
            //    usedFunction = (double x) => x0 - F(x0, arguments) * (x - x0) / (F(x, arguments) - F(x0, arguments));
            //}

            while (Math.Abs(F(solution, arguments)) > fault) {
                if (F(solution, arguments) * ddF(solution, arguments) >= 0) {
                    usedFunction = (double x) => x - F(x, arguments) * (x1 - x) / (F(x1, arguments) - F(x, arguments));
                }
                else {
                    usedFunction = (double x) => x0 - F(x0, arguments) * (x - x0) / (F(x, arguments) - F(x0, arguments));
                }
                solution = usedFunction(solution);
                Console.WriteLine(Math.Abs(F(solution, arguments)) + " " + fault + " " + solution.ToString($"F{digitsAfterDecimal}"));
                iterationsNumber++;
                //Console.WriteLine("==" + F(solution, arguments) * ddF(solution, arguments));
                if (iterationsNumber > 1000) {
                    return ("Превышено максимальное количество итераций!");
                }
            }

            return solution.ToString($"F{digitsAfterDecimal}");
        }
    }
}
