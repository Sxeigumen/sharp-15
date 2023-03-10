using System;
using System.IO;
using System.Timers;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Dir newDir = new Dir(@"C:\Users\artem\source\repos\sharp 15\ex1\bin\Debug\net6.0\1122");
                Dir newDir2 = new Dir(@"C:\Users\artem\Desktop");
                Watcher watcher = new Watcher();
                watcher.AddObserver(newDir);
                watcher.AddObserver(newDir2);
                watcher.NotifyObserversTimer();

            }
            
        }

        interface IObserver
        {
            void Update();
        }

        class Dir : IObserver
        {
            public string Path { get; set; }
            public Dir(string dirPath)
            {
                Path = dirPath;
            }
            public void Update()
            {
                if (Directory.Exists(Path))
                {

                    Console.WriteLine("Содержимое директории:");

                    Console.WriteLine("Директории:");
                    string[] dirs = Directory.GetDirectories(Path);
                    foreach (string elem in dirs)
                    {
                        Console.WriteLine(elem);
                    }
                    Console.WriteLine();

                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(Path);
                    foreach (string elem in files)
                    {
                        Console.WriteLine(elem);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Директории не существует");
                }
            }
        }

        interface IObservable
        {
            void AddObserver(IObserver obj);
            void RemoveObserver(IObserver obj);
            void NotifyObservers();
            void NotifyObserversTimer();
        }

      
        class Watcher : IObservable
        {
            List<IObserver> observers;
            public Watcher()
            {
                observers = new List<IObserver>();
            }

            public void AddObserver(IObserver unit)
            {
                observers.Add(unit);
            }

            public void RemoveObserver(IObserver unit)
            {
                observers.Remove(unit);
            }

            public void NotifyObservers()
            {
                foreach (IObserver observer in observers)
                    observer.Update();
            }
            public void NotifyObserversTimer()
            {
                while (true) 
                {
                    foreach (IObserver observer in observers)
                        observer.Update();
                    Thread.Sleep(20000);
                }
                
            }

            
        }
    }
}