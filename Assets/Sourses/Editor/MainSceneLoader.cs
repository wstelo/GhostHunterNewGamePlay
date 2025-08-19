using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class MainSceneLoader
{
    private const string MainScenePath = "Assets//Scenes/Boot.unity";
    private const string PREF_KEY_PREV_SCENE = "PREVIOUS SCENE";

     static MainSceneLoader()
    {
     //   EditorApplication.playModeStateChanged += ChangePlayModeState;
    }

    private static void ChangePlayModeState(PlayModeStateChange state)
    {
        if(EditorApplication.isPlaying == false && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }

            var path = SceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PREF_KEY_PREV_SCENE, path);

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                try
                {
                    EditorSceneManager.OpenScene(MainScenePath);
                }
                catch
                {
                    Debug.LogError($"Scene {MainScenePath} don't load");
                    EditorApplication.isPlaying = false;
                }
            }
            else
            {
                EditorApplication.isPlaying = false;
            }
        }

        if (EditorApplication.isPlaying == false && EditorApplication.isPlayingOrWillChangePlaymode == false)
        {
            var path = EditorPrefs.GetString(PREF_KEY_PREV_SCENE);

            try
            {
                EditorSceneManager.OpenScene(path);
            }
            catch
            {
                Debug.LogError($"Scene {path} don't load");
                EditorApplication.isPlaying = false;
            }
        }
    }
}
