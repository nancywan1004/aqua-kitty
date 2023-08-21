using CoreSystem.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class TransitionState : IBaseState
{
    private GlobalUIStateMachine _stateMachine;
    public Action OnGameRestart;
    public Action OnGameProceed;
    
    public void OnStateEnter(BaseStateMachine stateMachine)
    {
        _stateMachine = (GlobalUIStateMachine)stateMachine;
        _stateMachine.RestartMenuUI.SetActive(true);
        Time.timeScale = 0;
        SoundManager.StopSound();
        PlayerController.Instance.DisableInputActions();
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
        _stateMachine.RestartButtonUI.onClick.AddListener(() =>
        {
            _stateMachine.SetState(_stateMachine.GameState);
        });
        if (_stateMachine.HasNextLevel)
        {
            _stateMachine.NextLevelButtonUI.gameObject.SetActive(true);
            _stateMachine.NextLevelButtonUI.GetComponentInChildren<Text>().text = "LEVEL " + (_stateMachine.CurrentLevel+1);
            _stateMachine.NextLevelButtonUI.onClick.AddListener(() =>
            {
                _stateMachine.CurrentLevel += 1;
                _stateMachine.SetState(_stateMachine.GameState);
            });
        }
        else
        {
            _stateMachine.NextLevelButtonUI.gameObject.SetActive(false);
        }
    }
}