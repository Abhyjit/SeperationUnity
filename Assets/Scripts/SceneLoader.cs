using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    }

   
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
