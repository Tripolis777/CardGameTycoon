using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _cardDrawController; 
    [SerializeField] private GameObject _gameScoreController;
    [SerializeField] private GameObject _startMenuPrefab;
    [SerializeField] private GameObject _chooseTeamPrefab;
    [SerializeField] private GameObject _endGamePrefab;
    
    private List<GameObject> _currentControllers = new List<GameObject>();
    private GameState _state;

    public PlayerState PlayerState { get; private set; }

    public GameState State
    {
        get => _state;
        set
        {
            if (_state == value)
            {
                return;
            }

            _state = value;
            foreach (var controller in _currentControllers)
            {
                Destroy(controller);
            }
            _currentControllers.Clear();

            ChangeState();
        }
    }
    
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        PlayerState = new PlayerState();
        StartGame();
    }

    private void ChangeState()
    {
        switch (State)
        {
            case GameState.StartMenu:
                CreateController(_startMenuPrefab);
                break;
            case GameState.CommandMenu:
                CreateController(_chooseTeamPrefab);
                break;
            case GameState.Play:
                CreateController(_cardDrawController);
                CreateController(_gameScoreController);
                break;
            case GameState.End:
                CreateController(_endGamePrefab);
                break;
            default:
                return;
        }
    }

    private void CreateController(GameObject controller)
    {
        _currentControllers.Add(Instantiate(controller, transform));
    }
    
    private void StartGame()
    {
        _state = GameState.StartMenu;
        ChangeState();
    }
}
