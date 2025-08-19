using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;

    private SceneLoader _loader;

    private void Awake()
    {
        _loader = new SceneLoader();

        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitUntil(() => _loadingScreen.IsReady);

        yield return new WaitKeyDown();

        yield return _loader.LoadScene("MainMenu");
    }
}
