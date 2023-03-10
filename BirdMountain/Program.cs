using System;
using System.Collections.Generic;
using Microsoft.Win32;
namespace BirdMountain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = " ";
            foreach(string str in args)
            {
                System.Console.WriteLine(args);
            }

            if (args.Length != 0)
            {
                System.Console.WriteLine("Próba wgrania pliku: " + args[0]);
                //string currentDirectory = Directory.GetCurrentDirectory();
                string currentDirectory = @"D:\Programowanie\GitHub\CodeWars\BirdMountain\bin\Debug\net7.0";
                string[] files = Directory.GetFiles(currentDirectory, args[0], SearchOption.AllDirectories);
                if (files.Length == 0)
                    throw new FileNotFoundException("File not found: " + args);
                path = files[0];
            }
            else
            {
                bool fail = true;
                do
                {
                    try
                    {
                        Console.Write("Podaj nazwę pliku lub wpisz 0 zaby zakończyć: ");
                        path = Console.ReadLine();
                        if(path=="0")
                        {
                            fail = false;
                        }
                        System.Console.WriteLine("Próba wgrania pliku: " + path);
                        //string currentDirectory = Directory.GetCurrentDirectory();
                        string currentDirectory = @"D:\Programowanie\GitHub\CodeWars\BirdMountain\bin\Debug\net7.0";
                        string[] files = Directory.GetFiles(currentDirectory, path, SearchOption.AllDirectories);
                        if (files.Length == 0)
                            throw new FileNotFoundException("File not found: " + args);
                        path = files[0];
                        fail = false;
                    }
                    catch (FileNotFoundException)
                    {
                        System.Console.WriteLine("Wystąpił wyjątek: podano błedną nazwę pliku.");
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Wystąpił nieznany wyjątek!");
                        System.Console.WriteLine(ex.Message);
                    }
                }
                while (fail);
            }

            string[] lines = File.ReadAllLines(path);
            char[][] terrain = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                terrain[i] = lines[i].ToCharArray();
            }
            int[] result = Simulation.DryGround(terrain);
            System.Console.WriteLine("---------------------");
            System.Console.Write("Wynik: ");
            foreach (int i in result)
                System.Console.Write(i + " ");
            Console.ReadKey();

        }
    }
}
