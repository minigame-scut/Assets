using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private float musicVolume;
    private float soundVolume;

    private Slider volumeControl;
    private GameObject audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GameObject.Find("AudioManager");
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void changeMusicVolum()
    {
        volumeControl = GameObject.FindWithTag("volumeControl").GetComponent<Slider>();
        musicVolume = volumeControl.value;
        audioSource.GetComponent<AudioManager>().setMusciVolume(musicVolume);
    }

    public void changeSoundVolum()
    {
        volumeControl = GameObject.FindWithTag("volumeControl").GetComponent<Slider>();
        soundVolume  = volumeControl.value;
        audioSource.GetComponent<AudioManager>().setSoundVolume(soundVolume);
    }
}
