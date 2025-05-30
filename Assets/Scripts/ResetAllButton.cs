using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();

        Object.FindFirstObjectByType<LockManager>().UpdateLocks();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Progress resetted");
    }
}