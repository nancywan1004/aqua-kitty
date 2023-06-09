using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject bubbleBarUI;
    public Slider zipTimeSlider;

    private bool isBubbleBarActive = true;
    private void Start()
    {
        BubbleBarController.Instance.InitBubbleBar();
        toggleBubbleBar(isBubbleBarActive);

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
}
