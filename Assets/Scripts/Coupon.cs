using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Coupon : MonoBehaviour
{

    List<int> selectedNums = new List<int>();
    [Header("Animators")]
    [SerializeField] Animator[] Strokes;
    [SerializeField] Animator animator;
    [SerializeField] Animator levelAnim;

    [Header("Classes & Objects")]
    [SerializeField] GameController gc;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] TMP_Text score;


    void CheckSelected()
    {
        int rightNumbers = 0;
        animator.Play("CouponHide");
        levelAnim.Play("LevelOut");
        foreach (int number in selectedNums)
        {
            if (gc.selectedNumbers.Contains(number))
            {
                rightNumbers++;
            }
        }
        if (rightNumbers == gc.numbersToSelect)
        {
            Debug.Log("Right numbers");
            if (PlayerPrefs.GetInt("currentLevel") == 5)
            {
                winScreen.SetActive(true);
                PlayerPrefs.SetInt("currentLevel", 0);
            }
            else
            {
                StartCoroutine(CouponHide());
            }
        }
        else
        {
            Debug.Log("Wrong numbers");
            gameOverScreen.SetActive(true);
            score.text = $"Dotar�e� do rundy {PlayerPrefs.GetInt("currentLevel")}\r\nTw�j wynik w tej rundzie to:\r\n{rightNumbers}/{gc.numbersToSelect}\r\npoprawnych odpowiedzi";
            PlayerPrefs.SetInt("currentLevel", 0);
        }
    }
    IEnumerator CouponHide()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SampleScene");
        this.gameObject.SetActive(false);
    }

    public void CouponShow()
    {
        Strokes[gc.numbersToSelect - 1].Play("Stroke");
    }

    public void Select()
    {
        // Get the button that was pressed
        GameObject button = EventSystem.current.currentSelectedGameObject;

        Button btn = button.GetComponent<Button>();
        if (btn == null) return;


        // Trim the text so it gets rid of useless stuff 
        TMP_Text parentText = btn.GetComponentInParent<TMP_Text>();
        char[] chars = { '[', ']' };
        int number = Convert.ToInt32(parentText.text.Trim(chars));

        // Add selected number to list
        if (selectedNums.Contains(number))
        {
            selectedNums.Remove(number);
            button.GetComponentInParent<Animator>().Play("StrokeBack");
        }
        else
        {
            selectedNums.Add(number);
            button.GetComponentInParent<Animator>().Play("Stroke");
        }

        if (selectedNums.Count >= gc.numbersToSelect)
        {
            CheckSelected();
        }
    }
}
