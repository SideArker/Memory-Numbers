using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class RNGHandler : MonoBehaviour
{
    // Class variables
    [SerializeField] GameController gc;

    // Lists
    [SerializeField] List<GameObject> spheres = new List<GameObject>();
    [SerializeField] List<Material> materials = new List<Material>();


    // Randomizes sphere numbers so that duplicates don't appear 
    public void randomize()
    {

        // Shuffle the material list here
        List<Material> shuffledMats = new List<Material>();
        for (int i = 0; i < gc.numCount; i++)
        {
            int selectedNum = Random.Range(0, materials.Count);
            shuffledMats.Add(materials[selectedNum]);
            materials.Remove(materials[selectedNum]);

            // Add materials to spheres

            spheres[i].GetComponent<MeshRenderer>().material = shuffledMats[i];
            spheres[i].GetComponent<Rigidbody>().mass = Random.Range(1, 5); 

        }

        materials = shuffledMats;
    }

    // Selects spheres for the ticket
    public void selectSpheres()
    {
        gc.selectedNumbers.Clear();

        while (gc.selectedNumbers.Count < gc.selectionAmount)
        {
            int selectedIndex = Random.Range(0, gc.numCount);
            if (!gc.selectedNumbers.Contains(spheres[selectedIndex])) gc.selectedNumbers.Add(spheres[selectedIndex]);
        }

    }
}
