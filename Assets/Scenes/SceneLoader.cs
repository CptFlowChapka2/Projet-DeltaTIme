using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneLoaderEncaps encaps;
    public SceneRefEncaps currentScene;
    

    AsyncOperation asyncLoad;
    bool bLoadDone=true;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        encaps.SceneLoader = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadNextSceneInGroup();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPreviousSceneInGroup();
        }
    }

    public void LoadNextSceneInGroup()
    {
        LoadScene(currentScene.nextSceneInGroup);
    }
    public void LoadPreviousSceneInGroup()
    {
        LoadScene(currentScene.previousSceneInGroup);
    }

    public void LoadScene(SceneRefEncaps sceneRefEncaps)
    {
        if(asyncLoad is not null&&!asyncLoad.isDone) return;
        if(SceneManager.GetActiveScene()==SceneManager.GetSceneByName(sceneRefEncaps.name))return;
        StartCoroutine(LoadYourAsyncScene(sceneRefEncaps));
    }
    
    private IEnumerator LoadYourAsyncScene(SceneRefEncaps sceneRefEncaps)
    {
        
        asyncLoad = SceneManager.LoadSceneAsync(sceneRefEncaps.name,LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            //scene has loaded as much as possible,
            // the last 10% can't be multi-threaded
            if (asyncLoad.progress >= 0.9f)
            {
                currentScene = sceneRefEncaps;
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        
    }
}
