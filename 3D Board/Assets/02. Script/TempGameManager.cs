using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{

    public List<PlayerManager> playerManagerList;
    void Start()
    {
        playerManagerList = new List<PlayerManager>();
       
        GameObject[] temps = GameObject.FindGameObjectsWithTag("Manager");

        foreach (GameObject item in temps)
            playerManagerList.Add(item.GetComponent<PlayerManager>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
