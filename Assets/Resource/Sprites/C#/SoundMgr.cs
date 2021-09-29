using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundMgr Instance { get; private set; }
    private void Awake()
    {
        
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;
    private AudioClip[] audioClips;
    private void Start()
    {
        audioClips = Resources.LoadAll<AudioClip>("Audio");
        if (BGM) BGM.loop = true;
        if (SFX) SFX.loop = false;
    }
    public void PlayBGM(string name)
    {
        if (BGM && 0 < audioClips.Length)
        {
            foreach (var clip in audioClips)
            {
                if (clip.name.ToLower().Equals(name.ToLower()))
                {
                    BGM.clip = clip;
                    BGM.Play();
                    break;
                }
            }
        }
    }
    public void StopBGM()
    {
        if (BGM)
        {
            BGM.Stop();
        }
    }
    public void PlaySFX(string name)
    {
        if (SFX && 0 < audioClips.Length)
        {
            foreach (var clip in audioClips)
            {
                if (clip.name.ToLower().Equals(name.ToLower()))
                {
                    SFX.PlayOneShot(clip);
                    break;
                }
            }
        }
    }
    
}
