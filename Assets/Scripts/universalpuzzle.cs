using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class universalpuzzle : NetworkBehaviour
{
    public int total;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Käännynytsaatana()
    {
        if (total == 12 && IsHost) Debug.Log("vaihda scene");
    }
}
