using System;
using System.Threading;

namespace ConsoleApp30
{
    class Program
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true); //  создаем хандлер для синхронизации потоков
        static bool ind = true; // идентификатор для цикла
        static void Main(string[] args)
        {
            Thread thread = new Thread(Print); // Инициализируем новый поток , который будет выполнять метод
            Console.WriteLine("Enter string");
            thread.Start(Console.ReadLine()); // Запускаем поток
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    ind = false;// Останавливаем цикл при нажатии ентер
                    thread.Join(); // Дожидаемся выполнения метода потоком
                    Console.WriteLine("Enter string");
                    string str = Console.ReadLine();
                    thread = new Thread(Print);// Запускаем новый поток с новой строкой 
                    thread.Start(str);                                      
                }
            }
        }

        static void Print(object str) // Метод для вывода на консоль текста
        {
            waitHandler.WaitOne();// Переводим в режим ожидания что бы потоки не могли заходить в этот блок , до того момента как он не станет в режим сигнальный

            ind = true;
            Console.Clear();
            string printStr = (string)str;
            while (ind)
            {
                Console.WriteLine(printStr);
                Thread.Sleep(1000);
            }
            waitHandler.Set(); // Говорим ожидающим потокам что объект снова с сигнальном режиме 
        }
    }



    

}
