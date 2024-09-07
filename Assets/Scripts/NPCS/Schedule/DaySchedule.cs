using System.Collections.Generic;

public class DaySchedule
{
    private List<Task> _tasks;
    public List<Task> Tasks { get => _tasks; set => _tasks = value; }

    public DaySchedule(List<Task> tasks)
    {
        Tasks = tasks;
    }
}

