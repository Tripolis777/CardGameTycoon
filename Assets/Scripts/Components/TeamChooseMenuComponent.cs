using DefaultNamespace;
using Definitions;
using UnityEngine;
using UnityEngine.UI;

public class TeamChooseMenuComponent : MonoBehaviour
{
    [SerializeField] private Transform _teamPivot;
    [SerializeField] private EmployeDefinition[] _employeList;
    [SerializeField] private GameObject _employePrefab;
    [SerializeField] private Button _confirmButton;

    private void Awake()
    {
        foreach (var employe in _employeList)
        {
            var employeObj = Instantiate(_employePrefab, _teamPivot);
            employeObj.GetComponent<EmployeProfileComponent>().SetDefinition(employe);
        }
        
        _confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }

    private void OnConfirmButtonClick()
    {
        GameController.Instance.State = GameState.StartMenu;
    }

    private void OnDestroy()
    {
        _confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
    }
}
