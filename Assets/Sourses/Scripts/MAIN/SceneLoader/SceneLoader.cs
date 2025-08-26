using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public IEnumerator LoadScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneName, sceneMode);

        yield return new WaitUntil(() => waitLoading.isDone);
    }
}
