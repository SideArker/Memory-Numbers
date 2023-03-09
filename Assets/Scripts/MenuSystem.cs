using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{

    [SerializeField] GameObject pauseScreen;
    [SerializeField] Animator animator;
    TimeBar timebar;



    public void PauseButtonClick()
    {
        animator.ResetTrigger("close");
        animator.SetTrigger("open");
        Time.timeScale = 0f;
    }

    public void ResumeButtonClick()
    {
        animator.ResetTrigger("open");
        animator.SetTrigger("close");
        Time.timeScale = 1f;
    }


}
