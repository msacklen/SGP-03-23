using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TMP_Text ipText;
    [SerializeField] NetworkManager netManager;
    [SerializeField] UnityTransport utp;
    [SerializeField] GameObject xrRig;
    string ip = "172.24.18.67";

    public void Update()
    {
        ipText.SetText(ip);
    }

    public void Host()
    {
        netManager.StartHost();
        if(xrRig != null) Destroy(xrRig);
    }

    public void Join()
    {
        utp.SetConnectionData(ip, 7777);
        if(xrRig != null) Destroy(xrRig);
        netManager.StartClient();
    }

    #region Keyboard inputs
    public void KB_dot()
    {
        ip += ".";
    }
    public void KB_back()
    {
        ip = ip.Remove(ip.Length-1,1);
    }
    public void KB_0()
    {
        ip += "0";
    }
    public void KB_1()
    {
        ip += "1";
    }
    public void KB_2()
    {
        ip += "2";
    }
    public void KB_3()
    {
        ip += "3";
    }
    public void KB_4()
    {
        ip += "4";
    }
    public void KB_5()
    {
        ip += "5";
    }
    public void KB_6()
    {
        ip += "6";
    }
    public void KB_7()
    {
        ip += "7";
    }
    public void KB_8()
    {
        ip += "8";
    }
    public void KB_9()
    {
        ip += "9";
    }
    #endregion
}
