using System.Collections.Generic;
using UnityEngine;



public class BallGameManager : MonoBehaviour
{
    class ObjPoolingBall
    {
        public GameObject ball;
        public Vector3 startPos;
        public Vector3 endPos;

        public ObjPoolingBall(GameObject ball, Vector3 startPos, Vector3 endPos)
        {
            this.ball = ball;
            this.startPos = startPos;
            this.endPos = endPos;
        }
    }

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
    List<PlayerManager> playerList;

    [SerializeField]
    List<Transform> respawnPosList;

    List<ObjPoolingBall> ballList;

    List<int> positionIndex; 

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

        timer = timerReset = 1;

        ballList = new List<ObjPoolingBall>();
        positionIndex = new List<int>();
    }

    void Update()
    {

        switch (phase)
        {
            case Phase.Ready:

                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    count = Random.Range(10, 21);
                    

                    for (int i = 0; i < count; i++)
                    {
                        int randomPosIndex =0;
                        while (true)
                        {
                            randomPosIndex = Random.Range(0, respawnPosList.Count);
                            if (!positionIndex.Contains(randomPosIndex))
                            {
                                positionIndex.Add(randomPosIndex);
                                break;
                            }
                           
                        }
                         
                        //respawnPosList[randomPosIndex]
                        ballList.Add(new ObjPoolingBall(ObjPool.Instance.GetObject(respawnPosList[randomPosIndex]),
                                                                                   respawnPosList[randomPosIndex].position,
                                                                                   respawnPosList[randomPosIndex].position + new Vector3(0, Random.Range(5, 50),0)));
                        

                    }
                    phase = Phase.Start;
                }

                break;
            case Phase.Start:

                foreach (ObjPoolingBall item in ballList)
                {
                    item.ball.transform.position = Vector3.Lerp(item.startPos, item.endPos, speed);
                    
                }
                speed += Time.deltaTime; 
                

                break;
            case Phase.End:

                timer = timerReset;
                round++;

                break;
        }
    }
}
