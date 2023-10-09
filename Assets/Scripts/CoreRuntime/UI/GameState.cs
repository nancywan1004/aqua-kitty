using System;
using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : IBaseState
{
    // class variable not instance variable
    public static bool GameIsPaused = false;
    private GlobalUIStateMachine _stateMachine;
    private const string GARBAGE_SPAWNER_SETTINGS_DIRECTORY_PATH = "Garbage";
    private const string HELPER_SPAWNER_SETTINGS_DIRECTORY_PATH = "Helpers";

    public void RestartGame()
    {
        BubbleBarController.Instance.InitBubbleBar();
        SoundManager.StopSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PauseGame()
    {
        _stateMachine.PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        SoundManager.PauseSound();
    }

    // continue playing the game 
    public void ResumeGame()
    {
        _stateMachine.PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        SoundManager.ResumeSound();
    }
    
    public void OnStateEnter(BaseStateMachine stateMachine)
    {
        try
        {
            _stateMachine = (GlobalUIStateMachine)stateMachine;
            _stateMachine.ResumeButtonUI.onClick.AddListener(ResumeGame);
            if (_stateMachine.CurrentLevel > 1 && !IsSceneLoaded(_stateMachine.CurrentLevel))
            {
                UnloadLevel(_stateMachine.CurrentLevel - 1);
            } else if (IsSceneLoaded(_stateMachine.CurrentLevel))
            {
                UnloadLevel(_stateMachine.CurrentLevel);
            }
            LoadLevel(_stateMachine.CurrentLevel);
            _stateMachine.RestartMenuUI.SetActive(false);
            _stateMachine.PauseMenuUI.SetActive(false);
            BubbleBarController.Instance.InitBubbleBar();
            HealthBarController.Instance.InitHealthBar();
            GarbageSpawner.Instance.ConfigureSpawnerSetting(_stateMachine.CurrentLevel);
            HelperSpawner.Instance.ConfigureSpawnerSetting(_stateMachine.CurrentLevel);
            PlayerController.Instance.EnableInputActions();
            Time.timeScale = 1;
            SoundManager.PlaySound("background1");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public void OnStateExit(BaseStateMachine stateMachine)
    {

    }

    public void UpdateState(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (HealthBarController.Instance.isRunout() || BubbleBarController.Instance.isRunout() ||
            GarbageSpawner.Instance.GetGarbageRemained() == 0)
        {
            _stateMachine.SetState(_stateMachine.TransitionState);
        }
    }

    private static bool IsSceneLoaded(int level)
    {
        return SceneManager.GetSceneByName(GetSceneName(level)).isLoaded;
    }

    private static void LoadLevel(int level)
    {
        SceneManager.LoadSceneAsync(GetSceneName(level), LoadSceneMode.Additive);
    }
    
    private static void UnloadLevel(int level)
    {
        SceneManager.UnloadSceneAsync(GetSceneName(level));
    }

    private static string GetSceneName(int level)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(level + 2);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        //Debug.Log("Current loading scene is: " + sceneName);
        return sceneName;
    }
}
