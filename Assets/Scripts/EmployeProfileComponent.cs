using System;
using DefaultNamespace;
using Definitions;
using UnityEngine;
using UnityEngine.UI;

public class EmployeProfileComponent : MonoBehaviour
{
    [SerializeField] private Toggle _employeSelector;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Text _employeName;
    [SerializeField] private Transform _cardsPivot;
    [SerializeField] private GameObject _cardPrefab;

    public EmployeDefinition Definition { get; private set; }
    public bool IsSelected { get; private set; }

    public void SetDefinition(EmployeDefinition definition)
    {
        Definition = definition;
        _employeName.text = definition.Name;
        _avatarImage.sprite = definition.Avatar;
        SetCards( definition.Cards );

        _employeSelector.isOn = GameController.Instance.PlayerState.Team.ContainsKey(definition.Name);
    }

    private void SetCards(CardDefinition[] cards)
    {
        foreach (var cardDefinition in cards)
        {
            var cardObject = Instantiate(_cardPrefab, _cardsPivot);
            var cardComponent = cardObject.GetComponent<MiniCardComponent>();
            cardComponent.SetCardDefinition(cardDefinition);
        }
    }

    private void SelectEmploye(bool isSelected)
    {
        IsSelected = isSelected;
        _backgroundImage.color = isSelected ? _selectedColor : _unselectedColor;

        if (isSelected)
        {
            GameController.Instance.PlayerState.Team[Definition.Name] = Definition;
        }
        else
        {
            GameController.Instance.PlayerState.Team.Remove(Definition.Name);
        }
    }
    
    private void Awake()
    {
        _employeSelector.onValueChanged.AddListener(SelectEmploye);
    }

    private void OnDestroy()
    {
        _employeSelector.onValueChanged.RemoveListener(SelectEmploye);
    }
}