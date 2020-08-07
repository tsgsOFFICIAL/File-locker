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
            Console.Title = "File locker v.1.0";
            Console.ForegroundColor = ConsoleColor.White;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            //Check if we recieved any arguments
            if (args.Length != 0)
                {
                stopwatch.Start();
                getSubdirectories(args[0]);
                }
            else
                {
                Console.WriteLine("No command line arguments were found, do you want to specify a directory to search in? (Y/N)");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("If you choose NOT to specify, it is searching ALL drives and ALL directories, this will eat a lot of ram at once!");
                Console.ForegroundColor = ConsoleColor.White;
                switch (Console.ReadKey(true).KeyChar.ToString().ToUpper())
                    {
                    case "N":
                        string[] drives = Directory.GetLogicalDrives();
                        foreach (string drive in drives)
                            {
                            stopwatch.Start();
                            getSubdirectories(drive);
                            }
                        break;
                    default:
                        Console.Write("\nEnter a path to find subdirectories: ");
                        string _dir = Console.ReadLine();
                        stopwatch.Start();
                        getSubdirectories(_dir);
                        break;
                    }
                }
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone\n{0} directories found in just {1} seconds!", Directories.Count, stopwatch.Elapsed.TotalSeconds);
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
