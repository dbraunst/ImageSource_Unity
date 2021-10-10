using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static int sampleRate = 48000;
    public static float speedOfSound = 343.0f; //sound is 343 m/s

    public GameObject source;

    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("Source");

        if (source.GetComponent<AudioSource>().clip != null)
        {
            sampleRate = source.GetComponent<AudioSource>().clip.frequency;
        }
    }
}
