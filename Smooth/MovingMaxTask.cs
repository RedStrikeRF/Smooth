using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var counter = 1;
        LinkedList<double> max = new LinkedList<double>();
        Queue<double> windowedNumbers = new Queue<double>();

        foreach (DataPoint dataPoint in data)
        {
            if (counter <= windowWidth)
                counter++;
            else
            if (max.Count == 0)
                windowedNumbers.Dequeue();
            else if (max.First.Value == windowedNumbers.Dequeue())
                max.RemoveFirst();
            
            windowedNumbers.Enqueue(dataPoint.OriginalY);

            while (max.Count > 0 && max.Last.Value <= dataPoint.OriginalY)
                max.RemoveLast();

            max.AddLast(dataPoint.OriginalY);
            yield return dataPoint.WithMaxY(max.First.Value);
        }
    }
}