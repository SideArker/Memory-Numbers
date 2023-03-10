using NaughtyAttributes;
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
    bool gameIsActive = false;
    [SerializeField] Animator animator;

    private void Start()
    {
        currentTime = maxTime;
        image = GetComponent<Image>();

    }

    void Update()
    {
        if(gameIsActive) Timer();
          
    }

    [Button] public void TimerStart()
    {
        gameIsActive = true;
    }

    void Timer()
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

            animator.Play("TimerOut");

        }

    }

    
}
