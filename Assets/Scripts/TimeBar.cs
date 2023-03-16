using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public class TimeBar : MonoBehaviour
{
    [Header("Time variables")]
    public float currentTime;
    public float maxTime = 15f;

    Image image;
    float lerpValue;
    bool gameIsActive = false;
    [Header("Objects")]
    [SerializeField] Animator animator;

    private void Start()
    {
        currentTime = maxTime;
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (gameIsActive) Timer();
    }

    [Button]
    public void TimerStart()
    {
        gameIsActive = true;
    }

    void Timer()
    {
        if (currentTime > 0)
        {
            lerpValue = Mathf.Lerp(0, 1, currentTime / maxTime);
            currentTime -= Time.deltaTime;

            image.fillAmount = lerpValue;
        }
        else if (currentTime <= 0)
        {
            image.fillAmount = 0;

            animator.Play("TimerOut");
        }
    }
}
