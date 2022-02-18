using ContactCentre.Models.Enums;
using ContactCentre.Service;
using Xunit;

namespace ContactCentre.Tests.Services
{
    public class TestHierarchyService
    {
        [Fact]
        public void ServiceShouldReturnLimitForAgentLevel()
        {
            var hierarchyService = new HierarchyService();

            var limit = hierarchyService.GetLimit(LevelEnum.Agent);

            Assert.Equal(0, limit);
        }

        [Fact]
        public void ServiceShouldReturnLimitForSupervisorLevel()
        {
            var hierarchyService = new HierarchyService();

            var limit = hierarchyService.GetLimit(LevelEnum.Supervisor);

            Assert.Equal(0, limit);
        }

        [Fact]
        public void ServiceShouldReturnLimitForGeneralManagerLevel()
        {
            var hierarchyService = new HierarchyService();

            var limit = hierarchyService.GetLimit(LevelEnum.GeneralManager);

            Assert.Equal(1, limit);
        }

        [Fact]
        public void ServiceShouldHaveDefaultedCurrentLevelToAgent()
        {
            var hierarchyService = new HierarchyService();

            Assert.Equal(LevelEnum.Agent, hierarchyService.CurrentLevel.Level);
        }

        [Fact]
        public void ServiceShouldMoveLevelUpFromAgentToSupervisor()
        {
            var hierarchyService = new HierarchyService();

            hierarchyService.MoveLevelUp();

            Assert.Equal(LevelEnum.Supervisor, hierarchyService.CurrentLevel.Level);
        }

        [Fact]
        public void ServiceShouldMoveLevelUpFromSupervisorToGeneralManager()
        {
            var hierarchyService = new HierarchyService();
            hierarchyService.MoveLevelUp();
            
            hierarchyService.MoveLevelUp();

            Assert.Equal(LevelEnum.GeneralManager, hierarchyService.CurrentLevel.Level);
        }


        [Fact]
        public void ServiceShouldNotMoveLevelUpHigherThanGeneralManager()
        {
            var hierarchyService = new HierarchyService();
            hierarchyService.MoveLevelUp();
            hierarchyService.MoveLevelUp();
            
            hierarchyService.MoveLevelUp();

            Assert.Equal(LevelEnum.GeneralManager, hierarchyService.CurrentLevel.Level);
        }

        [Fact]
        public void ServiceShouldResetLevelToDefaultLevel()
        {
            var hierarchyService = new HierarchyService();
            hierarchyService.MoveLevelUp();
            
            hierarchyService.ResetLevel();

            Assert.Equal(LevelEnum.Agent, hierarchyService.CurrentLevel.Level);
        }
    }
}
