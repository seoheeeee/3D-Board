using System.Collections.Generic;
using UnityEngine;
using TMPro;



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
    int count;
    float timer, timerReset;
    int round = 1;

    [SerializeField]
    VariableJoystick joystick;

    [SerializeField]
    TMP_Text numText;

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
                    //count = Random.Range(10, 21);
                    count = 3;

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
                        ballList.Add(new ObjPoolingBall(ObjPool.Instance.GetObject(respawnPosList[positionIndex[i]]),
                                                                                   respawnPosList[positionIndex[i]].position,
                                                                                   respawnPosList[positionIndex[i]].position + new Vector3(0, Random.Range(5, 30),0)));
                    }
                    Debug.Log("123");
                    phase = Phase.Start;
                }

                break;
            case Phase.Start:

                foreach (ObjPoolingBall item in ballList)
                {

                    item.ball.transform.position = Vector3.Lerp(item.startPos, item.endPos, speed);
                    
                }
                speed += Time.deltaTime;
                if (speed > 1)
                {
                    phase = Phase.End;
                    speed = 0;
                }

                break;

            case Phase.End:

                foreach (ObjPoolingBall item in ballList)
                {
                    item.ball.transform.position = Vector3.Lerp(item.endPos, item.startPos, speed);
                }
                speed += Time.deltaTime;



                timer = timerReset;
                round++;

                break;
        }
    }

    void UpButtonClick()
    {
        
    }

}
