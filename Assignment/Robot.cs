// Change to 'using Assignment.InterfaceCommand' when you are ready to test your interface implementation
using Assignment.InterfaceCommand;

namespace Assignment;

public class Robot
{
    // These are properties, you can replace these with traditional getters/setters if you prefer.
    public int NumCommands { get; }

    // These properties are not good! The setter allows
    // us to move the robot even if it off
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }

    private const int DefaultCommands = 6;
    // An array is not the preferred data structure here.
    // You will get bonus marks if you replace the array with the preferred data structure
    // Hint: It is NOT a list either,
    private readonly Queue<RobotCommand> _commands; //I change the array to Queue
    private int _commandsLoaded = 0;

    public override string ToString()
    {
        return $"[{X} {Y} {IsPowered}]";
    }

    // You should not have to use any of the methods below here but you should
    // provide XML documentation for the argumented constructor, the Run method and one
    // of the LoadCommand methods.
    public Robot() : this(DefaultCommands) { }

    /// <summary>
    /// Constructor that initializes the robot with the capacity to store a user specified
    /// number of commands
    /// </summary>
    /// <param name="numCommands">The maximum number of commands the robot can store</param>
    public Robot(int numCommands)
    {
        _commands = new Queue<RobotCommand>();
        NumCommands = numCommands;
    }

    /// <summary>
    /// Runs the loaded commands on the robot.
    /// </summary>
    public void Run()
    {
        while(_commands.Count>0)
        {
            RobotCommand command = _commands.Dequeue();
            command.Run(this);
            --_commandsLoaded;
            Console.WriteLine(this);
        }
    }

    /// <summary>
    /// Load a command into robot's command list
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public bool LoadCommand(RobotCommand command)
    {
        if (_commandsLoaded >= NumCommands)
            return false;
        _commands.Enqueue(command);
        _commandsLoaded++;
        return true;
    }
}
