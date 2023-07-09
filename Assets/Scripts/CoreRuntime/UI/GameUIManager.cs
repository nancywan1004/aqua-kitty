using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : Singleton<GameUIManager>
{
    // class variable not instance variable
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject restartPrefab;
    public GameObject restartMenuUI;
    public Text restartText;
    public GameObject nextLevelButtonUI;

    public int levelName = 2;

    private bool isLevelEnd = false;

    private void Awake()
    {
        //Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
    }

    // called first
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Time.timeScale = 1;
        restartMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        HealthBarController.Instance.InitHealthBar();
        //Timer.Instance.InitTimer();
        SoundManager.PlaySound("background1");
    }

    void OnDisable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void Update()
    {
        if (!isLevelEnd)
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

            // Timer.Instance.isTimeout()
            if (HealthBarController.Instance.isRunout())
            {
                ShowRestartMenu();
            }
            else if (BubbleBarController.Instance.isRunout() && GarbageSpawner.Instance.GetGarbageRemained() > 0)
            {
                StartCoroutine(lastAttempt());
            }
            else if (GarbageSpawner.Instance.GetGarbageRemained() == 0)
            {
                ShowNextMenu();
            }
            else
            {
                //HealthBarController.Instance.UpdateHealthBar(oxygenSpeed);
                //Timer.Instance.UpdateTimer();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //SoundManager.PlaySound("background1");
        Debug.Log("On Screen Load");
    }

    IEnumerator lastAttempt()
    {
        yield return new WaitForSeconds(3);
        if (GarbageSpawner.Instance.GetGarbageRemained() > 0)
        {
            ShowRestartMenu();
        }
    }

    private void ShowRestartMenu()
    {
        restartText.text = "GAME OVER!";
        restartMenuUI.SetActive(true);
        Time.timeScale = 0;
        isLevelEnd = true;
        SoundManager.StopSound();
        //SoundManager.PlaySound("background2");
    }

    private void ShowNextMenu()
    {
        restartText.text = "WELL DONE";
        restartMenuUI.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            nextLevelButtonUI.SetActive(true);
        }
        Time.timeScale = 0;
        isLevelEnd = true;
        SoundManager.StopSound();
    }

    public void RestartGame()
    {
        SoundManager.StopSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        SoundManager.PauseSound();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        SoundManager.ResumeSound();
    }

    //Loads a new Unity scene, or reload the current one (it means all objects are reset)
    public void loadNewScene()
    {
        //load another scene
        SceneManager.LoadScene("3_Pond");
    }

}
