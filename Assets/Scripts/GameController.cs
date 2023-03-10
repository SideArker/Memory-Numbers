using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using Unity.Mathematics;
using System;
using UnityEngine.UIElements;
using TMPro;

public class GameController : MonoBehaviour
{
    // Level variables etc.
    int highScore = 0;
    // Other
    RNGHandler rng;
    public List<GameObject> selectedGameObjects = new List<GameObject>();
    public List<int> selectedNumbers = new List<int>();

    public int numCount = 70;
    public int selectionAmount = 20;
    public int numbersToSelect; // 3 is default
    int maxNumbersToSelect = 12;
    int[] levelsOfNumbers = { 3, 5, 8, 10, 12 };
    [SerializeField] TimeBar time;
    [SerializeField] float waitTime;
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject coupon;
    Animator animator;
    bool selected = false;
    [SerializeField] TMP_Text levelText;

    void Start()
    {
        waitTime = time.maxTime;
        rng = FindObjectOfType<RNGHandler>();
        advanceLevel();
        animator = GetComponent<Animator>();
        time.gameObject.transform.parent.GetComponent<Animator>().Play("TimerIn");
    }
    public void gameOver()
    {
        highScore = PlayerPrefs.GetInt("currentLevel");

    }
    IEnumerator waitPhase()
    {
        Debug.Log("Wait Phase...");
        yield return new WaitForSeconds(5);
        // do animation here
        selected = true;
        time.TimerStart();
        yield return new WaitForSeconds(waitTime);
        animator.Play("CameraDown");
        yield return new WaitForSeconds(1);
        coupon.SetActive(true);
        yield return new WaitForSeconds(1);
        coupon.GetComponent<Coupon>().couponShow();
    }
    private void Update()
    {
        if (selected)
        {
            foreach (var item in selectedGameObjects)
            {
                item.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.Lerp(item.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor"), selectedColor, Time.deltaTime * 2));
            }
            foreach (var item in selectedGameObjects)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, 0);

                item.transform.rotation = Quaternion.Lerp(item.transform.rotation, targetRotation, 2 * Time.deltaTime);
            }
        }
    }
    [Button]
    void ResetLevel()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
    }
    public void advanceLevel()
    {
        PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel")+1);
        levelText.text = $"Runda {PlayerPrefs.GetInt("currentLevel")}";
        if (numbersToSelect < maxNumbersToSelect) numbersToSelect = levelsOfNumbers[PlayerPrefs.GetInt("currentLevel") -1];
        // Randomize numbers
        rng.randomize();
        // Select numbers and lit them up
        rng.selectSpheres();
        // Show the numbers for 15 seconds then sort the spheres
        StartCoroutine(waitPhase());
    }
}