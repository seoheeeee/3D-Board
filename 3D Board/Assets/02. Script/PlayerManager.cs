using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [SerializeField]
    TMP_Text numTxt;
    [SerializeField]
    int num;

    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float offset;
    Vector3 heightPos;
    Vector3 offsetPos;

    [Range(0,1)]
    [SerializeField]
    float speed;
    [SerializeField]
    float delay;

    public bool isMove;
    public Node node;

    public int Num 
    { 
        get => num;
        set 
        {
            num = value;
            numTxt.text = value.ToString();
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        jumpHeight = 1.5f;
        offset = 0.75f;
        delay = 0.2f;
        heightPos = new Vector3(0, jumpHeight, 0);
        offsetPos = new Vector3(0, offset, 0);
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (photonView.IsMine)
            photonView.RPC("SetPlayer", RpcTarget.AllBuffered, PhotonNetwork.PlayerList.Length);
    }
    [PunRPC]
    void SetPlayer(int num)
    {
        Num = num;
    }
    public void Move(int diceValue, float speed = 1)
    {
        StartCoroutine(MovePlayer(diceValue, speed));
    }
    // 지역변수 speed = 1 이렇게 선언해주면 이 함수를 다른 곳에서 쓸 때 speed 값을 쓰지 않으면 자동으로 1로 할당된다.
    IEnumerator MovePlayer(int diceValue ,float speed = 1)
    {
        for (int i = 0; i < diceValue; i++)
        {
            // speed 변수에 담기는 값이 1이 넘으면 Lerp가 끝났다는 증거이기 때문에 말그대로 delay를 줘서 잠시 다음 이동하기 전 유예시간을 주는 코드
            while (1 + delay > this.speed)
            {
                //같은 이름의 변수는 this를 붙이면 전역변수 붙이지 않는다면 지역변수를 가르킨다.
                this.speed += Time.deltaTime * speed;

                //offset은 startPos와 endPos Y축 보정을 해주는 값이다.
                transform.position = Bezier(node.transform.position + offsetPos,
            node.nextNode[0].transform.position + offsetPos, this.speed);

                //코루틴을 통한 while문을 돌리기 위해선 while문 안에 yield return null 을 넣어주지 않는다면 정상적으로 작동하지 않는다.
                yield return null;
            }
            node = node.nextNode[0];
            this.speed = 0;
        }
    }
    Vector3 Bezier(Vector3 start, Vector3 end, float value)
    {
        Vector3 startH = start + heightPos;
        Vector3 endH = end + heightPos;

        Vector3 A = Vector3.Lerp(start, startH, value);
        Vector3 B = Vector3.Lerp(startH, endH, value);
        Vector3 C = Vector3.Lerp(endH, end, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);
        return F;
    }

    public void Teleport(Vector3 targetPos)
    {
        transform.position = targetPos;

        //photonView.RPC("TeleportRPC", RpcTarget.AllBuffered, targetPos);
    }
    //[PunRPC]
    //void TeleportRPC(Vector3 targetPos)
    //{
    //}

}
