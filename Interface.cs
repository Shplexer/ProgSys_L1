namespace chordMethod {
    enum saveChoiceControls {
        save = 1,
        cancel,
        exit
    }
    enum MainMenuControls {
        manual = 1,
        file,
        test,
        exit
    }
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
        public static void GiveMainMenu() {
            Console.WriteLine("Выберите метод ввода данных:");
            Console.WriteLine("1. Ручной ввод");
            Console.WriteLine("2. Ввод из файла");
            Console.WriteLine("3. Тестирование");
            Console.WriteLine("3. Выход");
            DivideLine();
        }
        public static SortedDictionary<string, double> GetArguments() {
            bool exitFlag = false;
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
                                { "x0", GetDoubleInput("x0") },
                                { "x1", GetDoubleInput("x1") },
                                { "e", GetDoubleInput("e (точность)") }
                            };
                            exitFlag = true;

                            break;
                        case MainMenuControls.file:
                            //Console.WriteLine("You selected Option 2.");

                            exitFlag = true;
                            break;
                        case MainMenuControls.test:
                            GiveTestMessage();
                            break;
                        case MainMenuControls.exit:
                            Console.WriteLine("Выход...");
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                else {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                    exitFlag = false;
                }
                if (arguments["a"] == 0 && arguments["b"] == 0 && arguments["c"] == 0) {
                    Console.WriteLine("Данное уравнение не имеет корней! Попробуйте снова.");
                    exitFlag = false;
                }
                else {
                    exitFlag = true;
                }
            }
            return arguments;
        }
        public static void GiveInputInstructions() {
            Console.WriteLine("Введите аргументы для уравнения типа: ax^3 + bx^2 + cx + d");
            Console.WriteLine("Для использования знака минуса введите аргументы с отрицательным значением");
            Console.WriteLine("Пример: x^3 - 0.2x^2 + 0.5x + 1.5 = 0");
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

                if (errFlag) {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                }

            } while (errFlag);

            return number;
        }
        private static void GiveTestMessage() {
            Console.WriteLine("Запускаю тестирование");
        }

        public static void saveChoice(string result, SortedDictionary<string, double> arguments) {
            bool errFlag = false;
            Console.WriteLine("Сохранить результат?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            Console.WriteLine("3. Выход из программы");
            do {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3) {
                    saveChoiceControls selection = (saveChoiceControls)choice;
                    switch (selection) {
                        case saveChoiceControls.save:
                            (bool isNameValid, string filePath) = Files.FileUploadValidation();
                            if (isNameValid) {
                                Files.FileUpload(filePath, result, arguments);
                            }
                            break;
                        case  saveChoiceControls.cancel:

                            break;
                        case saveChoiceControls.exit:

                            break;
                        default:
                            break;
                    }
                }
                else {
                    Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                    errFlag = true;
                }
            } while (errFlag);
        }

    }
}