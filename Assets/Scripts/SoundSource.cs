using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundSource : MonoBehaviour
{
    public Reflector reflection;
    public int order;
    public GameObject listener;

    public Vector3 lastPos = new Vector3(0, 0, 0);

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    float maxLength = 100.0f;
    int reflections = 1;
    public float[] hitDistance;
    public float distanceToListener;
    public float delayTimeInSeconds;
    public float delayTimeInSamples;
    public float distanceAttenuation;

    public float reflectionTotal = 1.0f;

    AudioSource audioSource;

    private void Start()
    {
        lastPos = gameObject.transform.position;
        listener = GameObject.FindGameObjectWithTag("Listener");

        //raycast line renderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        direction = listener.transform.position - transform.position;

        audioSource = GetComponent<AudioSource>();

        //setting variables
        distanceToListener = Vector3.Distance(listener.transform.position, transform.position);

        delayTimeInSeconds = distanceToListener / GlobalSettings.speedOfSound;
        delayTimeInSamples = GlobalSettings.sampleRate * delayTimeInSeconds;
        distanceAttenuation = 1 / distanceToListener;
    }

    private void Update()
    {
        if (order != 0)
        {
            ray = new Ray(transform.position, direction);

            lineRenderer.positionCount = 1; //indexing from 0
            lineRenderer.SetPosition(0, transform.position);
            float remainingLength = maxLength;

            for (int i = 0; i < reflections; i++)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength)) //if we hit something, add a source
                {
                    //get distance of first hit
                    hitDistance[i] = hit.distance;

                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    remainingLength -= Vector3.Distance(ray.origin, hit.point);

                    //generate a new ray from the hitpoint 
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                    if (hit.collider.tag != "Mirror")
                        break;
                }
                else
                {
                    //Debug.Log(lineRenderer.positionCount);
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                }

            }
        }

        //update distance
        distanceToListener = Vector3.Distance(listener.transform.position, transform.position);

        delayTimeInSeconds = distanceToListener / GlobalSettings.speedOfSound;
        delayTimeInSamples = GlobalSettings.sampleRate * delayTimeInSeconds;

        //play on Space, stop on p while SourceSpawner.cs resets
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("play!");
            audioSource.volume = distanceAttenuation * reflectionTotal;
            audioSource.PlayDelayed(delayTimeInSeconds);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.volume = 0.0f;
            audioSource.Stop();
        }
    }
}