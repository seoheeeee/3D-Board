using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool Instance = null;

    //[SerializeField]
    public GameObject poolingObj;

    Queue<GameObject> objPoolingQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    void Start()
    {
        Initialize(20);
    }

    void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            objPoolingQueue.Enqueue(CreatNewObject());
        }
    }

    GameObject CreatNewObject()
    {
        var temp = Instantiate(poolingObj);
        temp.SetActive(false);
        temp.transform.SetParent(transform);

        return temp;
    }

    public GameObject GetObject()
    {
        if (objPoolingQueue.Count > 0)
        {
            var obj = objPoolingQueue.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.localPosition = Vector3.zero;
            return obj;
        }

        else
        {
            var temp = CreatNewObject();
            temp.gameObject.SetActive(true);
            temp.transform.localPosition = Vector3.zero;
            return temp;
        }
    }

    public static void ReturnObj(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.objPoolingQueue.Enqueue(obj);
    }
}
