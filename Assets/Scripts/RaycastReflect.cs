using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflect : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    public GameObject[] reflectors;
    public float[] reflectorsCoeff;

    public float[] hitDistance;
    public float totalDistance;


    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    public GameObject listener;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        listener = GameObject.FindGameObjectWithTag("Listener");

    }

    private void Update()
    {
        direction = listener.transform.position - transform.position;
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
                if (hit.collider.tag == "Mirror" && i < reflectors.Length)
                {
                    reflectors[i] = hit.collider.gameObject;
                    reflectorsCoeff[i] = hit.collider.gameObject.GetComponent<Reflector>().absorptionCoeff;
                }

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
                lineRenderer.SetPosition(lineRenderer.positionCount-1, ray.origin + ray.direction * remainingLength);
            }
            
        }
    }

}
