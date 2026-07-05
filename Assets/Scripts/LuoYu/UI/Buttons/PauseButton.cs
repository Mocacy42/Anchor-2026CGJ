using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PassPanelManager passPanelManager;
    public void PutDown()
    {
        Time.timeScale = 0f;
        passPanelManager.PauseLevel();
    }
}
