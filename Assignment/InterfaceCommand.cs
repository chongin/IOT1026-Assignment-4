namespace Assignment.InterfaceCommand;

public interface RobotCommand
{
    void Run(Robot robot); // Interfaces are public and abstract by default.
}

public class OffCommand : RobotCommand
{
    public void Run(Robot robot) => robot.IsPowered = false;
}

public class OnCommand : RobotCommand
{
    public void Run(Robot robot) => robot.IsPowered = true;
}

public class WestCommand : RobotCommand
{
    public void Run(Robot robot) { if (robot.IsPowered) robot.X--; }
}

public class EastCommand : RobotCommand
{
    public void Run(Robot robot) { if (robot.IsPowered) robot.X++; }
}

public class SouthCommand : RobotCommand
{
    public void Run(Robot robot) { if (robot.IsPowered) robot.Y--; }
}

public class NorthCommand : RobotCommand
{
    public void Run(Robot robot) { if (robot.IsPowered) robot.Y++; }
}

public class ResetCommand : RobotCommand
{
    public void Run(Robot robot) { if (robot.IsPowered) robot.X = 0; robot.Y = 0; }
}

// Implement remaining tests (easy & fast)
// Write docs for Run & LoadCommand methids in Robot class (easyish and fast)
// Write your own RobotCommand 
// (Optional) If you have time you can write a RobotTester class as described instructions