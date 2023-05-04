using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koodinumero : MonoBehaviour
{
    public string koodi;
    private void Update()
    {
        if (koodi.Length == 3)
        {
            if (koodi != "743")
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Outline>().enabled = false;
                }
                koodi = string.Empty;
            }
            else
            {
                Debug.Log("open");
            }
        }
    }
}
