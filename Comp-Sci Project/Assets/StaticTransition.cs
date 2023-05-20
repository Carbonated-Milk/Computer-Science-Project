using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticTransition : MonoBehaviour
{
    static public StaticTransition singleton;
    void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject); 
    }

    private IEnumerator ChangeScene(int scene)
    {
        float transitionTime = .5f;

        ScreenTransition.singleton.TransitionOUT(transitionTime);

        yield return new WaitForSecondsRealtime(transitionTime);


        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(scene);
        loadingScene.allowSceneActivation = true;


        yield return new WaitUntil(() => { return loadingScene.isDone || loadingScene == null; });

        ScreenTransition.singleton.TransitionIN(transitionTime);

        yield return new WaitForSecondsRealtime(transitionTime);
    }

    public static void TransitionToScene(int scene)
    {
        singleton.StartCoroutine(singleton.ChangeScene(scene));
    }

}
