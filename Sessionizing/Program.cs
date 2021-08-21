using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using Sessionizing.Menus;
using Unity;

namespace Sessionizing
{
    class Program
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        
        static void Main(string[] args)
        {
            if (!TryGetFilesToRead(args, out IReadOnlyList<string> filesToRead))
            {
                Console.WriteLine("Invalid input files, exiting...");
                return;
            }
            
            var unityContainer = new UnityContainer();
            var initializer = new Initializer(unityContainer);
            initializer.Initialize(filesToRead);
            var menuGenerator = new MenuGenerator(unityContainer);
            try
            {
                Menu menu = menuGenerator.Generate();
                menu.Run();
            }
            catch (Exception e)
            {
                s_logger.Error(e, "Got an error");
                Console.WriteLine($"Got an error {e.Message}, existing...");
            }
        }

        private static bool TryGetFilesToRead(string[] args, out IReadOnlyList<string> filesToRead)
        {
            List<string> ans = new List<string>();
            filesToRead = null;
            if (args.Length == 0)
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (!File.Exists(arg))
                {
                    return false;
                }
                ans.Add(arg);
            }

            filesToRead = ans;
            return true;
        }
    }
}