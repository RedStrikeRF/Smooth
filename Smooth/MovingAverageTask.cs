using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
        var result = 0.0; var sum = 0.0;
        Queue<double> lastYs = new Queue<double>();

        foreach (var dataPoint in data)
        {
            if (lastYs.Count < windowWidth)
            {
                sum += dataPoint.OriginalY;
                result = sum / (lastYs.Count + 1);
            }
            else
                result += (dataPoint.OriginalY - lastYs.Dequeue()) / windowWidth;

            yield return dataPoint.WithAvgSmoothedY(result);

            lastYs.Enqueue(dataPoint.OriginalY);
        }
    }
}