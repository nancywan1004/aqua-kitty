using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BubbleBarController : Singleton<BubbleBarController>
{
    public float bubbleRemaining;
    public TMP_Text bubbleText;

    public Image[] bubbles;
    public Sprite bubbleIcon;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float maxBubble = 15.0f;

    public void InitBubbleBar()
    {
        bubbleRemaining = maxBubble;
        //SetMaxBubble(maxBubble);

    }

    //public void UpdateBubbleBar(float speed)
    //{
    //    if (bubbleIsRunning)
    //    {
    //        if (bubbleRemaining > 0)
    //        {
    //            bubbleRemaining -= speed * Time.deltaTime;
    //            SetBubble(bubbleRemaining);
    //        }
    //        else
    //        {
    //            Debug.Log("bubble has run out!");
    //            bubbleRemaining = 0;
    //            bubbleIsRunning = false;
    //        }
    //    }
    //}

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

    public float GetCurrBubble()
    {
        return bubbleRemaining;
    }

    public void SetMaxBubble(float bubble)
    {
        slider.maxValue = bubble;
        slider.value = bubble;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetBubble(float bubble)
    {
        bubbleText.text = bubble.ToString();
        for (int i = 0; i < bubbles.Length; i++)
        {
            if (i < bubble)
            {
                bubbles[i].sprite = bubbleIcon;
            }
            else
            {
                bubbles[i].color = new Color(1.0f, 1.0f, 1.0f, 0);
            }
         }
            //fill.color = gradient.Evaluate(slider.normalizedValue);

        }

    public bool isRunout()
    {
        return bubbleRemaining == 0;
    }

}
