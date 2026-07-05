using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevelButton : MonoBehaviour
{
    public void EnterLevel_1()
    {
        LevelManager.instance.EnterLevel(1);
    }
    public void EnterLevel_2()
    {
        LevelManager.instance.EnterLevel(2);
    }
    public void EnterLevel_3()
    {
        LevelManager.instance.EnterLevel(3);
    }
}
