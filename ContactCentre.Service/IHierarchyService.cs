using ContactCentre.Models;
using ContactCentre.Models.Enums;

namespace ContactCentre.Service
{
    public interface IHierarchyService
    {
        bool MoveLevelUp();
        int GetLimit(LevelEnum level);
        HierarchyLevel CurrentLevel { get; set; }
        void ResetLevel();
    }
}