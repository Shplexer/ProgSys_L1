using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace chordMethod {
    class Interface {

        private static void DivideLine() {
            Console.WriteLine("==========================================================================================================");
        }
        public static void GiveWelcomeMessage() {
            DivideLine();
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine("Вариант №3 работы №1 был выполнен студентом группы 423 Ореховым Даниилом.");
            Console.WriteLine("Задание: Для заданной функции на заданном интервале найти требуемое значение методом хорд.");
            DivideLine();
        }
        enum MainMenuControls {
            manual = 1,
            file,
            exit
        }
        public static void GiveMainMenu() {
            Console.WriteLine("Выберите метод ввода данных:");
            Console.WriteLine("1.Ручной ввод");
            Console.WriteLine("2. Ввод из файла");
            Console.WriteLine("3. Выход");
            DivideLine();
        }
        public static SortedDictionary<string, double> GetArguments() {
            bool exitFlag = false;
            // Initialize 'arguments' with an empty dictionary to avoid CS0165.
            SortedDictionary<string, double> arguments = new SortedDictionary<string, double>();

            while (!exitFlag) {
                GiveMainMenu();

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3) {
                    MainMenuControls selection = (MainMenuControls)choice;

                    switch (selection) {
                        case MainMenuControls.manual:
                            GiveInputInstructions();
                            arguments = new SortedDictionary<string, double> {
                                { "a", GetDoubleInput("a") },
                                { "b", GetDoubleInput("b") },
                                { "c", GetDoubleInput("c") },
                                { "d", GetDoubleInput("d") },
                                { "x0", GetDoubleInput("x0 (минимальное значение)") },
                                { "x1", GetDoubleInput("x1 (максимальное значение)") },
                                { "e", GetDoubleInput("e (точность функции)") }

                            };
                            exitFlag = true;
                            break;
                        case MainMenuControls.file:
                            Console.WriteLine("You selected Option 2.");
                            // Add your code for Option 2 here
                            // Ensure 'arguments' is assigned appropriately after file input processing.
                            exitFlag = true;
                            break;
                        case MainMenuControls.exit:
                            Console.WriteLine("Exiting the menu.");
                            // If exiting, 'arguments' could be returned as an empty dictionary, or
                            // you could choose to return null or a different value if that's appropriate for your application.
                            exitFlag = true;
                            break;
                        default:
                            break;
                    }
                }
                else {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                    // No need to set exitFlag here; it's already false.
                }
            }
            return arguments; 
        }
        public static void GiveInputInstructions() {
            Console.WriteLine("Введите аргументы для уравнения типа: ax^3 + bx^2 + cx + d");
            Console.WriteLine("Для использования знака минуса введите аргументы с отрицательным значением");
            Console.WriteLine("Пример: x^3 - 18x - 83 = 0");
        }
        public static double GetIntInput() {
            bool errFlag;
            int number;
            do {
                string? userInput = Console.ReadLine();
                errFlag = !int.TryParse(userInput, out number);

                if (errFlag) {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                }

            } while (errFlag);

            return number;
        }
        public static double GetDoubleInput(string inputName) {
            bool errFlag;
            double number;
            do {
                Console.WriteLine($"Введите переменную '{inputName}'");
                string? userInput = Console.ReadLine();
                errFlag = !double.TryParse(userInput?.Replace(',', '.'), out number);

                //Console.WriteLine($"Current culture: {CultureInfo.CurrentCulture.Name}");
                //Console.WriteLine(number);

                if (errFlag) {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                }
                
            } while (errFlag);

            return number;
        }

    }
}