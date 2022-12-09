using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{

    enum Status
    {

    }

    static GameManager instance;

    [SerializeField] Node startNode;
    [SerializeField] Dice[] dices;
    public List<PlayerManager> playerManagerList;

    int currentPlayer;

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
                playerManagerList[i].Teleport(startNode.nextNode[playerManagerList[i].Num].transform.position);
            }
        }

        for (int i = 0; i < playerManagerList.Count; i++)
            playerManagerList[i].node = GameObject.FindGameObjectWithTag("StartNode").GetComponent<Node>().nextNode[playerManagerList[i].Num - 1];


    }


    private void Update()
    {
        
    }

    public void DiceRoll(bool isRoll)
    {
        if (isRoll)
        {
            foreach (Dice item in dices)
                item.isRoll = true;
        }
        else
        {
            foreach (Dice item in dices)
                item.isEnd = true;
        }
    }




}
