using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance = null;//单实例类

    public AudioSource musicPlayer;//播放背景音乐的组件
    public AudioSource soundPlayer;//播放音效的组件

    public static AudioManager getInstance()
    {
        if (Instance == null)
        {
            Instance = new AudioManager();
        }
        return Instance;
    }   

    void Start()
    {
        //Instance = this;//初始化该实例类
        
    }

    //播放背景音乐
    public void PlayMusic(string name)
    {
        //如果当前背景音乐没有播放，播放给定的背景音乐（循环播放）
        if (!musicPlayer.isPlaying)
        {
            //给定的音乐资源必须在Resource文件夹中
            AudioClip clip = Resources.Load<AudioClip>(name);
            musicPlayer.clip = clip;
            musicPlayer.Play();
        }
    }

    //停止播放背景音乐
    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    //播放音效
    public void PlaySound(string name)
    {
        //给定的音效资源必须在Resource文件夹中
        AudioClip clip = Resources.Load<AudioClip>(name);
        soundPlayer.PlayOneShot(clip);
    }

    public void setMusciVolume(float mv)
    {
        musicPlayer.volume = mv;
    }
    public void setSoundVolume(float sv)
    {
        soundPlayer.volume = sv;
    }
}

