using System.Reflection;
using Assignment;
using Assignment.InterfaceCommand;

namespace AssignmentTest
{
    [TestClass]
    public class AssignmentTests
    {
        [TestMethod]
        public void DummyTest()
        {
            Assert.AreNotSame(1, 2);
        }

        [TestMethod]
        public void TestRun_Without_Commands()
        {
            Robot robot = new Robot();

            robot.Run();

            Assert.AreEqual(0, robot.X);
            Assert.AreEqual(0, robot.Y);
            Assert.IsFalse(robot.IsPowered);
        }

        [TestMethod]
        public void TestLoadCommand_LoadedSuccessfully()
        {
            Robot robot = new Robot();
            RobotCommand command = new WestCommand();

            bool result = robot.LoadCommand(command);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoadCommand_CommandLimitExceeded()
        {
            Robot robot = new Robot(1);
            RobotCommand command1 = new WestCommand();
            RobotCommand command2 = new OnCommand();

            bool result1 = robot.LoadCommand(command1);
            bool result2 = robot.LoadCommand(command2);

            Assert.IsTrue(result1);

            // Assert that the second command fails to load because the command limit exceeded
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void TestCheckCommand_CommandExists()
        {
            var tester = new RobotTester();
            var commandName = "West";

            var supportCommandsField = typeof(RobotTester).GetField("_supportCommands", BindingFlags.NonPublic | BindingFlags.Instance);
            supportCommandsField.SetValue(tester, new List<string> { "West" });

            var checkCommandMethod = typeof(RobotTester).GetMethod("CheckCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            bool result = (bool)checkCommandMethod.Invoke(tester, new object[] { commandName });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCheckCommand_CommandDoesNotExist()
        {
            var tester = new RobotTester();
            var commandName = "East";

            var supportCommandsField = typeof(RobotTester).GetField("_supportCommands", BindingFlags.NonPublic | BindingFlags.Instance);
            supportCommandsField.SetValue(tester, new List<string> { "West" });

            var checkCommandMethod = typeof(RobotTester).GetMethod("CheckCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            bool result = (bool)checkCommandMethod.Invoke(tester, new object[] { commandName });

            Assert.IsFalse(result);
        }
    }
}
