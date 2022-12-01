using System.Collections.Generic;
using UnityEngine;

public class BallGameManager : MonoBehaviour
{

    enum Phase
    {
        Ready,
        Start,
        End
    }

    enum State
    {
        A,
        B,
        C
    }

    [Range(0, 1)]
    public float speed;

    [SerializeField]
    Transform[] pos2;
    [SerializeField]
    GameObject player;


    [SerializeField]
    Transform[] pos4;
    [SerializeField]
    GameObject player2;

    [SerializeField]
    List<PlayerManager> playerList;

    [SerializeField]
    List<Transform> respawnPosList;

    [SerializeField]
    Phase phase;

    [SerializeField]
    State state;

    int count; 

    float timer, timerReset;

    int round = 1;

    void Start()
    {
        GameObject[] temps = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in temps)
        {
            playerList.Add(player.GetComponent<PlayerManager>());
        }

        timer = timerReset = 10;
    }

    void Update()
    {

        player.transform.position = Vector3.Lerp(pos2[0].transform.position, pos2[1].transform.position, speed);
        player2.transform.position = Vector3.Lerp(pos4[0].transform.position, pos4[1].transform.position, speed);

        switch (phase)
        {
            case Phase.Ready:

                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    count = Random.Range(10, 21);
                    phase = Phase.Start;

                    for (int i = 0; i < count; i++)
                    {
                        int randomPosIndex = Random.Range(0, respawnPosList.Count);
                        //respawnPosList[randomPosIndex]
                        ObjPool.Instance.GetObject(respawnPosList[randomPosIndex]);                   }
                }
                break;
            case Phase.Start:

                

                break;
            case Phase.End:

                timer = timerReset;
                round++;

                break;
        }
    }
}
