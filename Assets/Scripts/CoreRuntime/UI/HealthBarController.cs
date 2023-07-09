using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : Singleton<HealthBarController>
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TMP_Text hpValue;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite oneThirdHeart;
    public Sprite twoThirdsHeart;
    public Sprite emptyHeart;

    private float currentHealth;
    private bool hpIsRunning = false;
    private float maxHealth = 15.0f;

    public void InitHealthBar()
    {

        currentHealth = maxHealth;
        hpIsRunning = true;
        //SetMaxHealth(maxHealth);
    }

    public void UpdateHealthBar(float speed)
    {
        if (hpIsRunning)
        {
            if (currentHealth > 0)
            {
                currentHealth -= speed * Time.deltaTime;
                SetHealth(currentHealth);
            }
            else
            {
                Debug.Log("Oxygen has run out!");
                currentHealth = 0;
                hpIsRunning = false;
            }
        }
    }

    public void GetAttacked(float speed)
    {
        if (hpIsRunning)
        {
            if (currentHealth > 0)
            {
                currentHealth -= speed;
                SetHealth(currentHealth);
            }
            else
            {
                Debug.Log("Oxygen has run out!");
                currentHealth = 0;
                hpIsRunning = false;
            }
        }
    }

    public void StopUpdatingHealthBar()
    {
        hpIsRunning = false;
    }

    public float GetCurrHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        //fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        //slider.value = health;
        hpValue.text = health.ToString();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.Floor(health / 3))
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i == Mathf.Floor(health / 3))
            {
                float residual = health % 3;
                switch (residual)
                {
                    case 0:
                        hearts[i].sprite = emptyHeart;
                        break;
                    case 1:
                        hearts[i].sprite = oneThirdHeart;
                        break;
                    case 2:
                        hearts[i].sprite = twoThirdsHeart;
                        break;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            //fill.color = gradient.Evaluate(slider.normalizedValue);

        }
    }

    public void ClearText()
    {
        hpValue.text = string.Empty;
    }

    public bool isRunout()
    {
        return currentHealth == 0;
    }

}
