using System;
using Definitions;
using UnityEngine;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private Text _text;

    public CardDefinition Definition { get; private set; }

    public event Action<CardComponent> OnPlay;
    public event Action<CardComponent> OnDiscard;

    public void SetCardDefinition(CardDefinition definition)
    {
        Definition = definition;
        _text.text = definition.Name;
    }

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayClickHandler);
        _discardButton.onClick.AddListener(OnDiscardClickHandler);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayClickHandler);
        _discardButton.onClick.RemoveListener(OnDiscardClickHandler);
    }

    private void OnPlayClickHandler()
    {
        OnPlay?.Invoke(this);
    }
    
    private void OnDiscardClickHandler()
    {
        OnDiscard?.Invoke(this);
    }
}
