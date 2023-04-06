using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class puzzleLogic : MonoBehaviour
{
    [SerializeField] private Vector3 lockedPlace;
    [SerializeField] private Vector3 lockedRotation = new Vector3(-90, 0, 0);
    // Start is called before the first frame update
    private int total;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lockedPlace, transform.localPosition) < 0.33f)
        {
            transform.localPosition = lockedPlace;
            transform.eulerAngles = lockedRotation;
            GetComponent<Rigidbody>().isKinematic = true;

            total += 1;
            this.gameObject.GetComponent<puzzleLogic>().enabled = false;
            GetComponent<XRGrabInteractable>().enabled = false;
            Debug.Log(total);
        }
    }
}
