using System.Collections.Generic;
using Definitions;

namespace DefaultNamespace
{
    public class PlayerState
    {
        public string ProjectName;
        public ProjectLevel ProjectLevel = ProjectLevel.Small;
        public Dictionary<string, EmployeDefinition> Team = new Dictionary<string, EmployeDefinition>();
    }
}