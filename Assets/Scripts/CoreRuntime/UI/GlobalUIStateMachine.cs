using System;
using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalUIStateMachine : BaseStateMachine
{
    public GameState GameState { get; private set; }
    public TransitionState TransitionState { get; private set; }
    public int CurrentLevel { get; set; }
    public bool HasNextLevel { get; private set; }
    public const int LEVEL_COUNT = 2;

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
        CurrentLevel = 1;
        GameState = new GameState();
        TransitionState = new TransitionState();
        HasNextLevel = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (int.TryParse(scene.name.Substring(0, 1), out var sceneIndex) && sceneIndex >= LEVEL_COUNT + 1 && mode == LoadSceneMode.Additive)
        {
            HasNextLevel = false;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        SetState(GameState);
    }
    
    
}
