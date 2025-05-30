using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpriteButton : MonoBehaviour
{
    public int levelIndex;
    public GameObject lockIcon;

    private bool isUnlocked;

    private void Start()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        isUnlocked = levelIndex <= unlocked;

        if (lockIcon != null)
            lockIcon.SetActive(!isUnlocked);
    }

    private void OnMouseDown()
    {
        if (isUnlocked)
        {
            SceneManager.LoadScene("Level" + levelIndex);
        }
        else
        {
            Debug.Log("Level " + levelIndex + " is locked.");
        }
    }
}