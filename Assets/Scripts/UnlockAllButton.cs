using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{
    public int maxLevel;

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("UnlockedLevel", maxLevel);
        PlayerPrefs.Save();

        Object.FindFirstObjectByType<LockManager>().UpdateLocks();
        Debug.Log("All levels unlocked");
    }
}