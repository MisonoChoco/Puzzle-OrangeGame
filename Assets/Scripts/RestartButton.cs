using UnityEngine;
using UnityEngine.SceneManagement;
using static FruitController;

public class RestartLevelButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (SceneTracker.lastLevelIndex >= 0)
        {
            SceneManager.LoadScene(SceneTracker.lastLevelIndex);
        }
        else
        {
            Debug.LogWarning("No previous level recorded!");
        }
    }
}