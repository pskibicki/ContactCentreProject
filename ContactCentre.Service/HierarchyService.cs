using System.Linq;
using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Service
{
    public class HierarchyService : IHierarchyService
    {
        private readonly HierarchyDefinition _hierarchyDefinition;

        public HierarchyLevel CurrentLevel { get; set; }

        public HierarchyService()
        {
            _hierarchyDefinition = new HierarchyDefinition();
            _hierarchyDefinition.AddLevel(new HierarchyLevel(LevelEnum.Agent));
            _hierarchyDefinition.AddLevel(new HierarchyLevel(LevelEnum.Supervisor));
            _hierarchyDefinition.AddLevel(new HierarchyLevel(LevelEnum.GeneralManager, 1));

            CurrentLevel = _hierarchyDefinition.GetDefaultLevel();
        }

        public int GetLimit(LevelEnum level)
        {
            return _hierarchyDefinition.HierarchyLevels.FirstOrDefault(x => x.Level == level)?.Limit ?? 0;
        }

        public bool MoveLevelUp()
        {
            if (!IsHighestLevel())
            {
                CurrentLevel = CurrentLevel.NextLevel;
                return true;
            }

            return false;
        }

        public void ResetLevel()
        {
            CurrentLevel = _hierarchyDefinition.GetDefaultLevel();
        }

        private bool IsHighestLevel()
        {
            return CurrentLevel.NextLevel == null;
        }
    }
}
