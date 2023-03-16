using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void NextScene()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
        StartCoroutine(Animations());
    }

    IEnumerator Animations()
    {
        animator.Play("MainMenuOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }

}
