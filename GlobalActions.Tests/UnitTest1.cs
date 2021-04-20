using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GlobalActions.Models;
using NUnit.Framework;

namespace GlobalActions.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        public void Test1()
        {
            var scripts = new ScriptsList();
            scripts.Add("1");
            scripts.Edit("1", script =>
            {
                script.NodePipe = new Node
                {
                    ActionType = ActionType.Keyboard,
                };

                script.HotKey = new HotKey
                {
                    Key = 113,
                };

                script.Mode = ScriptMode.Single;
            });
            scripts.ToggleActive("1");

            Task.Run(() =>
            {
                while (HotKeyHandler.GetHotKey(out int hotKey))
                {
                    if (hotKey != default)
                    {
                        scripts.Toggle("1");
                    }
                }
            });

            // Thread.Sleep(5000);

            Console.ReadKey();
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
                var files = Directory.GetFiles("/dev/input/by-path/");
                // foreach (var file in files)
                // {
                using var fileStream = File.OpenRead("/dev/input/by-path/pci-0000:00:14.0-usb-0:8:1.1-event-mouse");
                using var streamReader = new StreamReader(fileStream);
                while (true)
                {
                    var str = streamReader.Read();
                    Console.WriteLine(str);
                }
                // }
        }
    }
}