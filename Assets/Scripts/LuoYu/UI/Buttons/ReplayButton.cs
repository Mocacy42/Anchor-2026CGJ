using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public void PutDown()
    {
        //重新加载当前场景
        LevelManager.instance.EnterLevel(LevelManager.instance.currentLevelIndex);
    }
}
