using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSourceSpawner : MonoBehaviour
{
    GameObject handlerGO;
    public ImageSourceHandler handler;
    public int order;
    public float totalReflectivity = 1.0f;
    public int wallIndex = 7;

    
    private void Awake()
    {
        //set handler object for easier accessing
        handlerGO = GameObject.FindGameObjectWithTag("Source");
        handler = handlerGO.GetComponent<ImageSourceHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // if this object is of a lesser order than the set max, spawn more
        if (order < handler.maxOrder)
        {
            StartCoroutine(Spawn());
        }

    }


    // Update is called once per frame
    void Update()
    {
        //This function does not currently work as intended
        //MoveImages();

        //Destroy soundsources and respawn them based on new source locations
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject[] imageSources = GameObject.FindGameObjectsWithTag("ImageSource");

            Debug.Log("destroy!");
            for (int i = 0; i < imageSources.Length; i++)
            {
                Destroy(imageSources[i]);
            }

            StartCoroutine(Spawn());
        }
    }

    //slight pause between order spawning for dramatic effect
    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnImages();
    }

    //spawn Images
    void SpawnImages()
    {
        SpawnLeftImage();
        SpawnRightImage();
        SpawnTopImage();
        SpawnBottomImage();
        SpawnFloorImage();
        SpawnCeilingImage();
    }

    //void MoveImages()
    //{
    //    switch (wallIndex)
    //    {
    //        case 0: //left
    //            transform.position = new Vector3(handler.leftWall.GetComponent<Transform>().position.x -
    //                        Mathf.Abs(handler.leftWall.GetComponent<Transform>().position.x - transform.position.x),
    //                         transform.position.y, transform.position.z);
    //            break;
    //        case 1: //right
    //            transform.position = new Vector3(handler.rightWall.GetComponent<Transform>().position.x +
    //                        Mathf.Abs(handler.rightWall.GetComponent<Transform>().position.x - transform.position.x),
    //                         transform.position.y, transform.position.z);
    //            break;
    //        case 2: //top
    //            transform.position = new Vector3(transform.position.x, transform.position.y,
    //                handler.topWall.GetComponent<Transform>().position.z +
    //                Mathf.Abs(handler.topWall.GetComponent<Transform>().position.z - transform.position.z));
    //            break;
    //        case 3: //bottom
    //            transform.position = new Vector3(transform.position.x, transform.position.y,
    //                handler.topWall.GetComponent<Transform>().position.z -
    //                Mathf.Abs(handler.topWall.GetComponent<Transform>().position.z - transform.position.z));
    //            break;
    //        case 4: //floor
    //            transform.position = new Vector3(transform.position.x,
    //                handler.floor.GetComponent<Transform>().position.y -
    //                Mathf.Abs(handler.floor.GetComponent<Transform>().position.y - transform.position.y),
    //                transform.position.z);
    //            break;
    //        case 5: //ceiling
    //            transform.position = new Vector3(transform.position.x,
    //                handler.floor.GetComponent<Transform>().position.y +
    //                Mathf.Abs(handler.floor.GetComponent<Transform>().position.y - transform.position.y),
    //                transform.position.z);
    //            break;
    //        default:
    //            break;

    //    }
    //}

    //Spawn individual image sources
    void SpawnLeftImage()
    {
        if (wallIndex != 0) //prevent double-spawning
        {
            //modify current reflectivity by coefficient of wall this image 'reflected off of'
            totalReflectivity = totalReflectivity * handler.leftWall.GetComponent<Reflector>().absorptionCoeff;

            //instantiate new GO offset from wall position
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                        new Vector3(handler.leftWall.GetComponent<Transform>().position.x -
                            Mathf.Abs(handler.leftWall.GetComponent<Transform>().position.x - transform.position.x),
                             transform.position.y, transform.position.z), Quaternion.identity);

            //set the instantiated object's parameters
            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.leftWall.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 0;
        }
    }

    void SpawnRightImage()
    {
        Debug.Log("SpawnRight");
        if (wallIndex != 1)
        { 
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                        new Vector3(handler.rightWall.GetComponent<Transform>().position.x +
                            Mathf.Abs(handler.rightWall.GetComponent<Transform>().position.x - transform.position.x),
                             transform.position.y, transform.position.z), Quaternion.identity);

            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.rightWall.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 1;
        }
    }

    void SpawnTopImage()
    {
        Debug.Log("SpawnTop");
        if (wallIndex != 2)
        {
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                        new Vector3(transform.position.x, transform.position.y,
                    handler.topWall.GetComponent<Transform>().position.z +
                    Mathf.Abs(handler.topWall.GetComponent<Transform>().position.z - transform.position.z)),
                    Quaternion.identity);

            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.topWall.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 2;
        }
    }

    void SpawnBottomImage()
    {
        Debug.Log("SpawnBottom");
        if (wallIndex != 3)
        {
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                        new Vector3(transform.position.x, transform.position.y,
                    handler.bottomWall.GetComponent<Transform>().position.z -
                    Mathf.Abs(handler.bottomWall.GetComponent<Transform>().position.z - transform.position.z)),
                    Quaternion.identity);

            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.bottomWall.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 3;
        }
    }

    void SpawnFloorImage()
    {
        Debug.Log("SpawnFloor");
        if (wallIndex != 4)
        {
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                new Vector3(transform.position.x,
                    handler.floor.GetComponent<Transform>().position.y -
                    Mathf.Abs(handler.floor.GetComponent<Transform>().position.y - transform.position.y),
                    transform.position.z), Quaternion.identity);

            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.floor.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 4;
        }
    }

    void SpawnCeilingImage()
    {
        Debug.Log("SpawnCeiling");
        if (wallIndex != 5)
        {
            GameObject newSource = Instantiate(handler.ImgSourcePrefabs[order],
                new Vector3(transform.position.x,
                    handler.ceiling.GetComponent<Transform>().position.y +
                    Mathf.Abs(handler.ceiling.GetComponent<Transform>().position.y - transform.position.y),
                    transform.position.z), Quaternion.identity);

            newSource.GetComponent<ImageSourceSpawner>().order = order + 1;
            newSource.GetComponent<ImageSourceSpawner>().totalReflectivity =
                   totalReflectivity * handler.ceiling.GetComponent<Reflector>().absorptionCoeff;
            newSource.GetComponent<ImageSourceSpawner>().wallIndex = 5;
        }
    }
}
