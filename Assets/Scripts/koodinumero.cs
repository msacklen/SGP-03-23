using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class koodinumero : NetworkBehaviour
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
                if (IsHost) NetworkManager.SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
            }
        }
    }
}
