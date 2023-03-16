using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] Animator animator;
    public void BackToMainMenu()
    {
        StartCoroutine(HideAnimMenu());

    }
    public void CloseUI()
    {
        StartCoroutine(HideAnim());
    }

    IEnumerator HideAnim()
    {
        animator.Play("Hide");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
    IEnumerator HideAnimMenu()
    {
        animator.Play("Hide");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
