using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSound : MonoBehaviour {


    //音乐文件
    public AudioClip[] music;
    private AudioSource bgmusic;
    //音量
    private float musicVolume;
    void Start () {
        bgmusic=gameObject.GetComponent<AudioSource>();
        musicVolume = 0.5F;
        if (!bgmusic.isPlaying)
        {
            bgmusic.clip = music[Random()];
            //关闭音乐
            bgmusic.Play();
        }
    }
    private int Random()
    {
        //生成水滴
        System.Random ranNum = new System.Random(System.Guid.NewGuid().GetHashCode());
        int x = ranNum.Next(6);
        return x;
    }
}
