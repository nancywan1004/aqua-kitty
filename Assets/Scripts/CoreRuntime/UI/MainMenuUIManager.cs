using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(LoadOnBoardingScene);
        SoundManager.PlaySound("background2");
    }

    private void LoadOnBoardingScene()
    {
        SceneManager.LoadScene("1_OnBoarding", LoadSceneMode.Additive);
    }
}
