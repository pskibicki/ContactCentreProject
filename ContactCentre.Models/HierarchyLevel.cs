using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class HierarchyLevel
    {
        public LevelEnum Level { get; set; }
        public HierarchyLevel NextLevel { get; set; }
        public int? Limit { get; set; }

        public HierarchyLevel(LevelEnum level)
        {
            Level = level;
        }

        public HierarchyLevel(LevelEnum level, int limit)
        {
            Level = level;
            Limit = limit;
        }
    }
}
