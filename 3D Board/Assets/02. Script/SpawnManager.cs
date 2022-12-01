using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject objRange;
    public GameObject ball;

    BoxCollider rangeCollider;

    private void Awake()
    {
        rangeCollider = objRange.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPos = objRange.transform.position;

        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPos = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPos = originPos = RandomPos;
        return respawnPos;
    }

    
    void Start()
    {
        
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        yield return new WaitForSeconds(4f);
        GameObject instantCapsul = Instantiate(ball, Return_RandomPosition(), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
