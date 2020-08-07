using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock_files
    {
    class Program
        {
        static List<string> Directories = new List<string>();
        static List<string> Files = new List<string>();

        /// <summary>
        /// Main method, the starting point of our application
        /// </summary>
        /// <param name="args">Console arguments</param>
        static void Main(string[] args)
            {
            //Check if we recieved any arguments
            if (args.Length != 0)
                {
                getSubdirectories(args[0]);
                }
            else
                {
                Console.Write("Enter a path to find subdirectories: ");
                getSubdirectories(Console.ReadLine());
                }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone\n{0} directories found!", Directories.Count);
            Pause(true);
            }

        /// <summary>
        /// A recursive method to get all subdirectories within a directory
        /// </summary>
        /// <param name="dir">where to look</param>
        static void getSubdirectories(string dir)
            {
            try
                {
                string[] directories = Directory.GetDirectories(dir);

                foreach (string directory in directories)
                    {
                    Console.WriteLine(directory);
                    Directories.Add(directory);
                    getSubdirectories(directory);
                    }
                }
            catch (Exception) { }
            }

        static void Pause(bool intercept)
            {
            Console.ReadKey(intercept);
            }
        static void Pause()
            {
            Console.ReadKey();
            }
        }
    }
