using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    //单例
    public static SceneTransitionManager instance;

    //转场画布引用
    [SerializeField] private Canvas TransitionCanvas;
    //进度条引用
    [SerializeField] private Slider slider;
    //协程变量，防止重复触发
    [SerializeField] private Coroutine currentCoroutine;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //加载指定场景
    public void LoadScene(string sceneName)
    {
        if(instance != null && currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(TransitionRoutine(sceneName));
        }else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator TransitionRoutine(string sceneName)
    {
        TransitionCanvas.gameObject.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while(asyncLoad.progress < 0.9f)
        {
            slider.value = asyncLoad.progress;
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        TransitionCanvas.gameObject.SetActive(false);
    }
}
