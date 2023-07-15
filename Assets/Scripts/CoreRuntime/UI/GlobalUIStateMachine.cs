using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUIStateMachine : BaseStateMachine
{
    public GameState GameState { get; private set; }
    public TransitionState TransitionState { get; private set; }
    public int CurrentLevel { get; set; }

    public GameObject PauseMenuUI
    {
        get => _pauseMenuUI;
        set => _pauseMenuUI = value;
    }
    public GameObject RestartMenuUI
    {
        get => _restartMenuUI;
        set => _restartMenuUI = value;
    }
    public Text RestartText
    {
        get => _restartText;
        set => _restartText = value;
    }
    public Button NextLevelButtonUI
    {
        get => _nextLevelButtonUI;
        set => _nextLevelButtonUI = value;
    }

    public Button RestartButtonUI
    {
        get => _restartButtonUI;
        set => _restartButtonUI = value;
    }

    public Button ResumeButtonUI
    {
        get => _resumeButtonUI;
        set => _resumeButtonUI = value;
    }

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _restartMenuUI;
    [SerializeField] private Text _restartText;
    [SerializeField] private Button _nextLevelButtonUI;
    [SerializeField] private Button _restartButtonUI;
    [SerializeField] private Button _resumeButtonUI;

    private void Awake()
    {
        CurrentLevel = 0;
        GameState = new GameState();
        TransitionState = new TransitionState();
    }

    private void Start()
    {
        SetState(GameState);
    }
    
    
}
