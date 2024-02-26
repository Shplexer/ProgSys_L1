using System;
using System.Text.RegularExpressions;

namespace chordMethod {
    class Files {
        public static (bool isValid, string fileName) FileUploadValidation() {
            bool saveFlag = true;
            bool errFlag = false;
            string pattern = @"^[a-zA-Z0-9_.-]+$";
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL",
                "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            string? fileName;
            do {
                saveFlag = true;
                errFlag = false;
                do {
                Console.WriteLine("Введите имя файла: ");
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName) || !Regex.IsMatch(fileName, pattern) || reservedNames.Contains(fileName.ToUpper()));


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
                    SaveChoiceControls selection = (SaveChoiceControls)Interface.GetIntInput();
                    switch (selection) {
                        case SaveChoiceControls.save:
                            saveFlag = true;
                            break;
                        case SaveChoiceControls.cancel:
                            saveFlag = false;
                            break;
                        default:
                            Console.WriteLine("Ошибка ввода! Попробуйте снова.");
                            errFlag = true;
                            break;
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
            using StreamWriter writer = new(fileName);
            foreach (KeyValuePair<string, double> kvp in arguments) {
                writer.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            writer.WriteLine("//");
            writer.WriteLine($"Найденный корень: {result}");
            writer.Close();
        }
        public static string FileDownloadValidation() {
            string? fileName;
            bool errFlag;
            do {
                errFlag = false;
                Console.WriteLine("Введите имя файла или напишите '~' для выхода: ");

                do {
                    fileName = Console.ReadLine();
                } while (string.IsNullOrEmpty(fileName));
                if (fileName == "~") {
                    return fileName;
                }


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
                    string? line;
                    string endSymbol = "//";
                    var keyValuePairs = new SortedDictionary<string, double>();
                    string[] mustHaveKeys = ["a", "b", "c", "d", "x0", "x1", "e"];
                    while (!reader.EndOfStream) {

                        line = reader.ReadLine();
                        while (string.IsNullOrEmpty(line)) {
                            line = reader.ReadLine();
                        }
                        // Check if the current line contains the symbol.
                        if (line.Trim().StartsWith(endSymbol)) {
                            break; // Stop reading if the symbol is encountered.
                        }
                        string[] parts = line.Split(':');
                        if (parts.Length == 2) {

                            string key = parts[0].Trim();
                            string valueString = parts[1].Trim().Replace(',', '.');

                            if (double.TryParse(valueString, out double value) && !keyValuePairs.ContainsKey(key) && mustHaveKeys.Contains(key)) {
                                keyValuePairs.Add(key, value);
                                //Console.WriteLine($"{key} {value}");
                            }
                            else {
                                errFlag = true;
                            }

                        }
                        else {
                            errFlag = true;
                        }
                    }
                    if (keyValuePairs.Count != mustHaveKeys.Length) {
                        errFlag = true;
                        //Console.WriteLine($"{keyValuePairs.Count} {mustHaveKeys.Length}");
                    }
                    if (errFlag) {
                        Console.WriteLine("Ошибка! Неверный формат данных в файле!");
                    }
                    reader.Close();
                }


            } while (errFlag);
            return fileName;
        }
        public static SortedDictionary<string, double> FileDownload(string fileName) {
            var arguments = new SortedDictionary<string, double>();

            using StreamReader reader = new(fileName);
            string? line;
            string endSymbol = "//";
            while (!reader.EndOfStream) {
                line = reader.ReadLine();
                while (string.IsNullOrEmpty(line)) {
                    line = reader.ReadLine();
                }
                if (line.Trim().StartsWith(endSymbol)) {
                    break;
                }
                string[] parts = line.Split(':');

                string key = parts[0].Trim();
                string valueString = parts[1].Trim().Replace(',', '.');

                double value = double.Parse(valueString);

                arguments.Add(key, value);
                Console.WriteLine($"{key} {value}");

            }
            reader.Close();
            return arguments;
        }
    }
}
