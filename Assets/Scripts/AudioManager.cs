using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] bgm;
    public AudioReverbFilter bgmReverb;
    public AudioEchoFilter bgmEcho;
    public static AudioManager instance => Instance();
    public static AudioManager Instance(AudioManager over = null)
    {
        // Create the singleton if it doesn't exist, otherwise return the singleton
        if (_instance == null)
        {
            if (over == null)
            {
                // This loads a prefab to create this singleton (This allows settings to be added in the editor via prefab)
                GameObject registry = Instantiate(Registry.instance.prefabs["AudioManager"]);
                _instance = registry.GetComponent<AudioManager>();
            }
            else _instance = over;
        }
        return _instance;
    }
    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay >= 0 && soundToPlay < sfx.Length) sfx[soundToPlay].Play();
    }
    public AudioClip GetSFX(int soundToPlay)
    {
        if (soundToPlay >= 0 && soundToPlay < sfx.Length) return sfx[soundToPlay].clip;
        return null;
    }
    public void PlayBGM(int musicToPlay)
    {
        if (musicToPlay >= 0 && musicToPlay < bgm.Length)
        {
            AudioSource track = bgm[musicToPlay];
            if (!track.isPlaying)
            {
                StopMusic();
                bgm[musicToPlay].Play();
            }
        }
    }
    public void StopMusic()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    public void SetDistortion(bool distort)
    {
        bgmEcho.enabled = distort;
        bgmReverb.enabled = distort;
    }
    private static AudioManager _instance;
    private void Awake()
    {
        if (Instance(this) != this) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}