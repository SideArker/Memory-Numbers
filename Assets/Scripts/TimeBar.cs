using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{

    Image image;
    public float currentTime;
    public float maxTime = 15f;
    float lerpValue;
    bool gameIsActive = true;

    private void Start()
    {
        currentTime = maxTime;
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(gameIsActive) TimerStart();
          
    }

    void TimerStart()
    {
        if (currentTime > 0)
        {
            lerpValue = Mathf.Lerp(0, 1, currentTime / maxTime);
            currentTime -= Time.deltaTime;

            image.fillAmount = lerpValue;
        }
        else if (currentTime <= 0)
        {
            image.fillAmount = 0;

            //miejsce na dalsze funkcje

        }

    }

    
}
