using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontQuitLevelButton : MonoBehaviour
{
    //退出关卡界面引用
    [SerializeField] private GameObject quitLevelPanel;
    public void PutDown()
    {
        Time.timeScale = 1.0f;
        quitLevelPanel.SetActive(false);
    }
}
