using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
    }

    public void NextScene()
    {
        StartCoroutine(Animations());
    }

    IEnumerator Animations()
    {
        animator.Play("MainMenuOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }

}
