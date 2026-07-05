using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassPanelManager : MonoBehaviour
{
    //通关界面引用
    [SerializeField] private GameObject passPanel;
    //暂停/通关文本引用
    [SerializeField] private TMP_Text passText;
    //通关时间文本引用
    [SerializeField] private TMP_Text passTime;
    //时间计数器引用
    [SerializeField] private TimeCounter timeCounter;
    //下一关卡按钮引用
    [SerializeField] private Button passButton;
    //退出关卡界面引用
    [SerializeField] private GameObject quitLevelPanel;
    //通过关卡
    public void PassLevel()
    {
        passPanel.SetActive(true);
        passTime.text = "通关！";
        passTime.text = $"{(int)timeCounter.timeCounter / 60}：{(int)timeCounter.timeCounter % 60}";
        //通关按钮为白色
        passButton.image.color = Color.white;
        passButton.GetComponent<Button>().enabled = true;
    }
    //暂停关卡
    public void PauseLevel()
    {
        passPanel.SetActive(true);
        passTime.text = "暂停";
        passTime.text = $"{(int)timeCounter.timeCounter / 60}：{(int)timeCounter.timeCounter % 60}";
        //未通关按钮为灰色
        passButton.image.color = Color.gray;
        passButton.GetComponent<Button>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            quitLevelPanel.SetActive(true);
        }
    }
}
