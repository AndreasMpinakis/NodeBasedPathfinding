

public class Task
{
    private int _startTime;
    /// <summary>
    /// ex. 1420 means 14:20 
    /// </summary>& 930 means 09:30
    public int StartTime { get => _startTime; set => _startTime = value; }

    private Node _destinationNode;
    public Node DestinationNode { get => _destinationNode; set => _destinationNode = value; }

    public Task(int startTime, Node destinationNode)
    {
        StartTime = startTime;
        DestinationNode = destinationNode;
    }
}
