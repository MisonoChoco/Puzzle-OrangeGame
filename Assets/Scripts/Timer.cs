using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FruitController;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60f;
    public float startDelay = 0.5f;
    public bool isCounting = true;

    private float delayTimer = 0.8f;

    private void Start()
    {
        delayTimer = startDelay;
    }

    private void Update()
    {
        // Wait before starting countdown
        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        if (!isCounting) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0, timeRemaining);

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";

        //lose condition
        if (timeRemaining <= 0f)
        {
            isCounting = false;
            SceneTracker.lastLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Lose");
        }
    }

    public void StopTimer()
    {
        isCounting = false;
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        isCounting = true;
        delayTimer = startDelay;
    }
}