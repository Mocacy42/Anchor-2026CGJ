using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitLevelButton : MonoBehaviour
{
    public void PutDown()
    {
        Time.timeScale = 1.0f;
        LevelManager.instance.EnterLevel(4);
    }
}
