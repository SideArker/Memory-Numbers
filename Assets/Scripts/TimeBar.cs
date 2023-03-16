using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour
{
    [Header("Time variables")]
    public const float maxTime = 15f;

    public bool timerStart = false;

    Image image;
    float lerpValue;
    [Header("Objects")]
    [SerializeField] Animator animator;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(timerStart) { StartCoroutine(Timer()); }
    }

    public IEnumerator Timer()
    {
        float currentTime = 0;
        while (currentTime < maxTime)
        {
            lerpValue = Mathf.Lerp(1, 0, currentTime / maxTime);
            currentTime += Time.deltaTime;

            image.fillAmount = lerpValue;
            yield return null;

        }
        image.fillAmount = 0;

        animator.Play("TimerOut");
    }
}
