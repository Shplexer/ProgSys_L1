namespace chordMethod {
    class Calculatons {
        private static double F(double x, SortedDictionary<string, double> arguments) {
            double result = arguments["a"] * Math.Pow(x, 3) + arguments["b"] * Math.Pow(x, 2) + arguments["c"] * x + arguments["d"];
            return result;
        }

        public static double dF(double x, SortedDictionary<string, double> arguments) {
            double result = 3 * arguments["a"] * Math.Pow(x, 2) + 2 * arguments["b"] * x + arguments["c"];
            return result;
        }

        public static double ddF(double x, SortedDictionary<string, double> arguments) {
            double result = 6 * arguments["a"] * x + 2 * arguments["b"];
            return result;
        }
        public static double FindRoot(SortedDictionary<string, double> arguments) {
            double leftBorder = arguments["x0"];
            double rightBorder = arguments["x1"];

            double fault = arguments["e"];

            // Начать с предположения, что корень находится в середине интервала.
            double solution = (leftBorder + rightBorder) / 2;
            // Проверить знак второй производной в середине интервала.
            bool isddFPositive = ddF(solution, arguments) >= 0;
            // Объявить функцию, которая будет использоваться для обновления решения.
            Func<double, double> usedFtion;

            // Если вторая производная положительна, использовать одну форму функции обновления.
            if (isddFPositive) {
                usedFtion = (double x) => x - F(x, arguments) * (rightBorder - x) / (F(rightBorder, arguments) - F(x, arguments));
            }
            // Если вторая производная отрицательна, использовать другую форму функции обновления.
            else {
                usedFtion = (double x) => leftBorder - F(leftBorder, arguments) * (x - leftBorder) / (F(x, arguments) - F(leftBorder, arguments));
            }

            // Продолжать итерации до тех пор, пока значение функции в точке решения не будет в пределах желаемого интервала ошибок.
            while (Math.Abs(F(solution, arguments)) > fault) {
                solution = usedFtion(solution);
            }

            return solution;
        }
    }
}
