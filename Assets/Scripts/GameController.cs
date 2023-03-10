using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using Unity.Mathematics;
using System;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    // Level variables etc.
    int currentLevel = 0;
    int highScore = 0;
    // Other
    RNGHandler rng;
    public List<GameObject> selectedGameObjects = new List<GameObject>();
    public List<int> selectedNumbers = new List<int>();

    public int numCount = 70;
    public int selectionAmount = 20; 
    public int numbersToSelect = 3; // 3 is default
    int maxNumbersToSelect = 10;
    [SerializeField] TimeBar time;
    [SerializeField] float waitTime;
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject coupon;
    Animator animator;
    bool selected = false;
    void Start()
    {
        waitTime = time.maxTime;
        rng = FindObjectOfType<RNGHandler>();
        advanceLevel();
        animator = GetComponent<Animator>();
    }
    public void gameOver()
    {
        highScore = currentLevel;

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
    void TestColor()
    {
        selectedGameObjects[0].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", selectedColor);
    }
    public void advanceLevel()
    {
        currentLevel++;
        if (numbersToSelect < maxNumbersToSelect && currentLevel % 2 == 0) numbersToSelect += 1;
        // Randomize numbers
        rng.randomize();
        // Select numbers and lit them up
        rng.selectSpheres();
        // Show the numbers for 15 seconds then sort the spheres
        StartCoroutine(waitPhase());
    }
}