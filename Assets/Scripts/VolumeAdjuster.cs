using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjuster : MonoBehaviour
{
    [SerializeField] float drainRate = 3;
    [SerializeField] Text volumeTxt = null;

    AudioSource music;
    float currentVolume = 0;
    
    public static VolumeAdjuster Instance;

    void Awake()
    {
        Instance = this;
        music = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentVolume = GetSystemVolume(VolumeUnit.Scalar) * 100;
        volumeTxt.text = ((int)currentVolume).ToString();
        music.volume = (currentVolume / 100);
    }

    [DllImport("vladjust")]
    public static extern float GetSystemVolume(VolumeUnit vUnit);
    [DllImport("vladjust")]
    public static extern void SetSystemVolume(double newVolume, VolumeUnit vUnit);

    public void AdjustVolume(float value)
    {
        float adjustment;
        if (value <= 0.1f)
            adjustment = -drainRate / 10;
        else
            adjustment = value / 2;

        currentVolume = Mathf.Clamp(currentVolume + adjustment, 0, 100);
        volumeTxt.text = ((int)currentVolume).ToString();

        float volumePercent = currentVolume / 100;

        SetSystemVolume(volumePercent, VolumeUnit.Scalar);
        music.volume = volumePercent;        
    }

    public void StartMusicPlay()
    {
        music.Play();
    }

    public void StopMusicPlay()
    {
        music.Pause();
    }
}

//The Unit to use when getting and setting the volume
public enum VolumeUnit
{
    //Perform volume action in decibels</param>
    Decibel,
    //Perform volume action in scalar
    Scalar
}