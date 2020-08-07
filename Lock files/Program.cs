using System;
using System.IO;
using System.Collections.Generic;

namespace Lock_files
    {
    class Program
        {
        static List<string> Directories = new List<string>();
        static List<string> Files = new List<string>();
        static int LockedFiles = 0;

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
                Console.WriteLine("If you choose NOT to specify, it is searching ALL drives and ALL directories, this will eat a lot of ram at once!\n");
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
            Console.Write("\nDo you wish to find");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" EVERY ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("file within the {0} directories? (Y/N)", Directories.Count);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning, this could take a moment!");
            Console.ForegroundColor = ConsoleColor.White;

            switch (Console.ReadKey(true).KeyChar.ToString().ToUpper())
                {
                case "Y":
                    stopwatch.Restart();
                    for (int i = 0; i < Directories.Count; i++)
                        {
                        FindAllFilesWithin(Directories[i]);
                        }
                    break;
                default:
                    Environment.Exit(0);
                    break;
                }

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone\n{0} files found in in just {1} seconds!", Files.Count, stopwatch.Elapsed.TotalSeconds);

            Console.WriteLine("Do you wish to lock all the files? (Y/N)");

            switch (Console.ReadKey(true).Key.ToString().ToUpper())
                {
                case "Y":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    for (int i = 0; i < Files.Count; i++)
                        {
                        LockFile(Files[i]);
                        Console.WriteLine(Files[i] + " is now locked >:D");
                        }
                    break;
                default:
                    Environment.Exit(0);
                    break;
                }

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone\n{0} files locked in in just {1} seconds!", LockedFiles, stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Press any key to unlock it all");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        static void FindAllFilesWithin(string dir)
            {
            try
                {
                string[] files = Directory.GetFiles(dir, "*.*");
                foreach (string file in files)
                    {
                    Files.Add(file);
                    Console.WriteLine(file);
                    }
                }
            catch (Exception)
                { }
            }

        /// <summary>
        /// Lock the file
        /// </summary>
        /// <param name="file"></param>
        static void LockFile(string file)
            {
            try
                {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                LockedFiles++;
                }
            catch (Exception)
                { }
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
