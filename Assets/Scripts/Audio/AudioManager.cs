using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public enum AudioKeys 
    {
        Menu, Gameplay, Results, Click, Hit, Collect, Suction, Back, Redo, Purchase
    }

    [System.Serializable]
    public struct AudioObject 
    {
        public AudioKeys key;
        public AudioClip clip;
        public bool loop;
    }
    [SerializeField]
    public AudioObject[] music;
    AudioKeys lastMusicKey = AudioKeys.Click; //made it something that would never be music for error checking reasons
    [SerializeField]
    public AudioObject[] sfx;

    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    public AudioSource musicAudioSrc;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    AudioClip GetMusicClip(AudioKeys key, out bool loop) 
    {
        loop = false;
        AudioClip ac = null;
        foreach(AudioObject obj in music) 
        {
            if (obj.key == key)
            {
                loop = obj.loop;
                return obj.clip;
            }
        }
        return ac;
    }
    AudioClip GetSFXClip(AudioKeys key)
    {
        AudioClip ac = null;
        foreach (AudioObject obj in sfx)
        {
            if (obj.key == key)
                return obj.clip;
        }
        return ac;
    }
    Coroutine _musicRoutine;
    public void TriggerMusic(AudioKeys key) 
    {
        if (lastMusicKey == key)
            return;

        lastMusicKey = key;
        bool shouldLoop;
        AudioClip ac = GetMusicClip(key, out shouldLoop);
        if (_musicRoutine != null) StopCoroutine(_musicRoutine);
        _musicRoutine = StartCoroutine(MusicFadeRoutine(ac, shouldLoop));
    }
    public void TriggerSFX(AudioKeys key)
    {
        AudioClip ac = GetSFXClip(key);
        
        //if this is a memory issue, we can make a pooling system.
        AudioSource src = gameObject.AddComponent<AudioSource>();
        src.clip = ac;
        src.outputAudioMixerGroup = sfxMixerGroup;
        src.Play();
        Destroy(src, ac.length);
        
    }
    IEnumerator MusicFadeRoutine(AudioClip nextClip, bool loop = true) 
    {

        if (musicAudioSrc.clip == null)
        {
            musicAudioSrc.clip = nextClip;
            musicAudioSrc.Play();
        }
        else
        {
            yield return StartCoroutine(FadeTo(-80f, musicMixerGroup, "MusicVolume",0.1f));
            musicAudioSrc.Stop();
            musicAudioSrc.clip = nextClip;
            musicAudioSrc.loop = loop;
            musicAudioSrc.Play();
            yield return StartCoroutine(FadeTo(0f, musicMixerGroup, "MusicVolume",0.1f));
        }
        _musicRoutine=null;
    }
    IEnumerator FadeTo(float toVal, AudioMixerGroup group, string mixerVariable, float duration = 1f) 
    {
        float t = 0f;
        float startVol = 0f;
        group.audioMixer.GetFloat(mixerVariable, out startVol);
        //Debug.Log("Default vol is " + startVol);
        float nextVol = startVol;

        while (t < duration) 
        {
            t+=Time.unscaledDeltaTime;
            float percent = t/duration;
            nextVol = Mathf.Lerp(startVol, toVal, percent);
            group.audioMixer.SetFloat(mixerVariable, nextVol);
            yield return new WaitForEndOfFrame();
        }
        group.audioMixer.SetFloat(mixerVariable, toVal);
    }
}
