using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempGameManager : MonoBehaviourPun
{
    [SerializeField] Node startNode;
    public List<PlayerManager> playerManagerList;
    void Start()
    {

        //playerManagerList = new List<PlayerManager>();

        GameObject[] temps = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in temps)
            playerManagerList.Add(item.GetComponent<PlayerManager>());



        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < playerManagerList.Count; i++)
            {
                playerManagerList[i].TeleportPlayer(startNode.nextNode[playerManagerList[i].Num].transform.position);
            }
        }



    }



}
