namespace chordMethod {
    class Calculatons {
        private static double F(double x, SortedDictionary<string, double> arguments) {
            //Console.WriteLine("Обрабатывается следующая функция: ");
            // int i = 3;
            // foreach(var pair in arguments) {
            //     //Console.WriteLine($"---{pair.Key}: {pair.Value} {i}");
            //     if (pair.Value < 0) {
            //         Console.Write($"- {Math.Abs(pair.Value)}x^{i} ");
            //     }
            //     else if(pair.Value > 0 && i > 0) {
            //         if(i == 3) {
            //             Console.Write($"{pair.Value}x^{i} ");
            //         }
            //         else {
            //             Console.Write($"+ {pair.Value}x^{i} ");
            //         }
            //     }
            //     if(i == 0) {
            //         Console.WriteLine("");
            //     }
            //     else if(i < 0) {
            //         Console.WriteLine($"{pair.Key}: {pair.Value} ");
            //     }
            //     i--;
            //}
            double result = arguments["a"] * Math.Pow(x, 3) + arguments["b"] * Math.Pow(x, 2) + arguments["c"] * x + arguments["d"];
            //Console.WriteLine($"{x}, {result}");
            //double result = Math.Pow(x, 3) - 18 * x - 83;
            return result;
        }
        public static double FindRoot(SortedDictionary<string, double> arguments) {
            //double xNext = 0;
            //double xPrev = arguments["x0"];
            //double xCurr = arguments["x1"];
            //double e = arguments["e"];
            //double tmp;
            ////
            //do {
            //    tmp = xNext;
            //    xNext = xCurr - F(xCurr, arguments) * (xPrev - xCurr) / (F(xPrev, arguments) - F(xCurr, arguments));
            //    xPrev = xCurr;
            //    xCurr = tmp;
            //    //Console.WriteLine("=======Res::" + xNext + "=======");
            //    //Console.WriteLine("===========");
            //} while (Math.Abs(xNext - xCurr) > e);

            //return xNext;

            double x0 = arguments["x0"];
            double x1 = arguments["x1"];
            double e = arguments["e"];
            if (F(x0, arguments) * F(x1, arguments) > 0) {
                throw new ArgumentException("The function values at initial points must have opposite signs.");
            }

            double xNew;

            do {
                // Calculate the intersection of the chord with the x-axis
                xNew = x1 - F(x1, arguments) * (x1 - x0) / (F(x1, arguments) - F(x0, arguments));

                // Check if the function value at xNew is close to zero
                if (Math.Abs(F(xNew, arguments)) < e) {
                    return xNew; // Root found
                }

                // Update the interval bounds for the next iteration
                x0 = x1;
                x1 = xNew;
            }
            while (Math.Abs(x1 - x0) > e);

            // If we reach here, xNew is our root approximation
            return xNew;
        }
    }
}
