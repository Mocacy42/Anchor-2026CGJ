using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void PutDown()
    {
        Time.timeScale = 1.0f;
        //加载下一场景
        if(LevelManager.instance.currentLevelIndex + 1 <= LevelManager.instance.sceneName.Count)
        {
            LevelManager.instance.EnterLevel(LevelManager.instance.currentLevelIndex + 1);
        }else
        {
            LevelManager.instance.EnterLevel(0);
        }
    }
}
