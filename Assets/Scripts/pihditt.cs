using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class pihditt : MonoBehaviour
{
    public Transform pihdit;
    public Transform paikkaavaimelle;
    private bool isActivated = false;
    private bool emt = false;
    public Transform kansi;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, paikkaavaimelle.position) < 0.1f && isActivated == true)
        {
            emt = true;
            transform.parent = pihdit;
            transform.position = paikkaavaimelle.position;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }

        if (emt != false)
        {
            GetComponent<XRGrabInteractable>().enabled = true;
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
