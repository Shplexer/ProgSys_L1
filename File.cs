using System;

namespace chordMethod {
    class File {
        public static bool FileUploadValidation() {
            bool saveFlag = true;
            bool errFlag = false;
            do {
                Console.WriteLine("Введите имя файла: ");
                string? fileName = Console.ReadLine();
                fileName = @"..\..\" + fileName;
                saveFlag = true;
                errFlag = false;
                if (!System.IO.File.Exists(fileName)) {
                    Console.WriteLine("Ошибка! Файл с таким именем не найден.");
                    errFlag = true;
                }
                else {
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
                
            } while (errFlag);
            return saveFlag;
        }
    }
}
