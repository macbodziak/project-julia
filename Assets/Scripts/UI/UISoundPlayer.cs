using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClicked;
    private AudioSource audioSource;
    static private UISoundPlayer _instance;

    static public UISoundPlayer Instance { get => _instance; }
    public AudioSource Source { get => audioSource; }

    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayButtonClickedSound()
    {
        audioSource.clip = buttonClicked;
        audioSource.Play();
    }
}
