// See https://aka.ms/new-console-template for more information
using MappingDate;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var schedule = ScheduleTask.GetSchedule();

var openDrills = DrillList.GetOpenDrills();

var closedDrills = DrillList.GetClosedDrills();

foreach (var closedDrill in closedDrills)
{
    var drillToCheck = openDrills.Where(c => c.TagReferenceId == closedDrill.TagReferenceId).FirstOrDefault();
    if (drillToCheck == null)
        continue;

    var openDrillNewestDueDate = drillToCheck.DrillItems.OrderBy(c => c.DueDate).Select(c => c.DueDate).FirstOrDefault();

    var processDate = schedule.StartDate;
    var usedTicks = new List<long>();
    while (processDate < openDrillNewestDueDate)
    {
        var processingDrillItems = closedDrill.DrillItems;
        Console.WriteLine($"Processing {processDate}");
        var nearestDiff = processingDrillItems.Where(c => !usedTicks.Contains(Math.Abs((c.CompletedDate.Value - processDate).Ticks)))
            .Min(c => Math.Abs((c.CompletedDate.Value - processDate).Ticks));
        var nearest = closedDrill.DrillItems.Where(c => Math.Abs((c.CompletedDate.Value - processDate).Ticks) == nearestDiff).First();
        nearest.DueDate = processDate;
        // add to usedTicks
        usedTicks.Add(nearestDiff);
        // NOTE: This is from the sched even calculator
        switch (schedule.SchedInterval)
        {
            case ScheduleTask.Interval.Monthly:
                processDate = processDate.AddMonths(1);
                break;
        }
        Console.WriteLine(JsonSerializer.Serialize(nearest));
        Console.WriteLine("------");
    }

}

/// <summary>
/// Get the nearest date from a range of dates.
/// </summary>
/// <param name="dateTime">The target date.</param>
/// <param name="dateTimes">The range of dates to find the nearest date from.</param>
/// <returns>The nearest date to the given target date.</returns>
static DateTime GetNearestDate(DateTime dateTime, params DateTime[] dateTimes)
{
    return dateTime.Add(dateTimes.Min(d => (d - dateTime).Duration()));
}

