using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSourceHandler : MonoBehaviour
{
    [Tooltip("How many orders of reflections")]
    public int maxOrder;
    public GameObject[] ImgSourcePrefabs;
    public Material[] orderMaterials;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject floor;
    public GameObject ceiling;

    public Vector3 lastPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        lastPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != lastPos)
        {
            //do something on position change
        }
        lastPos = gameObject.transform.position;
    }
}
