using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class avainkakkone : NetworkBehaviour
{

    [SerializeField] GameObject brailleBox;
    [SerializeField] GameObject brailletext;
    [SerializeField] GameObject braille;

    [SerializeField] GameObject brailleAvain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == brailleAvain)
        {
            if (IsHost)
            {
                GameObject _braille = Instantiate(braille, braille.transform.position, braille.transform.rotation);
                NetworkObject _brailleNO = _braille.GetComponent<NetworkObject>();
                if (IsHost) _brailleNO.Spawn();

                GameObject _brailletext = Instantiate(brailletext, brailletext.transform.position, brailletext.transform.rotation);
                NetworkObject _brailletextNO = _brailletext.GetComponent<NetworkObject>();
                if (IsHost) _brailletextNO.Spawn();
            }

            NetworkManager.Destroy(brailleBox);

            Debug.Log("boxi aukee");
        }
    }
}
