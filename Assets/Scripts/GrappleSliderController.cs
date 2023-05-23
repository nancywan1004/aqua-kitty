using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleSliderController : Singleton<GrappleSliderController>
{

    private Slider zipTimeSlider;

    private float holdTime = 0.5f;
    private float holdStartTime;

    private bool stopSlider;

    private void Awake()
    {
        zipTimeSlider = GetComponent<Slider>();
    }
    public void InitSlider(float startTime)
    {
        zipTimeSlider.gameObject.SetActive(true);
        holdStartTime = startTime;
        stopSlider = false;
        zipTimeSlider.maxValue = holdTime;
        zipTimeSlider.value = 0;

        StartCoroutine(GrappleHold());
        
    }

    IEnumerator GrappleHold()
    {
        while (stopSlider == false)
        {
            float time = Time.time - holdStartTime;
            if (time > holdTime)
            {
                stopSlider = true;
            }
            zipTimeSlider.value = time;
            Debug.Log("Hold down time is: " + time);

            yield return null;
        }
    }

    public void UpdateSlider()
    {
        float time = holdTime - holdStartTime;
        if (time < 0)
        {
            stopSlider = true;
        }

        if (stopSlider == false)
        {
            zipTimeSlider.value = time;
        }
    }

    public void HideSlider()
    {
/*        if (zipTimeSlider != null)
        {*/
            zipTimeSlider.gameObject.SetActive(false);
        //}

    }


}
