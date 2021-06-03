using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameScoreController : MonoBehaviour
    {
        [SerializeField] private Slider _featureSlider;
        [SerializeField] private Slider _designSlider;
        [SerializeField] private Slider _bugsSlider;
        [SerializeField] private Text _cashScoreText;
        [SerializeField] private Text _featureCount;
        [SerializeField] private Text _designPercent;
        [SerializeField] private Text _bugsCount;
        [SerializeField] private int _startCash;
        
        private int _featureScore;
        private int _designScore;
        private int _bugsScore;
        private int _cash;

        public int FeatureCompletePoints = 10;
        public int DesignCompletePoints = 30;

        public static GameScoreController Instance { get; private set; }
        
        public int FeatureScore
        {
            get => _featureScore;
            set
            {
                _featureScore = Math.Max(value, 0);
                _featureSlider.value = ((float) _featureScore / FeatureCompletePoints) % 1.0f;
                _featureCount.text = (_featureScore / FeatureCompletePoints).ToString();
            }
        }

        public int DesignScore
        {
            get => _designScore;
            set
            {
                _designScore = Math.Max(value, 0);
                _designSlider.value = Mathf.Clamp01((float) _designScore / DesignCompletePoints);

                _designPercent.text = $"{(int) (_designSlider.value * 100)}%";
            }
        }

        public int BugsScore
        {
            get => _bugsScore;
            set
            {
                _bugsScore = Math.Max(value, 0);

                var featureScore = Math.Max(_featureScore, 1);
                _bugsSlider.value = Mathf.Clamp01((float) _bugsScore / featureScore);
                _bugsCount.text = _bugsScore.ToString();
            }
        }
        public int Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                _cashScoreText.text = $"${_cash}";
            }
        }

        private void Awake()
        {
            Instance = this;
            Cash = _startCash;
        }
    }
}