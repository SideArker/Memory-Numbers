using System.Collections.Generic;
using UnityEngine;

public class RNGHandler : MonoBehaviour
{
    // Function variables
    int numCount = 70;
    GameController gameController;

    // Lists
    public List<int> numbers = new List<int>();
    [SerializeField] List<GameObject> hexagons = new List<GameObject>();


    // Randomizes numbers so that duplicates don't appear
    public void randomize()
    {
        numbers.Clear();
        List<int> allNums = new List<int>();
        for (int i = 0; i < numCount; i++) allNums.Add(i);

        for (int i = 0; i < numCount; i++)
        {
            int selectedNum = Random.Range(0, allNums.Count);
            numbers.Add(allNums[selectedNum]);
            allNums.Remove(allNums[selectedNum]);
        }


    }

    public void selectNumbers()
    {
        gameController.selectedNumbers.Clear();
        for(int i = 0; i < gameController.selectionAmount; i++)
        {
            int selectedNum = Random.Range(0, numbers.Count);
            if (!gameController.selectedNumbers.Contains(selectedNum)) gameController.selectedNumbers.Add(selectedNum);

        }
    }

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        randomize();
        selectNumbers();
    }
}