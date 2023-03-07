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

    public int selectionAmount = 3; // 3 is default
    float waitTime = 15f;


    float countScore()
    {
        return Mathf.Round((currentLevel * 50) * scoreMultiplier);
    }


    void advanceLevel()
    {
        scoreMultiplier += 0.05f;
        currentLevel++;
        rng.randomize();
        rng.selectNumbers();
    }

    void Start()
    {
        rng = FindObjectOfType<RNGHandler>();
    }
}