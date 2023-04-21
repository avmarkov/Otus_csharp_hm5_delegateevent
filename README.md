### Домашняя работа № 5. Делегаты и события

#### 1. Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности поиска максимального значения.
```cs
public static T GetMax(this IEnumerable e, Func<T, float> getParameter) where T : class;
```

Я сделал такую функцию:

```cs
//Обобщённая функция расширения, находящая и возвращающая максимальный элемент коллекции
public static T? GetMax<T>(this IEnumerable e, Func<T, float> getParameter) where T : class
{
    if (e == null)
    {
        return default(T);
    }

    T max = default(T);

    foreach (T item in e)
    {
        if (
                (max == null) ||
                (getParameter(item) > getParameter(max))
            )
        {
            max = item;
        }
    }

    return max;
}
```

Вызов этой функции происходит в коде ниже:

```cs
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
```

#### 2. Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
Я создал такой класс Scanner:
```cs
public class Scanner
{
    public event EventHandler<FileArgs> FileFound;

    private bool isCancel;
    public int Counter { get; set; }
    public void Scan(string path)
    {
        isCancel = false;

        Counter = 0;

        if (Directory.Exists(path))
        {
            ProcessDirectory(path);
        }           
        else
        {
            Console.WriteLine("Path " + path + " doesn't exist.");
        }
    }

    private void ProcessDirectory(string targetDirectory)
    {
        if (isCancel)
        {
            return;
        }


        var fileEntries = Directory.GetFiles(targetDirectory);

        foreach (string fileName in fileEntries)
        {
            if (isCancel)
            {
                return;
            }

            ProcessFile(fileName);
        }

        var subdirectoryEntries = Directory.GetDirectories(targetDirectory);

        foreach (string subdirectory in subdirectoryEntries)
        {
            if (isCancel)
            {
                return;
            }

            ProcessDirectory(subdirectory);
        }
    }

    private void ProcessFile(string path)
    {
        Counter++;

        // срабытывание события (обработка будет в FileFoundProc)
        FileFound?.Invoke(this, new FileArgs(path));
    }

    public void StopScanning()
    {
        Console.WriteLine("\nПрерывание обработки\n");

        isCancel = true;
    }
}
```

#### 3. Оформить событие и его аргументы с использованием .NET соглашений: 
* public event EventHandler FileFound;
* FileArgs – будет содержать имя файла и наследоваться от EventArgs

Событие находится в классе Scanner:
```cs
public event EventHandler<FileArgs> FileFound;
```

FileArgs оформил в виде класса:
```cs
public class FileArgs : EventArgs
{
    public string FilePath { get; set; }
    public FileArgs(string filePath = "")
    {
        FilePath = filePath;
    }
}
```

#### 4. Добавить возможность отмены дальнейшего поиска из обработчика

Отмена поиска осуществляется в обработчике FileFoundProc с помощью вызова scanner.StopScanning();
```cs
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
```

#### 5. Вывести в консоль сообщения, возникающие при срабатывании событий и результат поиска максимального элемента.
<image src="images/res1.png" alt="result">

<image src="images/res2.png" alt="result">
