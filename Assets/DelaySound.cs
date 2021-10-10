using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySound : MonoBehaviour
{
    AudioSource audioSource;
    SoundSource soundSource;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundSource = GetComponent<SoundSource>();
        lastPos = transform.position;

        Debug.Log("Distance" + (int)soundSource.delayTimeInSamples);
        float[] samples = new float[audioSource.clip.samples * audioSource.clip.channels + (int)soundSource.delayTimeInSamples];
        audioSource.clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = samples[i - (int)soundSource.delayTimeInSamples];
        }

        audioSource.clip.SetData(samples, 0);
    }

    // Update is called once per frame
    void Update()
    { 
    //{

    //    if (transform.position != lastPos)
    //    {
    //        float[] newSamples = new float[(audioSource.clip.samples *
    //            audioSource.clip.channels) + (int)soundSource.delayTimeInSamples];
    //        audioSource.clip.GetData(newSamples, 0);

    //        for (int i = 0; i < newSamples.Length; i++)
    //        {
    //            newSamples[i] = 1.0f * newSamples[i - (int)soundSource.delayTimeInSamples];
    //        }

    //        audioSource.clip.SetData(newSamples, 0);
    //    }
    }
}
