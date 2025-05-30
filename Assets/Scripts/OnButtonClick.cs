using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickLoader : MonoBehaviour
{
    // The name of the scene to load
    public string sceneToLoad;

    private void OnMouseDown()
    {
        // This method is called when the image is clicked
        SceneManager.LoadScene(sceneToLoad);
    }
}