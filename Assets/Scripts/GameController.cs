using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    // Level variables etc.
    int currentLevel = 0;
    int highScore = 0;
    // Other
    RNGHandler rng;
    public List<GameObject> selectedNumbers = new List<GameObject>();
    public int numCount = 70;
    public int selectionAmount = 20; // 3 is default
    public int numbersToSelect = 3;

    int maxNumbersToSelect = 10;
    float waitTime = 15f;
    [SerializeField] Color selectedColor;

    public void gameOver()
    {
        highScore = currentLevel;
        
    }

    IEnumerator waitPhase()
    {
        foreach (var item in selectedNumbers)
        {
            item.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        }

        yield return new WaitForSeconds(waitTime);
        // do animation here


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

    void Start()
    {
        rng = FindObjectOfType<RNGHandler>();
        advanceLevel();
    }
}