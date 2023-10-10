using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BubbleBarController : Singleton<BubbleBarController>
{
    public int bubbleRemaining;
    public TMP_Text bubbleText;

    public GameObject[] bubbles;
    public Sprite bubbleIcon;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public int MaxBubble { get; set; } = 0;

    public void InitBubbleBar()
    {
        bubbleRemaining = MaxBubble;
    }

    public void ConsumeBubble(int speed)
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

    private void SetBubble(int bubble)
    {
        bubbleText.text = bubble.ToString();
        var bubbleLeft = bubble;
        while (bubbleLeft < bubbles.Length)
        {
            bubbles[bubbleLeft].gameObject.SetActive(false);
            bubbleLeft++;
        }
        //fill.color = gradient.Evaluate(slider.normalizedValue);

        }

    public bool isRunout()
    {
        return bubbleRemaining == 0;
    }

}
