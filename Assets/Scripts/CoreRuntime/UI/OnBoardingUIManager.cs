using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnBoardingUIManager : MonoBehaviour
{

    public Image[] backgroundImages;
    private int currentPageIndex = 0;
    // Start is called before the first frame update
    private void Start()
    {
        if (backgroundImages != null)
        {
            //SoundManager.PlaySound("background2");
            backgroundImages[0].gameObject.SetActive(true);
            if (backgroundImages.Length >  1)
            {
                for (int i = 1; i < backgroundImages.Length; i++)
                {
                    backgroundImages[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void GoToNextAvailablePage()
    {
        currentPageIndex++;
        if (currentPageIndex <= backgroundImages.Length - 1)
        {
            backgroundImages[currentPageIndex - 1].gameObject.SetActive(false);
            backgroundImages[currentPageIndex].gameObject.SetActive(true);
        } else
        {
            StartCoroutine(loadGameScene());
            //SoundManager.StopSound();
            //SceneManager.LoadScene("SampleScene");

        }
    }

    IEnumerator loadGameScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("2_Lake");
    }

}
