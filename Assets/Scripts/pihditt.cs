using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pihditt : MonoBehaviour
{
    public Transform paikkaavaimelle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, paikkaavaimelle.position) < 0.2f)
        {
            transform.position = paikkaavaimelle.position;
        }
    }
}
