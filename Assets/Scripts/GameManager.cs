using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public int BestScore = 0;
    public bool OnMusic = true;
    public bool OnSound = true;
    public bool OnVib = true;
    public int Money = 0;
    public E_Team team = E_Team.SID;

    private Dictionary<string, AudioClip> MusicFiles = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> SoundFiles = new Dictionary<string, AudioClip>();
    private AudioSource MusicAudio;
    private AudioSource SoundAudio;

    private void Awake()
    {
        if(!Instance)    // Singleton pattern
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            if(Instance != this)
                Destroy(gameObject);
        }

        if(MusicFiles.Count <=0)
        {
            InitMusicFiles();
        }

        if (SoundFiles.Count <= 0)
        {
            InitSoundFiles();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios =  GetComponents<AudioSource>();

        MusicAudio = audios[0];
        SoundAudio = audios[1];
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(string name)
    {
        if(MusicAudio != null)
        {
            MusicAudio.Stop();
            MusicAudio.clip = MusicFiles[name];
            MusicAudio.Play();
        }
    }

    public void PlaySound(string name) {
        
        if(SoundAudio != null)
        {
            SoundAudio.PlayOneShot(SoundFiles[name]);
        }
    }

    public void StopMusic()
    {
        if (MusicAudio != null)
        {
            MusicAudio.Stop();
        }
    }

    public void PauseMusic()
    {
        if(MusicAudio != null)
        {
            MusicAudio.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (MusicAudio != null)
        {
            MusicAudio.UnPause();
        }
    }

    public void SwitchMusic(bool flag)
    {
        if(flag) { 
            OnMusic = true;
            MusicAudio.mute = false;
        }
        else { 
            OnMusic = false; 
            MusicAudio.mute = true;
        }
    }

    public void SwitchSound(bool flag)
    {
        if (flag)
        {
            OnSound = true;
            SoundAudio.mute = false;
        }
        else
        {
            OnSound = false;
            SoundAudio.mute = true;
        }
    }

    private void InitMusicFiles()
    {
        MusicFiles.Add("ingamebgm", Resources.Load("Sounds/UI/Sound_Game_Bgm") as AudioClip);
    }

    private void InitSoundFiles()
    {
        // Raid bgm
        SoundFiles.Add("raidbgm", Resources.Load("Sounds/UI/Sound_Game_Warr") as AudioClip);

        // PC sounds
        SoundFiles.Add("norkshootsound", Resources.Load("Sounds/Nork/Sound_PC_Nork1") as AudioClip);
        SoundFiles.Add("norkreloadsound", Resources.Load("Sounds/Nork/Sound_PC_Nork2") as AudioClip);
        SoundFiles.Add("norkskillsound", Resources.Load("Sounds/Nork/Sound_PC_Nork3") as AudioClip);
    }



}
