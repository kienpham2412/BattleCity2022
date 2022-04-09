using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static System.Random random = new System.Random();

    /// <summary>
    /// Shuffle all members of the list
    /// </summary>
    /// <param name="list">The list to be shuffled</param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T>(this IList<T> list)
    {
        int count = list.Count;
        while (count > 1)
        {
            count--;
            int index = random.Next(count + 1);
            T value = list[index];
            list[index] = list[count];
            list[count] = value;
        }
    }
}
