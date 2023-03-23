using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectId
{
    MuzzleFlashFx
}

public class ObjectPoolManager : SingletonMonoObject<ObjectPoolManager> 
{

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> PooledObjects;
        public GameObject Prefab;
        public int PoolSize;
        public PoolObjectId ObjectId;
    }
    [SerializeField] private Pool[] _objectPools;
    private void Awake()
    {
        SingletonThisObject(this);
    }

    private void Start()
    {
        InitializeObjectPool();
    }
    void InitializeObjectPool()
    {
        for (int i = 0; i < _objectPools.Length; i++)
        {
            _objectPools[i].PooledObjects = new Queue<GameObject>();

            for (int j = 0; j < _objectPools[i].PoolSize ; j++)
            {
                GameObject newObj = Instantiate(_objectPools[i].Prefab);
                newObj.SetActive(false);
                newObj.transform.SetParent(transform);
                _objectPools[i].PooledObjects.Enqueue(newObj);
            }
        }
    }
    public GameObject GetObjectFromPool(PoolObjectId poolId)
    {
        Debug.Log("helo");
        foreach (Pool pool in _objectPools)
        {
            if(pool.ObjectId == poolId)
            {
                if(pool.PooledObjects.Count<1)
                {
                    IncreasePoolSize(pool,3);
                }
                GameObject gameObj = pool.PooledObjects.Dequeue();
                return gameObj;
            }
        }
        return null; 
    }
    public void SetPool(GameObject objToSet, PoolObjectId poolId)
    {
        foreach (Pool pool in _objectPools)
        {
            if (pool.ObjectId == poolId)
            {
                objToSet.SetActive(false);
                objToSet.transform.SetParent(transform);
                pool.PooledObjects.Enqueue(objToSet);   

            }
        }
    }
    private void IncreasePoolSize(Pool pool, int increment)
    {
        for (int j = 0; j < increment; j++)
        {
            GameObject newObj = Instantiate(pool.Prefab);
            newObj.SetActive(false);
            newObj.transform.SetParent(transform);
            pool.PooledObjects.Enqueue(newObj);
        }
    }
}
