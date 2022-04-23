using System;
using UnityEngine;

public class Sound : MonoSingleton<Sound>
{
	private AudioSource m_bg;
	private AudioSource m_effect;
    public string resourceDir = "";

    protected override void Awake()
    {
        base.Awake();
        m_bg = gameObject.AddComponent<AudioSource>();
        m_bg.playOnAwake = false;
        m_bg.loop = true;

        m_effect = gameObject.AddComponent<AudioSource>();

    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName">音频名称</param>
    public void PlayBgAudio(string audioName)
    {
        string oldName;
        if (m_bg.clip == null)
        {
            oldName = "";
        }
        else
        {
            oldName = m_bg.clip.name;
        }

        if (audioName != oldName)
        {
            string path = resourceDir + "/" + audioName;
            AudioClip clip = Resources.Load<AudioClip>(path);
            if (clip != null)
            {
                m_bg.clip = clip;
                m_bg.Play();
            }
            else
            {
                Debug.LogError("未找到音频资源，路径为：" + path);
            }
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName">音频名称</param>
    public void PlayEffectAudio(string audioName)
    {
        string path = resourceDir + "/" + audioName;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            m_effect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("未找到音效资源，路径为：" + path);
        }
    }
}