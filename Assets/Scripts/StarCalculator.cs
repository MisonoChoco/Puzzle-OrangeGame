using UnityEngine;
using UnityEngine.UI;

public class StarCalculator : MonoBehaviour
{
    public GameObject[] stars;

    private void Start()
    {
        float timeLeft = PlayerPrefs.GetFloat("TimeLeft", 0f);
        int starCount = 0;

        if (timeLeft >= 25f)
            starCount = 3;
        else if (timeLeft >= 15f)
            starCount = 2;
        else if (timeLeft >= 10f)
            starCount = 1;

        ShowStars(starCount);
    }

    private void ShowStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }
}