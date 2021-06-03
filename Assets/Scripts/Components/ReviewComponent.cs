using Components;
using States;
using UnityEngine;
using UnityEngine.UI;

public class ReviewComponent : MonoBehaviour
{
    [SerializeField] private StarComponent[] _stars;
    [SerializeField] private Text _reviewerName;
    [SerializeField] private Text _reviewText;

    public void SetReviewMark(int mark)
    {
        foreach (var star in _stars)
        {
            if (mark / 2 > 0)
            {
                mark -= 2;
                star.CurrentStarState = StarState.Full;
            } else if (mark == 1)
            {
                mark = 0;
                star.CurrentStarState = StarState.Half;
            }
            else
            {
                star.CurrentStarState = StarState.Empty;
            }
        }
    }

    public void SetReviewerName(string name)
    {
        _reviewerName.text = name;
    }

    public void SetReviewText(string text)
    {
        _reviewText.text = text;
    }
    
    private void Awake()
    {
        SetReviewMark(0);
        SetReviewerName("Залупное издание");
        SetReviewText("Залупа конская, а не игра!");
    }
}
