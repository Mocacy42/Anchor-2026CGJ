using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //单例
    public static SFXManager instance;

    //音乐播放器引用
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //播放音效
    public void PlayBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    //设置声音大小
    public void SetBGMVolume(float volume) => audioSource.volume = volume;
}
