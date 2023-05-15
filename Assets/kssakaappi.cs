using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kssakaappi : MonoBehaviour
{
    public void addnumber()
    {
        transform.parent.GetComponent<koodinumero>().koodi += gameObject.name;
    }
}
