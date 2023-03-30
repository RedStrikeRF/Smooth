using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
        var isFirstElement = true;
        var lastPoint = 0.0;

        foreach (DataPoint dataPoint in data)
        {
            lastPoint = alpha * dataPoint.OriginalY + (1 - alpha) * lastPoint;
            if (isFirstElement)
            {
                lastPoint = dataPoint.OriginalY;
                isFirstElement = false;
                lastPoint = alpha * dataPoint.OriginalY + (1 - alpha) * lastPoint;
            }

            yield return dataPoint.WithExpSmoothedY(lastPoint);
        }
    }
}
