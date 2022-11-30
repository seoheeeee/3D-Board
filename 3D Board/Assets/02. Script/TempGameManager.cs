using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{

    public List<PlayerManager> playerManagerList;
    GameObject[] temps;
    void Start()
    {

        //playerManagerList = new List<PlayerManager>();
       
        temps = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in temps)
            playerManagerList.Add(item.GetComponent<PlayerManager>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
