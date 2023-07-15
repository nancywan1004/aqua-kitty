using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionState : IBaseState
{
    private GlobalUIStateMachine _stateMachine;
    
    public void OnStateEnter(BaseStateMachine stateMachine)
    {
        _stateMachine = (GlobalUIStateMachine)stateMachine;
        _stateMachine.RestartMenuUI.SetActive(true);
        Time.timeScale = 0;
        SoundManager.StopSound();
        if (HealthBarController.Instance.isRunout())
        {
            ShowRestartMenu();
        }
        else if (BubbleBarController.Instance.isRunout() && GarbageSpawner.Instance.GetGarbageRemained() > 0)
        {
            ShowRunOutOfBubbleMenu();
        }
        else if (GarbageSpawner.Instance.GetGarbageRemained() == 0)
        {
            ShowNextMenu();
        }
    }

    public void OnStateExit(BaseStateMachine stateMachine)
    {
        
    }

    public void UpdateState(BaseStateMachine stateMachine)
    {
       
    }
    
    private void ShowRestartMenu()
    {
        _stateMachine.RestartText.text = "GAME OVER!";
    }

    private void ShowRunOutOfBubbleMenu()
    {
        _stateMachine.RestartText.text = "BUBBLES ARE OUT!";
    }

    private void ShowNextMenu()
    {
        _stateMachine.RestartText.text = "WELL DONE";
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            _stateMachine.RestartButtonUI.onClick.AddListener(() =>
            {
                _stateMachine.CurrentLevel -= 1;
                _stateMachine.SetState(_stateMachine.GameState);
            });
            _stateMachine.NextLevelButtonUI.gameObject.SetActive(true);
            _stateMachine.NextLevelButtonUI.onClick.AddListener(() =>
            {
                _stateMachine.SetState(_stateMachine.GameState);
            });
        }
    }
}