using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip playClip;

    public void DoSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playClip);
    }
}
