﻿using System;
using System.Globalization;

namespace chordMethod {
    class Program {
        static void Main() {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            Interface.GiveWelcomeMessage();
            SortedDictionary<string, double> arguments = Interface.GiveMainMenu();
            
            
        }
    }
}