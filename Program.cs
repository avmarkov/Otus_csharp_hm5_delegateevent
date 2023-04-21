
using hm5_delegateevent;

List<MyClass> list = new List<MyClass> {
                                            new MyClass(43.1f),
                                            new MyClass(67),
                                            new MyClass(-454.7f),
                                            new MyClass(69.6f),
                                            new MyClass(15f),
                                       };

Console.WriteLine("Поиск максимального элемента коллекции: ");

foreach (MyClass item in list)
{
    Console.Write(item.Value + "; ");
}

var max = list.GetMax<MyClass>(e => e.Value);

Console.WriteLine("\nМаксимальный элемент коллекции: " + max.Value);


string dir = @"C:\Users\Public";


Console.WriteLine("\nСканируемый каталог файлов: " + dir +"\n");

Scanner scanner = new Scanner();

// пописываемся на событие 
scanner.FileFound += FileFoundProc;

scanner.Scan(dir);

Console.WriteLine("Finish.");


static void FileFoundProc(object sender, FileArgs file)
{
    Console.WriteLine("Сработало событие. Найден файл:" + file.FilePath);

    Scanner scanner = (Scanner)sender;
    if (scanner.Counter > 150)
    {
        // Прерывание обработки
        scanner.StopScanning();
    }
}

