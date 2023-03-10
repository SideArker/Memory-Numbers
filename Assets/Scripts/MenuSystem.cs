using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("SampleScene");
    }


}
