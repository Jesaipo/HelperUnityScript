using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyBox;
using System.Globalization;
using System;

public class MyRandomSeedSetter : MonoBehaviour
{
    public bool m_SetOnStart = false;
    public bool m_SetWeekSeed = false;
    [ConditionalField(nameof(m_SetWeekSeed), inverse: true)]
    public int m_Seed = 0;


    private void Start()
    {
        if(m_SetWeekSeed)
        {
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            m_Seed = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        }

        if (m_SetOnStart)
            SetSeed();
    }

    public void SetSeed()
    {
        MyRandom.SetRandomSeed(m_Seed);
    }
}

public static class MyRandom
{
    private static System.Random m_SeededRandomGenerator = new System.Random();
    private static System.Random m_FullRandomGenerator = new System.Random();

    public static void SetRandomSeed(int seed)
    {
        if(seed == 0)
        {
            seed = GetRandomInt();
        }
        m_SeededRandomGenerator = new System.Random(seed);
    }

    public static bool GetSeededRandomBool()
    {
        return GetSeededRandomInt() % 2 == 0;
    }

    public static int GetSeededRandomInt()
    {
        return m_SeededRandomGenerator.Next();
    }

    public static float GetSeededRandomFloat()
    {
        return (float)m_SeededRandomGenerator.NextDouble();
    }

    public static float GetSeededRandomUnitFloat()
    {
        double random = m_SeededRandomGenerator.NextDouble();
        return (float)(random - (int)(random));
    }

    public static double GetSeededRandomDouble()
    {
        return m_SeededRandomGenerator.NextDouble();
    }

    public static int GetSeededRandomIntRange(int min, int max)
    {
        return m_SeededRandomGenerator.Next(min, max);
    }

    public static float GetSeededRandomFloatRange(float min, float max)
    {
        return (float)(m_SeededRandomGenerator.Next()) % (max - min) + min;
    }

    public static List<T> SeededShuffleList<T>(List<T> listToSuffle)
    {
        return listToSuffle.OrderBy(a => GetSeededRandomUnitFloat()).ToList();
    }

    public static T SeededGetElementInList<T>(List<T> listToSuffle)
    {
        return listToSuffle[GetSeededRandomIntRange(0, listToSuffle.Count)];
    }


    public static bool GetRandomBool()
    {
        return GetRandomInt() % 2 == 0;
    }

    public static int GetRandomInt()
    {
        return m_FullRandomGenerator.Next();
    }

    public static float GetRandomFloat()
    {
        return (float)m_FullRandomGenerator.NextDouble();
    }

    public static double GetRandomDouble()
    {
        return m_FullRandomGenerator.NextDouble();
    }

    public static int GetRandomIntRange(int min, int max)
    {
        return m_FullRandomGenerator.Next(min, max);
    }

    public static float GetRandomFloatRange(float min, float max)
    {
        return (float)(m_FullRandomGenerator.Next()) % (max - min) + min;
    }

    public static List<T> RandomShuffleList<T>(List<T> listToSuffle)
    {
        return listToSuffle.OrderBy(a => GetSeededRandomUnitFloat()).ToList();
    }

    public static T GetElementInList<T>(List<T> listToSuffle)
    {
        return listToSuffle[GetRandomIntRange(0, listToSuffle.Count)];
    }
}
