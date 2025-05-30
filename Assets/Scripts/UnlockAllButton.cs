using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockAllLevels : MonoBehaviour
{
    public int maxLevel;

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("UnlockedLevel", maxLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Object.FindFirstObjectByType<LockManager>().UpdateLocks();
        Debug.Log("All levels unlocked");
    }
}