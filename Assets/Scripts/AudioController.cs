using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioClip bingClip;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void PlayComboSound(int comboAmount)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.pitch = .9f + comboAmount * 0.1f;
        audioSource.PlayOneShot(bingClip);

        Destroy(audioSource, bingClip.length);
    }
}
