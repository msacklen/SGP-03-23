using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

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
        if (total == 12 && IsHost) StartCoroutine("sceneLoader");
    }

    IEnumerator sceneLoader()
    {
        yield return new WaitForSeconds(5);
        NetworkManager.SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
    }
}
