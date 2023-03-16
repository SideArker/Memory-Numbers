using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Other
    RNGHandler rng;
    [Header("Lists")]
    public List<GameObject> selectedGameObjects = new List<GameObject>();
    public List<int> selectedNumbers = new List<int>();
    readonly int[] levelsOfNumbers = { 3, 5, 8, 10, 12 };

    [Header("Game Settings")]
    public const int numCount = 70;
    public const int selectionAmount = 20;
    public int numbersToSelect;
    [SerializeField] float timerWaitTime = 1f;
    [SerializeField] Color selectedColor;

    const float waitTime = 1f;
    const int maxNumbersToSelect = 12;

    [Header("Objects")]
    [SerializeField] TimeBar time;
    [SerializeField] GameObject coupon;
    [SerializeField] TMP_Text levelText;

    bool selected = false;
    Animator animator;

    void Start()
    {
        timerWaitTime = TimeBar.maxTime;
        rng = FindObjectOfType<RNGHandler>();
        AdvanceLevel();
        animator = GetComponent<Animator>();
        time.gameObject.transform.parent.GetComponent<Animator>().Play("TimerIn");
    }

    IEnumerator WaitPhase()
    {
        Debug.Log("Wait Phase...");
        yield return new WaitForSeconds(5);
        // do animation here
        selected = true;
        time.timerStart = true;
        yield return new WaitForSeconds(timerWaitTime);
        animator.Play("CameraDown");
        selected = false;
        yield return new WaitForSeconds(waitTime);
        coupon.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        coupon.GetComponent<Coupon>().CouponShow();

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

    public void AdvanceLevel()
    {
        PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
        levelText.text = $"Runda {PlayerPrefs.GetInt("currentLevel")}";
        if (numbersToSelect < maxNumbersToSelect) numbersToSelect = levelsOfNumbers[PlayerPrefs.GetInt("currentLevel") - 1];
        // Randomize numbers
        rng.Randomize();
        // Select numbers and lit them up
        rng.SelectSpheres();
        // Show the numbers for 15 seconds then sort the spheres
        StartCoroutine(WaitPhase());
    }
}