using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    string name;
    public float absorptionCoeff;

    private void Start()
    {
        name = gameObject.name;
    }
}
