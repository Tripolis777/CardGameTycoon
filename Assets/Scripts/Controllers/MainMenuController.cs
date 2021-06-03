using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _chooseTeamButton;
        [SerializeField] private InputField _projectNameInput;
        [SerializeField] private Button _startProjectButton;
        [SerializeField] private ProjectPanelLevelController _projectLevelPanelLevel;
        
        private void Awake()
        {
            _startProjectButton.onClick.AddListener(StartProjectClick);
            _chooseTeamButton.onClick.AddListener(ChooseTeamClick);
            _projectNameInput.onValueChanged.AddListener(OnProjectNameChanged);
            
            _projectNameInput.text = GameController.Instance.PlayerState.ProjectName;
        }

        private void OnDestroy()
        {
            _startProjectButton.onClick.RemoveListener(StartProjectClick);
            _chooseTeamButton.onClick.RemoveListener(ChooseTeamClick);
            _projectNameInput.onValueChanged.RemoveListener(OnProjectNameChanged);
        }

        private void OnProjectNameChanged(string projectName)
        {
            GameController.Instance.PlayerState.ProjectName = projectName;
        }

        private void ChooseTeamClick()
        {
            GameController.Instance.State = GameState.CommandMenu;
        }

        private void StartProjectClick()
        {
            GameController.Instance.State = GameState.Play;
        }
    }
}