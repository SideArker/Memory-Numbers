using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RNGHandler : MonoBehaviour
{
    // Function variables

    GameController gameController;

    // Lists
    public List<int> numbers = new List<int>();
    [SerializeField] List<GameObject> spheres = new List<GameObject>();


    // Randomizes numbers so that duplicates don't appear
    public void randomize()
    {
        numbers.Clear();
        List<int> allNums = new List<int>();
        for (int i = 0; i < gameController.numCount; i++) allNums.Add(i);

        for (int i = 0; i < gameController.numCount; i++)
        {
            int selectedNum = Random.Range(0, allNums.Count);
            numbers.Add(allNums[selectedNum]);
            // Adds numbers to spheres
            spheres[i].GetComponentInChildren<TMP_Text>().text = allNums[selectedNum].ToString();
            allNums.Remove(allNums[selectedNum]);
        }

    }

    public void selectNumbers()
    {
        gameController.selectedNumbers.Clear();
        while(gameController.selectedNumbers.Count < gameController.selectionAmount)
        {
            int selectedIndex = Random.Range(0, numbers.Count);
            if (!gameController.selectedNumbers.Contains(numbers[selectedIndex])) gameController.selectedNumbers.Add(numbers[selectedIndex]);

        }
    }

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        randomize();
        selectNumbers();
    }
}