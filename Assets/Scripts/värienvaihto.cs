using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class värienvaihto : MonoBehaviour
{
    public Material Material1;
    void OnApplicationQuit()
    {
        GetComponent<MeshRenderer>().material = Material1;
    }
}
