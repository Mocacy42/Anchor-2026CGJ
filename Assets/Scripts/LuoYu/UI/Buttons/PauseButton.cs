using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PassPanelManager passPanelManager;
    public void PutDown()
    {
        //Ω˚”√ÕÊº“ ‰»Î
        InputInstance.Instance.PInput.Player.Disable();
        Time.timeScale = 0f;
        passPanelManager.PauseLevel();
    }
}
