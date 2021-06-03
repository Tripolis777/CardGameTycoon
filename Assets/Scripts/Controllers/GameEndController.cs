using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameEndController : MonoBehaviour
{
    [SerializeField] private Text _projectNameText;
    [SerializeField] private ReviewComponent[] _reviews;
    [SerializeField] private Text _cashText;
    [SerializeField] private Button _closeButton;

    private List<string> _reviewsTexts = new List<string>
    {
        "Та еще ебанина, но поиграть можно.", 
        "Вова, напиши какие-нить номарльные ревьюшки.",
        "В рот ебал я это казино!",
        "Как будто в говне искупался.",
        "Это надо переписать!",
        "Черный экран! Пофикисте плиз!"
    };

    private List<string> _reviewers = new List<string>
    {
        "Антон Залогвинов",
        "IGG",
        "Игрофилия",
        "StartGame",
        "Мамкин блогер",
        "Автор игры"
    };
    
    private void CalculateResults()
    {
        _reviewers.Shuffle();
        _reviewsTexts.Shuffle();
        var result = 0f;
        var i = 0;
        foreach (var review in _reviews)
        {
            var coef = GetRandomReviewMark();
            review.SetReviewMark(Mathf.RoundToInt(coef));
            result += coef * 1000;

            review.SetReviewText(_reviewsTexts[i]);
            review.SetReviewerName(_reviewers[i]);
            i++;
        }

        var integerResult = Mathf.RoundToInt(result);
        GameController.Instance.PlayerState.Cash += integerResult;
        _cashText.text = integerResult.ToString("#,0");
    }

    private float GetRandomReviewMark()
    {
        var featureRand = Random.Range(0.75f, 1.5f);
        var designRand = Random.Range(0.75f, 2f);
        var state = GameController.Instance.PlayerState;
        return Mathf.Log(state.FeatureCount * featureRand + 1) * Mathf.Log10(state.DesignCount * designRand + 1) -
               Mathf.Log(1f / Mathf.Max(1, state.BugsCount), 0.5f);
    }
    
    private void Awake()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _projectNameText.text = GameController.Instance.PlayerState.ProjectName;
    }

    private void Start()
    {
        CalculateResults();
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        GameController.Instance.State = GameState.StartMenu;
    }
}
