using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerfulBubbleBarController : Singleton<PowerfulBubbleBarController>
{
    public float bubbleRemaining;
    public TMP_Text bubbleText;

    public Image[] bubbles;
    public Sprite bubbleIcon;

    private float maxBubble = 0f;

    public void InitBubbleBar()
    {
        bubbleRemaining = maxBubble;
        SetBubble(bubbleRemaining);

    }

    public void ConsumeBubble(float speed)
    {

        if (bubbleRemaining > 0)
        {
            bubbleRemaining -= speed;
            SetBubble(bubbleRemaining);
        }
        else
        {
            Debug.Log("bubble has run out!");
            bubbleRemaining = 0;
        }
    }

    public void GainBubble(float speed)
    {
        bubbleRemaining += speed;
        SetBubble(bubbleRemaining);
    }

    public float GetCurrBubble()
    {
        return bubbleRemaining;
    }

    public void SetBubble(float bubble)
    {
        if (bubbles != null)
        {
            bubbleText.text = bubble.ToString();
            for (int i = 0; i < bubbles.Length; i++)
            {
                if (i < bubble)
                {
                    bubbles[i].sprite = bubbleIcon;
                    bubbles[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    Debug.Log("Power bubble shown: " + bubble);
                }
                else
                {
                    bubbles[i].color = new Color(1.0f, 1.0f, 1.0f, 0);
                }
            }
        }
    }

    public bool isRunout()
    {
        return bubbleRemaining == 0;
    }

}
