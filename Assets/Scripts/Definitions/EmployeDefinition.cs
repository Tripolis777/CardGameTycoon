using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Employe", menuName = "ScriptableObjects/Employe", order = 2)]
    public class EmployeDefinition : ScriptableObject
    {
        public string Name;
        public Sprite Avatar;
        public CardDefinition[] Cards;
    }
}