using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //单例
    public static LevelManager instance;
    //关卡列表
    [SerializeField] public List<string> sceneName = new List<string>();
    //当前关卡下标
    [SerializeField] public int currentLevelIndex;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }
    //进入关卡(以关卡在列表中的下标为索引)
    public void EnterLevel(int index)
    {
        SceneTransitionManager.instance.LoadScene(sceneName[index]);
        currentLevelIndex = index;
    }
}
