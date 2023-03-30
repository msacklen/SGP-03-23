using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        NetworkManager.OnServerStarted += LoadLobby;
    }

    void LoadLobby()
    {
        NetworkManager.SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    void LoadLevel()
    {
        NetworkManager.SceneManager.LoadScene("Apartment", LoadSceneMode.Single);
    }
}
