using System;
using System.Collections;
using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : IBaseState
{
    // class variable not instance variable
    public static bool GameIsPaused = false;
    private GlobalUIStateMachine _stateMachine;

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
        _stateMachine.CurrentLevel += 1;
        Time.timeScale = 1;
        _stateMachine.RestartMenuUI.SetActive(false);
        _stateMachine.PauseMenuUI.SetActive(false);
        BubbleBarController.Instance.InitBubbleBar();
        HealthBarController.Instance.InitHealthBar();
        SoundManager.PlaySound("background1");
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
}
