using System.Collections.Generic;

public class Schedule_Berthar : ScheduleHandler
{
    protected override void SetDailyPersonalSchedules()
    {
        List<Task> TasksList_Day1 = new()
        {
            new Task(0830, Utils.Instance.GetTavern())
        };

        personalSchedule = new(TasksList_Day1);
    }

    protected override void SetDailyJobSchedules()
    {
        base.SetDailyJobSchedules();
    }
}
