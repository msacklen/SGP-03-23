using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class spawnlappu : NetworkBehaviour
{
    private bool isSpawned = false;
    [SerializeField] GameObject vihjelappu;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SpawnLappu()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            GameObject _lappu = Instantiate(vihjelappu, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), transform.rotation);
            NetworkObject _lappuNO = _lappu.GetComponent<NetworkObject>();
            _lappuNO.Spawn();

            Destroy(GetComponent<FixedJoint>());
        }
    }
}
