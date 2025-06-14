using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public GameObject objectToGetPositionAndRotation;
    public int amountToPool;
    [SerializeField] float timer;
    [SerializeField] float timeSpeed;
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeSpeed)
        {
            GameObject gameObjectRuning = GetPooledObject();
            gameObjectRuning.transform.position = objectToGetPositionAndRotation.transform.position;
            gameObjectRuning.SetActive(true);
            timer = 0;
        }
       
        //Vector3 vector = gameObjectRuning.transform.position;
        //vector.x -= 0.01f;
        //gameObjectRuning.transform.position = vector;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
              {
                return pooledObjects[i];
               }
        }
        return null;
    }

}