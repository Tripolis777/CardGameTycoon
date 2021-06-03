using System.Collections.Generic;
using Definitions;

namespace DefaultNamespace
{
    public class PlayerState
    {
        public string ProjectName;
        public ProjectLevel ProjectLevel = ProjectLevel.Small;
        public Dictionary<string, EmployeDefinition> Team = new Dictionary<string, EmployeDefinition>();

        public int FeatureCount = 0;
        public int DesignCount = 0;
        public int BugsCount = 0;

        public int Cash = 1000;
    }
}