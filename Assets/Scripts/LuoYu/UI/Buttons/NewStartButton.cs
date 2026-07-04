using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStartButton : MonoBehaviour
{
    public void PutDown()
    {
        //加载至第一关
        LevelManager.instance.EnterLevel(1);
    }
}
