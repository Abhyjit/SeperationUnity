using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;// Required for scene management

public class SceneLoader : MonoBehaviour
{
    // Load a scene immediately by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load a scene with a delay
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    }

    // Coroutine to load the scene after a delay
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
