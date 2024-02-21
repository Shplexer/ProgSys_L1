using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace chordMethod {
    class Calculatons {
        public static int countDigitsAfterDecimalPoint(double number) {
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

        //public static double dF(double x, SortedDictionary<string, double> arguments) {
        //    double result = 3 * arguments["a"] * Math.Pow(x, 2) + 2 * arguments["b"] * x + arguments["c"];
        //    return result;
        //}

        //public static double ddF(double x, SortedDictionary<string, double> arguments) {
        //    double result = 6 * arguments["a"] * x + 2 * arguments["b"];
        //    return result;
        //}
        public static string FindRoot(SortedDictionary<string, double> arguments) {
            double leftBorder = arguments["x0"];
            double rightBorder = arguments["x1"];

            double fault = arguments["e"];

            int digitsAfterDecimal = countDigitsAfterDecimalPoint(fault);

            Console.WriteLine($"Number of digits after the decimal point: {digitsAfterDecimal}");

            // Начать с предположения, что корень находится в середине интервала.
            double solution = (leftBorder + rightBorder) / 2;
            if (F(leftBorder, arguments) * F(rightBorder, arguments) > 0) {
                return "На интервале нет корня";
            }
            else {

                // Проверить знак второй производной в середине интервала.
                // bool isddFPositive = ddF(solution, arguments) >= 0;
                // Объявить функцию, которая будет использоваться для обновления решения.
                Func<double, double> usedFtion;

                // Если вторая производная положительна, использовать одну форму функции обновления.
                //if (isddFPositive) {
                //Console.WriteLine(isddFPositive);
                //usedFtion = (double x) => x - F(x, arguments) * (rightBorder - x) / (F(rightBorder, arguments) - F(x, arguments));
                //}
                // Если вторая производная отрицательна, использовать другую форму функции обновления.
                //else {
                //    Console.WriteLine(isddFPositive);
                usedFtion = (double x) => leftBorder - F(leftBorder, arguments) * (x - leftBorder) /
                                               (F(x, arguments) - F(leftBorder, arguments));
                //}

                // Продолжать итерации до тех пор, пока значение функции в точке решения не будет в пределах желаемого интервала ошибок.
                while (Math.Abs(F(solution, arguments)) > fault) {
                    solution = usedFtion(solution);
                    Console.WriteLine(Math.Abs(F(solution, arguments)) + " " + fault + " " + solution.ToString($"F{digitsAfterDecimal}"));
                }
            }
            //TODO: преобразовать итоговое решение в стрингу
            return solution.ToString($"F{digitsAfterDecimal}");
        }
    }
}
