using Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MiniCardComponent : MonoBehaviour
    {
        [SerializeField] private Image _cardIcon;

        private CardDefinition _definition;

        public void SetCardDefinition(CardDefinition definition)
        {
            _definition = definition;
            _cardIcon.sprite = definition.Icon;
        }
    }
}