using System;

namespace lab12_console_
{
    class Matrix
    {
        //поля класса
        double[][] DoubleArray;
        int n;
        int m;

        public Matrix(int rows, int cols)       //конструктор массива
        {
            n = rows;
            m = cols;
            DoubleArray = new double[n][];
        }

        public void EnterElements()     //заполнение массива
        {
            try
            {
                for (int i = 0; i < n; i++)
                {
                    DoubleArray[i] = new double[n];
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write($"[{i},{j}] = ");
                        DoubleArray[i][j] = double.Parse(Console.ReadLine());
                    }

                }
            }
            catch
            {
                Console.WriteLine("Ошибка! Ввод некорректных данных!");
                Environment.Exit(0);
            }

        }

        public void PrintMatrix()        //Вывод массива на экран
        {
            for (int i = 0; i < DoubleArray.Length; i++)
            {
                for (int j = 0; j < DoubleArray[i].Length; j++)
                {
                    Console.Write(DoubleArray[i][j] + "\t");
                }
                Console.WriteLine();
            }

        }

        public void Sort()      //сортировка массива (отсортировать элементы каждой строки массива в порядке убывания)
        {
            for (var i = 0; i < DoubleArray.Length; ++i)
            {
                Array.Sort(DoubleArray[i]);
                Array.Reverse(DoubleArray[i]);
            }
        }

        public int ElementCount        //подсчет количества элементов в массиве (доступно только для чтения)
        {
            get { return n * m; }       //аксессор для чтения внутренней переменной класса
        }

        public double ScalarMultiply        //Увеличение на скаляр (доступно только для записи)
        {
            set     //аксессор для записи значения во внутреннее поле класса
            {
                double roundTo = Math.Pow(10, 1);
                for (int i = 0; i < DoubleArray.Length; i++)
                {
                    for (int j = 0; j < DoubleArray[i].Length; j++)
                    {
                        DoubleArray[i][j] = Math.Truncate((DoubleArray[i][j] += value) * roundTo) / roundTo;     //value -
                                                                                        //неявный параметр, содержащий значение, которое присваивается свойству
                    }

                }
            }
        }
        public double this[int n, int m]        //двумерный индексатор (позволяет обращаться к данным по индексу)
                                                //this - ключевое слово, используемое вместо названия
        {
            get { return DoubleArray[n][m]; }
        }


        public static Matrix operator ++(Matrix myNewClass)     //перегруженный оператор увеличивающий значение всех элементов на 1
        {
            double roundTo = Math.Pow(10, 2);
            for (int i = 0; i < myNewClass.DoubleArray.Length; i++)
            {
                for (int j = 0; j < myNewClass.DoubleArray[i].Length; j++)
                {
                   myNewClass.DoubleArray[i][j] = Math.Truncate((myNewClass.DoubleArray[i][j] += 1) * roundTo) / roundTo;
                }

            }
            return myNewClass;
        }


        public static Matrix operator --(Matrix myNewClass)     //перегруженный оператор уменьшающий значение всех элементов на 1
        {
            double roundTo = Math.Pow(10, 2);
            for (int i = 0; i < myNewClass.DoubleArray.Length; i++)
            {
                for (int j = 0; j < myNewClass.DoubleArray[i].Length; j++)
                {
                    myNewClass.DoubleArray[i][j] = Math.Truncate((myNewClass.DoubleArray[i][j] -= 1) * roundTo) / roundTo;
                }

            }
            return myNewClass;
        }
        
        //обращение к экземпляру класса дает значение true, если каждая строка массива упорядоченна по возрастанию,
        //иначе false.
        public static bool operator true(Matrix myNewClass)     // перегрузка констант true и false
        {
            int flag = 0;
            for (int r = 0; r < myNewClass.DoubleArray.Length; r++)
            {
                for (int i = 0; i < myNewClass.DoubleArray[r].Length; i++)
                {
                    for (int j = 0; j < myNewClass.DoubleArray[r].Length - 1; j++)
                    {
                        if (myNewClass.DoubleArray[r][j] < myNewClass.DoubleArray[r][j + 1]) 
                        {
                            flag++;
                        }
                    }
                }
            }
            if (flag == 0)
                return true;        //true - если массив упорядочен по возрастанию
            else return false;
        }

        //обращение к экземпляру класса дает значение true, если каждая строка массива упорядоченна по возрастанию,
        //иначе false.
        public static bool operator false(Matrix myNewClass)        // перегрузка констант true и false
        {
            int flag = 0;
            for (int r = 0; r < myNewClass.DoubleArray.GetLength(0); r++)
            {
                for (int i = 0; i < myNewClass.DoubleArray.GetLength(1); i++)
                {
                    for (int j = 0; j < myNewClass.DoubleArray.GetLength(1) - 1; j++)
                    {
                        if (myNewClass.DoubleArray[r][j] < myNewClass.DoubleArray[r][j + 1])
                        {
                            flag++;
                        }
                    }
                }
            }
            if (flag != 0)
                return false;
            else return true;

        }
        public static Matrix operator *(Matrix A, Matrix B)     //перегрузка, умножающая два массива соответствующих размерностей
        {
            int a = 0;
            int b = 0;

            try
            {
                if (A.n == B.n && A.m == B.m)
                {
                    a = A.n;
                    b = A.m;
                }
                else
                {
                    Console.WriteLine("Массивы разных размерностей!");
                    Console.Write("Введите количество строк: ");
                    try
                    {
                        a = Convert.ToInt32(Console.ReadLine());
                        if (a <= 0)
                        {
                            Console.WriteLine("Ошибка! Количество строк не может иметь отрицательное или нулевое значение!");
                            Environment.Exit(0);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Неверный формат ввода данных!");
                        Environment.Exit(0);
                    }

                    Console.Write("Введите количество столбцов: ");
                    try
                    {
                        b = Convert.ToInt32(Console.ReadLine());
                        if (b <= 0)
                        {
                            Console.WriteLine("Ошибка! Количество столбцов не может иметь отрицательное или нулевое значение!");
                            Environment.Exit(0);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка! Неверный формат ввода данных!");
                        Environment.Exit(0);
                    }

                    B = new Matrix(a, b);       //создание второй матрицы
                }
            }
            catch
            {
                Console.WriteLine("Ошибка");
            }
            
            Matrix C = new Matrix(a, b);    //создание конечной (перемноженной) матрицы
            Console.WriteLine("Заполнение:");
            B = new Matrix(a, b);               //заполнение второй матрицы
            B.EnterElements();
            double roundTo = Math.Pow(10, 2);
            for (int i = 0; i < C.DoubleArray.Length; i++)
            {
                C.DoubleArray[i] = new double[C.m];
                for (int j = 0; j < C.DoubleArray[i].Length; j++)
                {
                    for (int k = 0; k < C.DoubleArray[i].Length; k++)
                    {
                       C.DoubleArray[i][j] = Math.Truncate((C.DoubleArray[i][j] += A[i, k] * B[k, j]) * roundTo / roundTo);       
                        //умножаем строки одной матрицы на столбцы другой
                    }

                }
            }
            Console.WriteLine("\nПеремноженная матрица:\n");
            C.PrintMatrix();
            return C;
        }
        
        public static implicit operator Matrix(double[][] mx)       //преобразование в двумерный массив (неявное преобразование)
        {
            return new Matrix(mx);
        }
        
        public static explicit operator double[][](Matrix mx)       //преобразование в ступенчатый массив (явное преобразование)
        {
            return mx.DoubleArray;
        }
        
        public Matrix(double[][] mx)        //заполнение двумерного массива исходя из уже заполненного ступенчатого
        {
            DoubleArray = new double[mx.Length][];
            for (int i = 0; i < mx.Length; ++i)
            {
                DoubleArray[i] = new double[mx[i].Length];
                for (int j = 0; j < mx[i].Length; ++j)
                {
                    DoubleArray[i][j] = mx[i][j];
                }
            }
        }


    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            int m = 0;
            Console.Write("Введите количество строк: ");
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n <= 0)
                {
                    Console.Write("Ошибка! Количество строк не может иметь отрицательное или нулевое значение!");
                    Environment.Exit(0);
                }
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверный формат ввода данных!");
                Environment.Exit(0);
            }

            Console.Write("Введите количество столбцов: ");
            try
            {
                m = Convert.ToInt32(Console.ReadLine());
                if (m <= 0)
                {
                    Console.Write("Ошибка! Количество столбцов не может иметь отрицательное или нулевое значение!");
                    Environment.Exit(0);
                }
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверный формат ввода данных!");
                Environment.Exit(0);
            }

            Matrix newMatrix = new Matrix(n, m);
            int command = 0;
            do
            {

                Console.Write("\nВыберите команду: \n" +
                        "Введите 1, чтобы заполнить массив\n" +
                        "Введите 2, чтобы вывести элементы массива\n" +
                        "Введите 3, чтобы отсоритровать элементы каждой строки в порядке убывания\n" +
                        "Введите 4, чтобы получить количество элементов в массиве\n" +
                        "Введите 5, чтобы увеличить значение всех элементов массива на скаляр\n" +
                        "Введите 6, чтобы обратиться к конкретному элементу\n" +
                        "Введите 7, чтобы увеличить все элементы массива на 1\n" +
                        "Введите 8, чтобы уменьшить все элементы массива на 1\n" +
                        "Введите 9, чтобы получить информацию отсорирован массив или нет(константа true)\n" +
                        "Введите 10, чтобы получить информацию отсорирован массив или нет(константа false)\n" +
                        "Введите 11, чтобы перемножить массивы\n" +
                        "Введите 12, чтобы преобразовать массив в двумерный\n" +
                        "Введите 13, чтобы преобразовать массив в ступенчатый\n");
                Console.WriteLine();
                Console.WriteLine("Введите число от 1 до 13");

                try
                {
                    command = Convert.ToInt32(Console.ReadLine());
                    if (command <= 0 || command > 13)
                    {
                        Console.WriteLine("Ошибка! Введите число от 1 до 13");
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка! Неверный ввод данных!");
                }

                Console.WriteLine();

                switch (command)
                {
                    case 1:
                        newMatrix.EnterElements();
                        break;
                    case 2:
                        Console.WriteLine("Массив:");
                        newMatrix.PrintMatrix();
                        break;
                    case 3:
                        Console.WriteLine("Отсортированный массив: ");
                        newMatrix.Sort();
                        newMatrix.PrintMatrix();
                        break;
                    case 4:
                        Console.WriteLine($"Количество элементов в массиве: {newMatrix.ElementCount}");
                        break;
                    case 5:
                        Console.Write("Введите скаляр: ");
                        double scalar = 0;
                        try
                        {
                            scalar = Convert.ToDouble(Console.ReadLine());
                            if (scalar <= 0)
                            {
                                Console.WriteLine("Ошибка! Скаляр не может иметь отрицательное или нулевое значение!");
                                Environment.Exit(0);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка! Неверный формат ввода данных!");
                        }
                        newMatrix.ScalarMultiply = scalar;
                        newMatrix.PrintMatrix();

                        Console.WriteLine("Все элементы увеличены на скаляр");
                        break;
                    case 6:
                        int row = 0;
                        int col = 0;
                        
                            Console.WriteLine("Введите номер строки (отсчет от 0): ");
                        try
                        {
                            row = Convert.ToInt32(Console.ReadLine());
                            if (row < 0)
                            {
                                Console.WriteLine("Ошибка! Такой строки не существует!");
                                Environment.Exit(0);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка! Некорректный формат ввода данных!");
                            Environment.Exit(0);
                        }
                            
                            Console.WriteLine("Введите номер столбца (отсчет от 0): ");
                        try
                        {
                            col = Convert.ToInt32(Console.ReadLine());
                            if (col < 0)
                            {
                                Console.WriteLine("Ошибка! Такого столбца не существует!");
                                Environment.Exit(0);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ошибка! Некорректный формат ввода данных!");
                            Environment.Exit(0);
                        }
                        

                        try
                        {
                            Console.WriteLine($"Элемент под индексом [{row},{col}]: " + newMatrix[row, col]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ошибка! Такого элемента не существует!");
                            continue;
                        }
                        break;

                    case 7:
                        Console.WriteLine("Все элементы увеличены на 1\n");
                        newMatrix++;
                        Console.WriteLine("Массив:");
                        newMatrix.PrintMatrix();
                        break;

                    case 8:
                        Console.WriteLine("Все элементы уменьшены на 1\n");
                        newMatrix--;
                        Console.WriteLine("Массив:");
                        newMatrix.PrintMatrix();
                        break;

                    case 9:

                        case 10:
                        if (newMatrix) Console.WriteLine("Строки массива упорядочены по возростаню.");
                        else
                            Console.WriteLine("Строки массива не упорядочены по возростанию.");
                        break;

                    case 11:
                        Console.Write("Перемножить массивы\n");
                        int a = 0;
                        int b = 0;

                        Matrix myNewClass = new Matrix(a, b);

                        newMatrix *= myNewClass;

                        break;

                    case 12:
                        Matrix array = newMatrix;
                        Console.WriteLine("Массив преобразованный в двухмерный");
                        array.PrintMatrix();
                        break;

                    case 13:
                        Matrix array1 = (double[][])newMatrix;
                        Console.WriteLine("Массив преобразованный в ступенчатый");
                        array1.PrintMatrix();
                        break;

                }
            } while (command != 13);
            Console.WriteLine("Конец программы");
        }

    }

}
