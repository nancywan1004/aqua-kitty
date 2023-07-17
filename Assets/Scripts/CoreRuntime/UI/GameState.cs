using System.IO;
using CoreRuntime.Garbage;
using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : IBaseState
{
    // class variable not instance variable
    public static bool GameIsPaused = false;
    private GlobalUIStateMachine _stateMachine;
    private const string GARBAGE_SPAWNER_SETTINGS_DIRECTORY_PATH = "Assets/Content/Garbabge/Resources";

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
        _stateMachine = (GlobalUIStateMachine)stateMachine;
        if (_stateMachine.CurrentLevel > 0)
        {
            UnloadLevel(_stateMachine.CurrentLevel);
        }
        _stateMachine.CurrentLevel += 1;
        LoadLevel(_stateMachine.CurrentLevel);
        _stateMachine.RestartMenuUI.SetActive(false);
        _stateMachine.PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        BubbleBarController.Instance.InitBubbleBar();
        HealthBarController.Instance.InitHealthBar();
        GarbageSpawner.Instance.ConfigureSpawnerSetting(Resources.Load<GarbageSpawnerSSO>(Path.Combine(GARBAGE_SPAWNER_SETTINGS_DIRECTORY_PATH, "GarbageSpawnerSettings_Lv", _stateMachine.CurrentLevel.ToString())));
        SoundManager.PlaySound("background1");
    }

    public void OnStateExit(BaseStateMachine stateMachine)
    {
        //SceneManager.UnloadSceneAsync(_currentSceneName);
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

    private void LoadLevel(int level)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(level + 2);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        Debug.Log("Current loading scene is: " + sceneName);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
    
    private void UnloadLevel(int level)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(level + 2);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        Debug.Log("Current loading scene is: " + sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    } 
}
