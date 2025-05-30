using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    private void OnMouseDown()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();

        Object.FindFirstObjectByType<LockManager>().UpdateLocks();
        Debug.Log("Progress resetted");
    }
}