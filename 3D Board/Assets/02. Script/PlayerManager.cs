using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;


public class PlayerManager : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [SerializeField]
    TMP_Text numTxt;
    [SerializeField]
    int num;

    public int Num { 
        
        get => num;

        set 
        {
            num = value;
            numTxt.text = value.ToString();
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "IsAdmin", "Admin" } });

        if(photonView.IsMine)
            photonView.RPC("SetPlayer", RpcTarget.AllBuffered, PhotonNetwork.PlayerList.Length);
    }

    [PunRPC]
    void SetPlayer(int num)
    {
        Num = num;
    }

}
