using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{

    static GameManager instance;

    [SerializeField] Node startNode;
    public List<PlayerManager> playerManagerList;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

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

    void DiceRoll()
    {

    }


}
