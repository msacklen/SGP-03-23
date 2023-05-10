using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class universalpuzzle : NetworkBehaviour
{
    [SerializeField] GameObject rotatedpuzzle;
    GameObject[] puzzlepieces;

    public int total;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (total == 12 && IsHost)
        {
            GameObject _lappu = Instantiate(rotatedpuzzle, new Vector3(1.85f, 1.012f, -1.092f), Quaternion.Euler(180, 0, 0));
            NetworkObject _lappuNO = _lappu.GetComponent<NetworkObject>();
            _lappuNO.Spawn();

            puzzlepieces = GameObject.FindGameObjectsWithTag("Piece");

            foreach (GameObject piece in puzzlepieces)
            {
                NetworkManager.Destroy(piece);
            }
            NetworkManager.Destroy(GameObject.Find("highlight(Clone)"));
            NetworkManager.Destroy(gameObject);
        }
    }
}
