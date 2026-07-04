using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelButton : MonoBehaviour
{
    public void PutDown()
    {
        //加载至选关界面
        LevelManager.instance.EnterLevel(0);
    }
}
