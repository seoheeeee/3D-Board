using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    void Start()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }    
}
