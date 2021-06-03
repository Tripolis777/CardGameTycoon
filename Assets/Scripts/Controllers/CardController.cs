using System.Collections.Generic;
using System.Linq;
using Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private Button _drawButton;
        [SerializeField] private Text _cardsCount;
        [SerializeField] private CardDefinition[] _deck;
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private GameObject _handGameObject;
        [SerializeField] private int _startCardsCount;

        private List<CardDefinition> _discardedDeck = new List<CardDefinition>();
        private Stack<CardDefinition> _drawDeck = new Stack<CardDefinition>();
        
        public int DrawCardCost = 100;
        
        private List<CardComponent> _handCards = new List<CardComponent>();

        private void Awake()
        {
            _drawButton.onClick.AddListener(DrawCardHandler);
            var playerState = GameController.Instance.PlayerState;

            _discardedDeck = playerState.Team.SelectMany(x => x.Value.Cards.ToList()).ToList();
            if (_discardedDeck.Count == 0)
            {
                _discardedDeck = _deck.ToList();
            }

            while (_handCards.Count < _startCardsCount)
            {
                DrawCard();
            }
        }

        private void OnDestroy()
        {
            _drawButton.onClick.RemoveListener(DrawCardHandler);
        }

        private void DrawCardHandler()
        {
            DrawCard();
            GameScoreController.Instance.Cash -= DrawCardCost;
        }

        private void DrawCard()
        {
            if (_drawDeck.Count == 0)
            {
                ShuffleDeck();
            }

            var card = _drawDeck.Pop();
            var cardGO = Instantiate(_cardPrefab, _handGameObject.transform);
            var cardComponent = cardGO.GetComponent<CardComponent>();
            cardComponent.SetCardDefinition(card);
            cardComponent.OnPlay += OnPlayCard;
            cardComponent.OnDiscard += OnDiscardCard;
            _handCards.Add(cardComponent);

            _cardsCount.text = _drawDeck.Count.ToString();
        }

        private void OnDiscardCard(CardComponent obj)
        {
            _discardedDeck.Add(obj.Definition);
            GameScoreController.Instance.BugsScore -= obj.Definition.DiscardBugsScore;
            DeleteCard(obj);
        }

        private void OnPlayCard(CardComponent obj)
        {
            var definition = obj.Definition;
            _discardedDeck.Add(definition);
            GameScoreController.Instance.BugsScore += definition.BugsScore;
            GameScoreController.Instance.DesignScore += definition.DesignScore;
            GameScoreController.Instance.FeatureScore += definition.FeatureScore;
            GameScoreController.Instance.Cash += definition.CashAffection;
            
            DeleteCard(obj);
        }

        private void DeleteCard(CardComponent component)
        {
            component.OnPlay -= OnPlayCard;
            component.OnDiscard -= OnDiscardCard;

            _handCards.Remove(component);
            Destroy(component.gameObject);
        }
        
        private void ShuffleDeck()
        {
            var count = _discardedDeck.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var rand = UnityEngine.Random.Range(0, i + 1 );
                var card = _discardedDeck[rand];
                _discardedDeck.RemoveAt(rand);
                _drawDeck.Push(card);
            }
        }
    }
}