using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject bubbleBarUI;
    public GameObject powerfulBubbleBarUI;
    public Slider zipTimeSlider;

    private bool isBubbleBarActive = true;
    private void Start()
    {
        BubbleBarController.Instance.InitBubbleBar();
        PowerfulBubbleBarController.Instance.InitBubbleBar();
        toggleBubbleBar(isBubbleBarActive);
        togglePowerfulBubbleBar(!isBubbleBarActive);

    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene2")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isBubbleBarActive = !isBubbleBarActive;
                toggleBubbleBar(isBubbleBarActive);
                togglePowerfulBubbleBar(!isBubbleBarActive);
            }
        }
    }


    private void toggleBubbleBar(bool isBubbleBarVisible)
    {
        //Assuming parent is the parent game object
        if (bubbleBarUI != null)
        {
            for (int i = 0; i < bubbleBarUI.transform.childCount; i++)
            {
                var child = bubbleBarUI.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(isBubbleBarVisible);
            }
        }
    }

    private void togglePowerfulBubbleBar(bool isPowerfulBubbleBarVisible)
    {
        if (powerfulBubbleBarUI != null)
        {
            for (int i = 0; i < powerfulBubbleBarUI.transform.childCount; i++)
            {
                var child = powerfulBubbleBarUI.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(isPowerfulBubbleBarVisible);
            }
        }
    }

}
