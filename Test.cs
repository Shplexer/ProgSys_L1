using System;

namespace chordMethod {
    class Test {
        public static void TestFindRootFindsCorrectRoot() {

            var arguments = new SortedDictionary<string, double> {
                ["x0"] = 1.5,   
                ["x1"] = 2.5,   
                ["e"] = 0.001,
                ["a"] = 1,    
                ["b"] = -6,   
                ["c"] = 11,   
                ["d"] = -6    
            };

            const double expectedRootDouble = 2.0000;
            
            string expectedRoot = expectedRootDouble.ToString("F4");
            string actualRoot = Calculatons.FindRoot(arguments);
            
            if (actualRoot == expectedRoot) {
                Console.WriteLine("ТЕСТ ПРОЙДЕН");
            }
            else {
                Console.WriteLine("ТЕСТ НЕ ПРОЙДЕН");
                Console.WriteLine($"РЕЗУЛЬТАТ РАБОТЫ ПРОГРАММЫ: {actualRoot}");
                Console.WriteLine($"ОЖИДАЕМЫЙ РЕЗУЛЬТАТ: {expectedRoot}");
                Interface.DivideLine();
            }
        }
    }
}
