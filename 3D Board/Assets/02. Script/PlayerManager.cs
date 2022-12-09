using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [SerializeField]
    TMP_Text numTxt;
    [SerializeField]
    int num;
    public int step;

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
    public void Move(int diceValue, Direction direction , float speed = 1)
    {
        StartCoroutine(MovePlayer(diceValue, direction, speed));
    }
    // �������� speed = 1 �̷��� �������ָ� �� �Լ��� �ٸ� ������ �� �� speed ���� ���� ������ �ڵ����� 1�� �Ҵ�ȴ�.
    IEnumerator MovePlayer(int diceValue , Direction direction ,float speed = 1)
    {
        for (int i = 0; i < diceValue; i++)
        {
            if (node.direction != Direction.Straight)
            {
                step -= i;
                break;
            }
            // speed ������ ���� ���� 1�� ������ Lerp�� �����ٴ� �����̱� ������ ���״�� delay�� �༭ ��� ���� �̵��ϱ� �� �����ð��� �ִ� �ڵ�
            while (1 + delay > this.speed)
            {
                //���� �̸��� ������ this�� ���̸� �������� ������ �ʴ´ٸ� ���������� ����Ų��.
                this.speed += Time.deltaTime * speed;

                //offset�� startPos�� endPos Y�� ������ ���ִ� ���̴�.
                transform.position = Bezier(node.transform.position + offsetPos,
            node.nextNode[0].transform.position + offsetPos, this.speed);

                //�ڷ�ƾ�� ���� while���� ������ ���ؼ� while�� �ȿ� yield return null �� �־����� �ʴ´ٸ� ���������� �۵����� �ʴ´�.
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
    }
    //[PunRPC]
    //void TeleportRPC(Vector3 targetPos)
    //{
    //}

}
