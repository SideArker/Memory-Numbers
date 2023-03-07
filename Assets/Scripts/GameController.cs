using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    // Level variables etc.
    int currentLevel = 0;
    int highScore = 0;
    float scoreMultiplier = 1f;
    // Other
    RNGHandler rng;
    public List<int> selectedNumbers = new List<int>();
    public int numCount = 70;
    public int selectionAmount = 20; // 3 is default
    float waitTime = 15f;
    float countScore()
    {
        return Mathf.Round((currentLevel * 50) * scoreMultiplier);
    }
    void advanceLevel()
    {
        scoreMultiplier += 0.05f;
        currentLevel++;
        // Randomize numbers
        rng.randomize();
        // Select numbers and lit them up
        rng.selectNumbers();
        // Show the numbers for 15 seconds then sort the spheres
        StartCoroutine(waitPhase());
    }
    IEnumerator waitPhase()
    {
        yield return new WaitForSeconds(waitTime);
        // do animation here

    }
    void Start()
    {
        rng = FindObjectOfType<RNGHandler>();
        advanceLevel();
    }
}