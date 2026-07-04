using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    //µ¥Àý
    public static BGMManager instance;

    //ÒôÀÖ²¥·ÅÆ÷ÒýÓÃ
    [SerializeField] private AudioSource audioSource;

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
    //²¥·ÅÒôÀÖ
    public void PlayBGM(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    //ÔÝÍ£ÒôÀÖ
    public void StopBGM() => audioSource.Stop();
    //ÉèÖÃÉùÒô´óÐ¡
    public void SetBGMVolume(float volume) => audioSource.volume = volume;
}
