using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer music, effects;

    public AudioSource hit, enemydead, backgroundMusic, daHeroe,arrow,saltoHeroe,daZom,run,gameOver,posion;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null) { 
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
