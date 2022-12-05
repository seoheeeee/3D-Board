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
    float height = 1.5f;
    [Range(0,1)]
    [SerializeField]
    float speed;
    [SerializeField]
    float delay = 0.1f;

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
    private void Update()
    {
        if(node != null)
        transform.position = Bezier(node.transform.position, node.nextNode[0].transform.position, speed);
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "IsAdmin", "Admin" } });

        if (photonView.IsMine)
            photonView.RPC("SetPlayer", RpcTarget.AllBuffered, PhotonNetwork.PlayerList.Length);
    }
    [PunRPC]
    void SetPlayer(int num)
    {
        Num = num;
    }
    public void Move()
    {

    }
    Vector3 Bezier(Vector3 start, Vector3 end, float value)
    {
        Vector3 startH = start + new Vector3(0, height, 0);
        Vector3 endH = end + new Vector3(0, height, 0);

        Vector3 A = Vector3.Lerp(start, startH, value);
        Vector3 B = Vector3.Lerp(startH, endH, value);
        Vector3 C = Vector3.Lerp(endH, end, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);
        return F;
    }

    IEnumerator MovePlayerCorutain(Vector3 startPos, Vector3 endPos)
    {
        speed = 0;
        while (true)
        {
            isMove = true;
            speed += Time.deltaTime;
            transform.position = Bezier(startPos, endPos, speed);

            if (speed >= 1.0f + delay)
            {
                isMove = false;
                break;
            }
            yield return null;
        }
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
