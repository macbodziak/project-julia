using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField][RequiredField] private List<AudioClip> onHitSounds;
    [SerializeField][RequiredField] private AudioClip onDeathSound;
    [SerializeField][RequiredField] private AudioClip onRelieveSound;
    [SerializeField][RequiredField] private AudioClip onDodgeSound;

    public bool SoundEnabled = true;
    private AudioSource _audioSource;


    private void Awake()
    {
        SoundEnabled = true;
        Debug.Assert(onHitSounds != null);
        Debug.Assert(onDeathSound);
        Debug.Assert(onRelieveSound);
        Debug.Assert(onDodgeSound);
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayOnHitSound()
    {
        if (SoundEnabled && onHitSounds.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, onHitSounds.Count);
            _audioSource.PlayOneShot(onHitSounds[randomIndex]);
        }
    }


    public void PlayOnDeathSound()
    {
        if (SoundEnabled)
        {
            _audioSource.PlayOneShot(onDeathSound);
        }
    }


    public void PlayOnDodgeSound()
    {
        if (SoundEnabled)
        {
            _audioSource.PlayOneShot(onDodgeSound);
        }
    }


    public void PlayOnRelieveSound()
    {
        _audioSource.PlayOneShot(onRelieveSound);
    }


    public void PlayClip(AudioClip clip)
    {
        if (SoundEnabled && clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
