using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class MusicManager : BaseManager<MusicManager>
{
    private AudioSource bkMusic = null;

    private float bkValue = 1;

    private GameObject soundObj = null;

    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 1;


    public MusicManager()
    {
        MonoManager.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        for(int i = soundList.Count - 1; i >= 0; --i)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isLoop"></param>
    /// <param name="callBack"></param>
    public void PlaySound(string name, bool isLoop,float soundValue, UnityAction<AudioSource> callBack = null)
    {
        if(soundObj == null)
        {
            //soundObj = new GameObject();
            //soundObj.name = "Sound";
            soundObj = GameObject.FindGameObjectWithTag("Sound");
        }
        //路径根据实际位置更改
        ResManager.GetInstance().LoadAsync<AudioClip>("Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = soundObj.GetComponent<SoundObj>().m_MixerGroup;
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            //source.spatialBlend = 0.6f;
            source.Play();
            soundList.Add(source);
            callBack?.Invoke(source);
        });
    }
    /// <summary>
    /// 停止音效
    /// </summary>
    /// <param name="source"></param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
    /// <summary>
    /// 改变音效大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for(int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = value;
        }
    }
}
