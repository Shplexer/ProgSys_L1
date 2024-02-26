using System;
using System.Globalization;

namespace chordMethod {
    class Program {
        public static void Main() {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            while (true) {
                Interface.GiveWelcomeMessage();

                SortedDictionary<string, double> arguments = Interface.GetArguments();
                string x = Calculatons.FindRoot(arguments);
                
                Interface.DivideLine();
                Console.WriteLine($"Результат: {x}");
                Interface.DivideLine();
                
                Interface.SaveChoice(x, arguments);
            }
        }
    }
}