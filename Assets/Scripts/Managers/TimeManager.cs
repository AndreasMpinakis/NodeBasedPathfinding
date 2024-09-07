using System;
using System.Threading;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnDayChanged;

    // Properties
    public static int Hour { get; private set; }
    public static int Minute { get; private set; }
    public static int FullTime { get; private set; }
    public static int Day { get; private set; }
    public static int DayOfTheWeek { get { return Day % 7; } }

    [SerializeField] private float tenMinutesToRealTimeSeconds = 1f;
    [SerializeField] private float timer;

    private void Start()
    {
        Day = 1;
        OnDayChanged?.Invoke();

        Hour = 08;
        Minute = 00;
        FullTime = Convert.ToInt32($"{Hour:00}{Minute:00}");

        timer = tenMinutesToRealTimeSeconds;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute += 10;

            if (Minute >= 60)
            {
                Hour++;
                Minute = 00;

                if (Hour == 25)
                {
                    Hour = 01;
                }

                //print($"{"---"}{Hour:00}{Minute:00}");
                FullTime = Convert.ToInt32($"{Hour:00}{Minute:00}");
                //print(FullTime);

                if (Hour == 02)
                {
                    Day++;
                    // Day count changes after 2 in the night but currently nothing else happens
                    OnDayChanged?.Invoke();
                }
            }
            else
            {
                FullTime = Convert.ToInt32($"{Hour:00}{Minute:00}");
            }
            OnMinuteChanged?.Invoke();

            timer = tenMinutesToRealTimeSeconds;
        }
    }
}
