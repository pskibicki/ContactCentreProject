using System.Collections.Generic;
using System.Linq;
using ContactCentre.Models.Enums;

namespace ContactCentre.Models
{
    public class HierarchyDefinition
    {
        public List<HierarchyLevel> HierarchyLevels { get; set; } = new();

        public void AddLevel(HierarchyLevel hierarchyLevel)
        {
            var last = HierarchyLevels.LastOrDefault();
            if (last != null)
            {
                last.NextLevel = hierarchyLevel; 
            }

            HierarchyLevels.Add(hierarchyLevel);
        }

        public HierarchyLevel GetDefaultLevel()
        {
            return HierarchyLevels.FirstOrDefault() ?? new HierarchyLevel(LevelEnum.Agent);
        }
    }
}
