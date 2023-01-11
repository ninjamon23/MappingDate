namespace MappingDate
{
    public class DrillList
    {
        public string TagReferenceId { get; set; }
        public string ScheduleTaskId { get; set; }
        public List<DrillItem> DrillItems { get; set; }

        public static List<DrillList> GetOpenDrills()
        {
            return new List<DrillList> { new DrillList
            {
                TagReferenceId = "tag fire drill",
                ScheduleTaskId = "schedule 1",
                DrillItems = new List<DrillItem> { new DrillItem
                    {
                        Title = "Fire Drill",
                        DueDate = new DateTime(2022,3,10)
                    },
                        new DrillItem
                    {
                        Title = "Fire Drill",
                        DueDate = new DateTime(2022,4,10)
                    }
                }
            }
            };
        }
        public static List<DrillList> GetClosedDrills()
        {
            return new List<DrillList> { new DrillList
{
    TagReferenceId = "tag fire drill",
    ScheduleTaskId = "schedule 1",
    DrillItems = new List<DrillItem> { new DrillItem
        {
            Title = "Fire Drill",
            CompletedDate = new DateTime(2022,1,11),
            LinkId = "form submission "
        },
        new DrillItem
        {
            Title = "Fire Drill",
            CompletedDate = new DateTime(2022,2,10)
        }
    }
}
            };
        }
    }

    public class DrillItem
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string LinkId { get; set; }
    }

    public class ScheduleTask
    {
        public string ScheduleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Interval SchedInterval { get; set; }

        public enum Interval
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        }

        public static ScheduleTask GetSchedule()
        {
            return new ScheduleTask
            {
                SchedInterval = ScheduleTask.Interval.Monthly,
                StartDate = new DateTime(2022, 1, 10),
                EndDate = new DateTime(2023, 1, 10),
                ScheduleId = "schedule 1"
            };
        }
    }
}
