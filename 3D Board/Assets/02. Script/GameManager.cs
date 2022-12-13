using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{

    enum State
    {

    }

    static GameManager instance;

    [SerializeField] Node startNode;
    [SerializeField] Dice[] dices;
    public List<PlayerManager> playerManagerList;

    Queue<PlayerManager> playerQueue;
    PlayerManager currentPlayer;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;

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
        playerQueue = new Queue<PlayerManager>();

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Dice", Vector3.zero, Quaternion.identity);

        GameObject[] temps = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in temps)
        {
            PlayerManager temp = item.GetComponent<PlayerManager>();
            if (temp.photonView.IsMine)
            {
                temp.node = GameObject.FindGameObjectWithTag("StartNode").GetComponent<Node>().nodeList[temp.Num - 1];
                temp.transform.position = temp.node.transform.position + new Vector3(0,0.1f,0);
                temp.Move(6);
            }
            playerQueue.Enqueue(temp);
            playerManagerList.Add(temp);
        }


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
