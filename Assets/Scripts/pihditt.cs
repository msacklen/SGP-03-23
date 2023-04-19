using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pihditt : MonoBehaviour
{
    public Transform pihdit;
    public Transform paikkaavaimelle;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, paikkaavaimelle.position) < 0.2f && isActivated == true)
        {
            transform.parent = pihdit;
            transform.position = paikkaavaimelle.position;
        }
    }

    public void activated()
    {
        isActivated = true;
    }

    public void deactivate()
    {
        isActivated = false;
    }
}
