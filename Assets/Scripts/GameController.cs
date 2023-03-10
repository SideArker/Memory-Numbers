using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

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
    public int selectionAmount = 20; // 3 is default
    public int numbersToSelect = 3;

    int maxNumbersToSelect = 12;
    [SerializeField] float waitTime = 15f;
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject coupon;

    Animator animator;
    
    void Start()
    {
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

        foreach (var item in selectedGameObjects)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", selectedColor);
        }

        yield return new WaitForSeconds(waitTime);

        animator.Play("CameraDown");
        yield return new WaitForSeconds(1);

        coupon.SetActive(true);

    }

    //private void Update()
    //{
    //    foreach (var item in selectedNumbers)
    //    {
    //        item.transform.rotation = Quaternion.EulerAngles(0,item.transform.rotation.y, item.transform.rotation.z);
    //    }
    //}

    [Button] void TestColor()
    {
        selectedGameObjects[0].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", selectedColor);
    }

    public void advanceLevel()
    {
        currentLevel++;
        if(numbersToSelect < maxNumbersToSelect && currentLevel % 2 == 0) numbersToSelect += 1;

        // Randomize numbers
        rng.randomize();
        // Select numbers and lit them up
        rng.selectSpheres();
        // Show the numbers for 15 seconds then sort the spheres
        StartCoroutine(waitPhase());
    }

}