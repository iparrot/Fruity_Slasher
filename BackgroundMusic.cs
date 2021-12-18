using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource music;
    static float seconds =0;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMusicVolume(PlayerPrefs.GetInt("isVolumeOn"));
        music.time = seconds;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveMusicTime()
    {
        seconds = music.time;
    }

    public void SetMusicVolume(float _volume)
    {
        if (_volume != 0) _volume /= 2;
        music.volume = _volume;
    }
}
