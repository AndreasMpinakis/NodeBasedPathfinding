using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// A list of all the Jobs with their respected schedules for each day of the week.
/// </summary>
public static class JobsSchedules
{
    public static List<Task> GetWoodCuttersSchedule(int dayNumber)
    {
        List<Task> tasksList = new();
        switch (dayNumber)
        {
            case 1:
                // Stand - Be Outside the Lodge.
                tasksList.Add(new Task(0830, Utils.Instance.GetWoodcuttersLodge()));
                // Go to Wood Deposit.
                tasksList.Add(new Task(0900, Utils.Instance.GetWoodcuttersWoodDeposit()));
                // Chop Wood in the Forest.
                tasksList.Add(new Task(0920, Utils.Instance.GetWoodcuttersChoppingPoint()));
                // Return to the Lodge to Deposit the Wood
                tasksList.Add(new Task(1300, Utils.Instance.GetWoodcuttersWoodDeposit()));

                // TODO: Need to find a way to calculate or know when a character will reach the destination and how long will it take maybe ?
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            default:
                break;
        }
        return tasksList;
    }

    public static List<Task> GetTavernKeepersSchedule()
    {
        List<Task> tasksList = new()
        {


        };

        return tasksList;
    }
}
