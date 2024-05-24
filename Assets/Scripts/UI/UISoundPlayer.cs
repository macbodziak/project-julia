using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClicked;
    [SerializeField] private AudioClip wonSound;
    [SerializeField] private AudioClip lostSound;
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
            Debug.Assert(buttonClicked);
            Debug.Assert(wonSound);
            Debug.Assert(lostSound);
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



    public void PlayWonSound()
    {
        audioSource.clip = wonSound;
        audioSource.Play();
    }



    public void PlayLostSound()
    {
        audioSource.clip = lostSound;
        audioSource.Play();
    }
}
