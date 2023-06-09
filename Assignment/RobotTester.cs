using System;
using System.Reflection;
using System.Xml.Linq;
using Assignment.InterfaceCommand;

namespace Assignment
{
    public class RobotTester
    {
        private Robot _robot;
        private List<string> _supportCommands;
        private int _totalCommandCount;
        public RobotTester()
        {
            _totalCommandCount = 6;
            _supportCommands = new List<string>();
            InitCommandNames();
            
            _robot = new Robot(_totalCommandCount);
        }

        public void TestRobot()
        {
            Console.WriteLine($"Give {_totalCommandCount} commands to the robot. Possible commands are:");
            
            foreach (string item in _supportCommands)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");

            LoadCommandToRobot();
            _robot.Run();
        }

        private void LoadCommandToRobot()
        {
            int selectedCommandCount = 0;
            while (selectedCommandCount < _totalCommandCount)
            {
                string commandName = Console.ReadLine() ?? "";
                if (!CheckCommand(commandName))
                {
                    Console.WriteLine("Invalid command, try again!");
                    continue;
                }

                RobotCommand command = CreateCommandByName(commandName);
                if (!_robot.LoadCommand(command))
                {
                    Console.WriteLine("Over the command limit.");
                    break;
                }
                ++selectedCommandCount;
            }
        }

        private bool CheckCommand(string commandName)
        {
            return _supportCommands.Contains(commandName);
        }

        private RobotCommand CreateCommandByName(string name)
        {
            string className = $"Assignment.InterfaceCommand.{name}Command";
            Type classType = Type.GetType(className) ?? typeof(RobotCommand);
            return (RobotCommand)Activator.CreateInstance(classType);
        }

        private void InitCommandNames()
        {
            string namespaceName = "Assignment.InterfaceCommand";
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in types)
            {
                if (type.Namespace == namespaceName && type.IsClass)
                {
                    string commandName = type.Name.Replace("Command", string.Empty);
                    _supportCommands.Add(commandName);
                }
            }
        }
    }
}

