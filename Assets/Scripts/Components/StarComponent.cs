using States;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class StarComponent : MonoBehaviour
    {
        [SerializeField] private Image _fullStar;
        
        private StarState _starState = StarState.Empty;
    
        public StarState CurrentStarState
        {
            get => _starState;
            set
            {
                _starState = value;
                _fullStar.fillAmount = (float) value / 2;
            }
        }

        private void Awake()
        {
            CurrentStarState = StarState.Empty;
        }
    }
}
