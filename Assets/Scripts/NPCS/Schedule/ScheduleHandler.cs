using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(NPCMovement))]
public class ScheduleHandler : MonoBehaviour
{
    private NPCMovement movement;

    protected DaySchedule personalSchedule;
    protected DaySchedule jobSchedule;

    public List<Task> TotalTasksForTheDay;
    public Task currentTask;


    private void OnEnable()
    {
        TimeManager.OnDayChanged += ConfigureNPCSchedule;
        TimeManager.OnMinuteChanged += TryPerformTask;
    }

    private void OnDisable()
    {
        TimeManager.OnDayChanged -= ConfigureNPCSchedule;
        TimeManager.OnMinuteChanged -= TryPerformTask;
    }

    protected virtual void SetDailyPersonalSchedules()
    {
        // Meant to be overriden by each Character.
    }

    protected virtual void SetDailyJobSchedules()
    {
        // Meant to be overriden by each Character.
    }

    void Start()
    {
        movement = GetComponent<NPCMovement>();
        //TasksForTheDay = new();
    }

    // Every day configure the NPC Schedule 
    private void ConfigureNPCSchedule()
    {
        SetDailyPersonalSchedules();
        SetDailyJobSchedules();

        if (jobSchedule == null)
        {
            TotalTasksForTheDay = personalSchedule.Tasks;
            currentTask = TotalTasksForTheDay[0];
        }
        else
        {
            TotalTasksForTheDay = jobSchedule.Tasks;

            // Add the Personal Tasks that start After the last Job Task.
            foreach (var personalTask in personalSchedule.Tasks)
            {
                if (personalTask.StartTime > jobSchedule.Tasks[^1].StartTime)
                {
                    TotalTasksForTheDay.Add(personalTask);
                }
            }

            currentTask = TotalTasksForTheDay[0];
        }

    }

    private void TryPerformTask()
    {
        print($"{TimeManager.FullTime} ---  {currentTask.StartTime}");

        if (TimeManager.FullTime == currentTask.StartTime)
        {
            // do the task
            // More there
            movement.GoToTargetNode(currentTask.DestinationNode);
            // Do stuff
            //  ...
            //
            // remove it when done (?)
            TotalTasksForTheDay.RemoveAt(0);
            if (TotalTasksForTheDay.Count > 0)
                currentTask = TotalTasksForTheDay[0];
        }
    }
}