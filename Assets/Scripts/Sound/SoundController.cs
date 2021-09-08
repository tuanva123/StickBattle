using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public enum SourceAudio { Music, Effect, Coin };
    [SerializeField]
    // private AudioMixer mixer;
    public AudioSource musicSource;


    //clip sfx
    public AudioClip shoot1;
    public AudioClip shoot2;
    public AudioClip shoot3;
    public AudioClip booster1;
    public AudioClip booster2;
    public AudioClip booster3;
    public AudioClip sfxBoom;

    public AudioClip enemyDie;
    public AudioClip enemyHitDame;
    public AudioClip playerDie;
    public AudioClip playerHitDame;
    public AudioClip playerJump;
    public AudioClip playerPunch;

    public AudioClip win;
    public AudioClip lose;
    

    //clip music;
    public AudioClip BGM;
    public AudioClip bg_Bos;




    private AudioClip _currentMusic;
    private static SoundController _instance;
    public static SoundController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundController>();
                if (_instance != null)
                    DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    public bool StateMusic
    {
        set
        {
            bool _state = value;
            PlayerPrefs.SetInt("StateMusicOnOff", _state == true ? 1 : 0);
        }
        get
        {
            int _idState = PlayerPrefs.GetInt("StateMusicOnOff",1);
            return _idState == 1 ? true : false;
        }
    }
    public bool StateSound
    {
        set
        {
            bool _state = value;
            PlayerPrefs.SetInt("StateSoundOnOff", _state == true ? 1 : 0);
        }
        get
        {
            int _idState = PlayerPrefs.GetInt("StateSoundOnOff", 1);
            return _idState == 1 ? true : false;
        }
    }
    public bool StateVibration
    {
        set
        {
            bool _state = value;
            PlayerPrefs.SetInt("StateVibrationOnOff", _state == true ? 1 : 0);
        }
        get
        {
            int _idState = PlayerPrefs.GetInt("StateVibrationOnOff",1);
            return _idState == 1 ? true : false;
        }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
        musicSource = GetComponent<AudioSource>();
    }

    public void PauseMusic()
    {
        //Play the clip.
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        //Play the clip.
        musicSource.UnPause();
    }
    //public void StopMusic()
    //{
    //    musicSource.Stop();
    //}
    //public bool IsMusicPlaying()
    //{
    //    return musicSource.isPlaying;
    //}
    public void PlayBGMusic(AudioClip clip)
    {
        if (clip == null) return;
        GetComponent<AudioSource>().clip = clip;
        if (StateMusic)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Pause();
        }
    }
    #region === Play Sound ===
    public void PlaySfx(AudioClip clip, float volume = 1)
    {
        if (!StateSound || clip == null)
        {
            return;
        }
        // Debug.Log(clip.name);
        GetComponent<SoundPool>().GetSfx(clip, volume);
    }
    #endregion
}
