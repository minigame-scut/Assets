using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;//单实例类

    public AudioSource MusicPlayer;//播放背景音乐的组件
    public AudioSource SoundPlayer;//播放音效的组件

    void Start()
    {
        Instance = this;//初始化该实例类
    }

    //播放背景音乐
    public void PlayMusic(string name)
    {
        //如果当前背景音乐没有播放，播放给定的背景音乐（循环播放）
        if (!MusicPlayer.isPlaying)
        {
            //给定的音乐资源必须在Resource文件夹中
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }

    //停止播放背景音乐
    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    //播放音效
    public void PlaySound(string name)
    {
        //给定的音效资源必须在Resource文件夹中
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.PlayOneShot(clip);
    }
}

