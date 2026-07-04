using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    //文本引用
    [SerializeField] private TMP_Text timeCounterText;
    //计时器
    public float timeCounter;

    private void Update()
    {
        timeCounter -= Time.deltaTime;

        timeCounterText.text = $"{timeCounter / 60}：{timeCounter % 60}";
    }
}
