using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartImmediateButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}