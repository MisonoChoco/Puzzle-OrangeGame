using UnityEngine;

public class LockManager : MonoBehaviour
{
    public GameObject[] levelButtons;
    public GameObject[] lockIcons;

    private void OnEnable()
    {
        UpdateLocks();
    }

    public void UpdateLocks()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = i < unlockedLevel;
            levelButtons[i].SetActive(true);
            lockIcons[i].SetActive(!isUnlocked);
        }
    }
}