using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public enum ProjectLevel
    {
        Small, 
        Medium,
        Big
    }
    
    public class ProjectPanelLevelController : MonoBehaviour
    {
        [SerializeField] private Button _smallProjectButton;
        [SerializeField] private Button _mediumProjectButton;
        [SerializeField] private Button _bigProjectButton;
        [SerializeField] private Text _projectLevelLabel;

        private ProjectLevel _projectLevel;
        public ProjectLevel ProjectLevel
        {
            get => _projectLevel;
            private set
            {
                _projectLevel = value;
                _projectLevelLabel.text = _projectLevel.ToString();
                GameController.Instance.PlayerState.ProjectLevel = _projectLevel;
            } 
        }

        private void Awake()
        {
            ProjectLevel = GameController.Instance.PlayerState.ProjectLevel;
            
            _smallProjectButton.onClick.AddListener(SmallProjectClick);
            _mediumProjectButton.onClick.AddListener(MediumProjectClick);
            _bigProjectButton.onClick.AddListener(BigProjectClick);
        }

        private void OnDestroy()
        {
            _smallProjectButton.onClick.RemoveListener(SmallProjectClick);
            _mediumProjectButton.onClick.RemoveListener(MediumProjectClick);
            _bigProjectButton.onClick.RemoveListener(BigProjectClick);
        }

        private void BigProjectClick()
        {
            ProjectLevel = ProjectLevel.Big;
        }

        private void MediumProjectClick()
        {
            ProjectLevel = ProjectLevel.Medium;
        }

        private void SmallProjectClick()
        {
            ProjectLevel = ProjectLevel.Small;
        }
    }
}