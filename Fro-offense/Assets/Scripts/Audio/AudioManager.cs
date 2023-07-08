using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float SFX_Volume = 1.0f;
    [SerializeField] [Range(0, 1)] float OST_Volume = 1.0f;
    [SerializeField] internal AudioSource SFX_AudioSource;
    [SerializeField] internal AudioSource OST_AudioSource;

    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;

            SFX_Volume = PlayerPrefs.GetFloat("sfxVolume", SFX_Volume);
            OST_Volume = PlayerPrefs.GetFloat("ostVolume", OST_Volume);

            SFX_AudioSource.volume = SFX_Volume;
            OST_AudioSource.volume = OST_Volume;
        }

    }
    private void Start()
    {
        //Play_OST("InGameMusic");
        //Play_OST("MenuMusic");
    }

    private void Update()
    {
        SFX_AudioSource.volume = SFX_Volume;
        OST_AudioSource.volume = OST_Volume;
    }



    public void Play_SFX(string _audioName, float _customPitch = -1.0f)
    {
        GameObject loadObject = Resources.Load<GameObject>("Audio/SFX/" + _audioName);
        if (loadObject == null)
        {
            Debug.LogError("Audio " + _audioName + " not found");
            return;
        }

        SoundData clipData = loadObject.GetComponent<SoundData>();
        SFX_AudioSource.volume = clipData.volume;
        if (_customPitch <= 0) SFX_AudioSource.pitch = clipData.pitch;
        else SFX_AudioSource.pitch = _customPitch;
        SFX_AudioSource.loop = clipData.loops;

        SFX_AudioSource.PlayOneShot(clipData.clip);
    }
    public void Play_SFX(string _audioName, AudioSource _audioSource, bool _usePrefabData = true)
    {
        GameObject loadObject = Resources.Load<GameObject>("Audio/SFX/" + _audioName);
        if (loadObject == null)
        {
            Debug.LogError("Audio " + _audioName + " not found");
            return;
        }

        SoundData clipData = loadObject.GetComponent<SoundData>();
        if (_usePrefabData)
        {
            _audioSource.volume = clipData.volume;
            _audioSource.pitch = clipData.pitch;
            _audioSource.loop = clipData.loops;
        }

        _audioSource.PlayOneShot(clipData.clip);
    }

    public void Stop_OST()
    {
        OST_AudioSource.volume = 0.1f;
        OST_AudioSource.Stop();
    }

    public void Play_OST(string _audioName)
    {
        GameObject loadObject = Resources.Load<GameObject>("Audio/OST/" + _audioName);
        if (loadObject == null)
        {
            Debug.LogError("Audio " + _audioName + " not found");
            return;
        }

        SoundData clipData = loadObject.GetComponent<SoundData>();
        OST_AudioSource.volume = clipData.volume;
        OST_AudioSource.pitch = clipData.pitch;
        OST_AudioSource.loop = clipData.loops;

        OST_AudioSource.clip = clipData.clip;
        OST_AudioSource.Play();
    }


    public bool OST_IsPlaying()
    {
        return OST_AudioSource.isPlaying;
    }


    //IEnumerator PlayMusicTest()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Play_OST("ThinkingMusic");

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5.0f);
    //        Play_SFX("coinSFX");
    //    }
    //}

}
