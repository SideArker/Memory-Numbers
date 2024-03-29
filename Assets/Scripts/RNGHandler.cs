using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class RNGHandler : MonoBehaviour
{

    [Header("Lists")]
    [SerializeField] List<GameObject> spheres = new List<GameObject>();
    [SerializeField] List<Material> materials = new List<Material>();


    // Randomizes sphere numbers so that duplicates don't appear 
    public void Randomize()
    {
        // Shuffle the material list here
        List<Material> shuffledMats = new List<Material>();
        for (int i = 0; i < GameController.numCount; i++)
        {
            int selectedNum = UnityEngine.Random.Range(0, materials.Count);
            shuffledMats.Add(materials[selectedNum]);
            materials.Remove(materials[selectedNum]);

            // Add materials to spheres
            spheres[i].GetComponent<MeshRenderer>().material = shuffledMats[i];
            spheres[i].GetComponent<Rigidbody>().mass = UnityEngine.Random.Range(1, 5);
        }
        materials = shuffledMats;
    }

    // Selects spheres for the ticket
    public void SelectSpheres()
    {
        GameController gc = GetComponent<GameController>();
        gc.selectedNumbers.Clear();

        while (gc.selectedNumbers.Count < GameController.selectionAmount)
        {
            int selectedIndex = UnityEngine.Random.Range(0, GameController.numCount);
            if (!gc.selectedGameObjects.Contains(spheres[selectedIndex]))
            {
                gc.selectedGameObjects.Add(spheres[selectedIndex]);

                // Get the number of the sphere
                string matName = spheres[selectedIndex].GetComponent<MeshRenderer>().material.name;
                int sphereNum = Convert.ToInt32(Regex.Replace(matName, "[^.0-9]", ""));

                gc.selectedNumbers.Add(sphereNum);
            }
        }
    }
}
