using System.Collections.Generic;

public class Schedule_Fendrel : ScheduleHandler
{
    protected override void SetDailyPersonalSchedules()
    {
        List<Task> TasksList = new()
        {
            new Task(0830, Utils.Instance.GetWoodcuttersLodge()),
            new Task(1400, Utils.Instance.GetTavern())
        };

        personalSchedule = new(TasksList);
    }

    protected override void SetDailyJobSchedules()
    {
        jobSchedule = new(JobsSchedules.GetWoodCuttersSchedule(TimeManager.DayOfTheWeek));
    }
}
