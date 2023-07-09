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
        startButton.onClick.AddListener(loadOnBoardingScene);
        SoundManager.PlaySound("background2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadOnBoardingScene()
    {
        //SoundManager.StopSound();
        SceneManager.LoadScene("1_OnBoarding", LoadSceneMode.Additive);
    }
}
