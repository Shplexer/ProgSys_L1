using System;

namespace chordMethod {
    class Files {
        public static (bool isValid, string fileName) FileUploadValidation() {
            bool saveFlag = true;
            bool errFlag = false;
            string? fileName;
            do {
                saveFlag = true;
                errFlag = false;
                Console.WriteLine("Введите имя файла: ");
                do {
                    fileName = Console.ReadLine();
                }while(string.IsNullOrEmpty(fileName));


                if (!fileName.EndsWith(".txt")) {
                    fileName += ".txt";
                }
                fileName = $"../../../{fileName}";

                //if (!File.Exists(fileName)) {
                //    Console.WriteLine("Ошибка! Файл с таким именем не найден.");
                //    errFlag = true;
                //}
                if (File.Exists(fileName)) {
                    Console.WriteLine("Файл с таким именем уже существует. Перезаписать?");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");
                    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 2) {
                        saveChoiceControls selection = (saveChoiceControls)choice;
                        switch (selection) {
                            case saveChoiceControls.save:
                                saveFlag = true;
                                break;
                            case saveChoiceControls.cancel:
                                saveFlag = false;
                                break;
                            default:
                                break;
                        }
                    }
                    else {
                        Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                        errFlag = true;
                    }
                }
                else {
                    saveFlag = true;
                    errFlag = false;
                }


            } while (errFlag);
            return (saveFlag, fileName);
        }

        public static void FileUpload(string fileName, string result, SortedDictionary<string, double> arguments) {
            using StreamWriter writer = new (fileName);
            foreach (KeyValuePair<string, double> kvp in arguments) {
                writer.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            writer.WriteLine("//");
            writer.WriteLine($"Найденный корень: {result}");
        }
        public static string fileDownloadValidation() {
            string? fileName;
            bool errFlag;
            do {
                errFlag = false;
                Console.WriteLine("Введите имя файла: ");
                do {
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName));


                if (!fileName.EndsWith(".txt")) {
                    fileName += ".txt";
                }
                fileName = $"../../../{fileName}";

                if (!File.Exists(fileName)) {
                    Console.WriteLine("Ошибка! Файл с таким именем не найден.");
                    errFlag = true;
                }
                else {
                    StreamReader reader = new StreamReader(fileName);
                    errFlag = false;
                }


            } while (errFlag);
            return fileName;
        }
    }
}
