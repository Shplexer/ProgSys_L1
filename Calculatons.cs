using System;

namespace chordMethod {
    class Calculatons {
        private static double Function(double x, SortedDictionary<string, double> arguments) {
            return arguments["a"] * Math.Pow(x, 3) + arguments["b"] * Math.Pow(x, 2) + arguments["c"] * x + arguments["d"];
        }
        public static double FindRoot(SortedDictionary<string, double> arguments) {
            double xNext = 0;
            double xPrev = arguments["x0"];
            double xCurr = arguments["x1"];
            double e = arguments["e"];
            double tmp;

            do {
                tmp = xNext;
                xNext = xCurr - Function(xCurr, arguments) * (xPrev - xCurr) / (Function(xPrev, arguments) - Function(xCurr, arguments));
                xPrev = xCurr;
                xCurr = tmp;
            } while (Math.Abs(xNext - xCurr) > e);

            return xNext;
        }
    }
}
