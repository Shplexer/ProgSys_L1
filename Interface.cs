namespace chordMethod {
    enum SaveChoiceControls {
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
        public static void DivideLine() {
            Console.WriteLine("==========================================================================================================");
        }
        public static void GiveWelcomeMessage() {
            DivideLine();
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine("Вариант №3 работы №1 был выполнен студентом группы 423 Ореховым Даниилом.");
            Console.WriteLine("Задание: Для заданной функции на заданном интервале найти требуемое значение методом хорд.");
            DivideLine();
        }
        private static void GiveMainMenu() {
            Console.WriteLine("Выберите метод ввода данных:");
            Console.WriteLine("1. Ручной ввод");
            Console.WriteLine("2. Ввод из файла");
            Console.WriteLine("3. Тестирование");
            Console.WriteLine("4. Выход");
            DivideLine();
        }
        public static SortedDictionary<string, double> GetArguments() {
            bool exitFlag = false;
            SortedDictionary<string, double> arguments = new SortedDictionary<string, double>();

            while (!exitFlag) {
                GiveMainMenu();

                MainMenuControls selection = (MainMenuControls)GetIntInput();

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
                        string fileName = Files.FileDownloadValidation();
                        if(fileName == "~") {
                            exitFlag = false;
                            continue;
                        }
                        arguments = Files.FileDownload(fileName);
                        exitFlag = true;
                        break;
                    case MainMenuControls.test:
                        Test.TestFindRootFindsCorrectRoot();
                        exitFlag = false;
                        continue;
                    case MainMenuControls.exit:
                        Console.WriteLine("Выход...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                        exitFlag = false;
                        continue;
                }

                if (arguments["a"] == 0 && arguments["b"] == 0 && arguments["c"] == 0) {
                    Console.WriteLine("Данное уравнение не имеет корней! Попробуйте снова.");
                    exitFlag = false;
                }
                else if (arguments["x0"] >= arguments["x1"]) {
                    Console.WriteLine("Некорректно введен интервал! Попробуйте снова.");
                    exitFlag = false;
                }
                else {
                    exitFlag = true;
                }
            }
            return arguments;
        }
        private static void GiveInputInstructions() {
            Console.WriteLine("Введите аргументы для уравнения типа: ax^3 + bx^2 + cx + d");
            Console.WriteLine("Для использования знака минуса введите аргументы с отрицательным значением");
            Console.WriteLine("Пример: x^3 - 0.2x^2 + 0.5x + 1.5 = 0");
        }
        public static int GetIntInput() {
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
        private static double GetDoubleInput(string inputName) {
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
        public static void SaveChoice(string result, SortedDictionary<string, double> arguments) {
            bool errFlag = false;
            Console.WriteLine("Сохранить результат?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            Console.WriteLine("3. Выход из программы");
            do {
                SaveChoiceControls selection = (SaveChoiceControls)GetIntInput();
                switch (selection) {
                    case SaveChoiceControls.save:
                        (bool isNameValid, string filePath) = Files.FileUploadValidation();
                        if (isNameValid) {
                            Files.FileUpload(filePath, result, arguments);
                        }
                        break;
                    case SaveChoiceControls.cancel:

                        break;
                    case SaveChoiceControls.exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                        errFlag = true;
                        break;
                }
            } while (errFlag);
        }
    }
}