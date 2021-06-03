using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
    public class CardDefinition : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;

        public int FeatureScore;
        public int DesignScore;
        public int BugsScore;
        public int DiscardBugsScore;
        public int CashAffection;
    }
}