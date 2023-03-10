using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

public class Coupon : MonoBehaviour
{

    List<int> selectedNums = new List<int>();
    [SerializeField] Animator[] Strokes;
    [SerializeField] GameController gc;


    void checkSelected()
    {

        foreach(int number in selectedNums)
        {
            if (gc.selectedNumbers.Contains(number)) Debug.Log("Wrong selection");
            else
            {

            }
        }

    }

    public void couponShow()
    {
        Strokes[gc.selectionAmount].Play("Stroke");
    }

    public void select()
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
            checkSelected();
        }


    }
}
