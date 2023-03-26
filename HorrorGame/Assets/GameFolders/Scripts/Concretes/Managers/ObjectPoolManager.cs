using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectId
{
    MuzzleFlashFx,
    Bullet,
    BulletCasin,
    WoodBulletHoleFx,
}

namespace Managers
{
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
        private void Update()
        {
            //foreach (Pool pool in _objectPools)
            //{
            //    Debug.Log(pool.ObjectId.ToString() + " " + pool.PooledObjects.Count);

            //}
        }
        void InitializeObjectPool()
        {
            for (int i = 0; i < _objectPools.Length; i++)
            {
                _objectPools[i].PooledObjects = new Queue<GameObject>();

                for (int j = 0; j < _objectPools[i].PoolSize; j++)
                {
                    GameObject newObj = Instantiate(_objectPools[i].Prefab);
                    newObj.SetActive(false);
                    newObj.transform.SetParent(transform);
                    _objectPools[i].PooledObjects.Enqueue(newObj);
                }
            }
        }
        public GameObject GetObjectFromPool(Transform newTransform, PoolObjectId poolId)
        {

            foreach (Pool pool in _objectPools)
            {
                if (pool.ObjectId == poolId)
                {
                    if (pool.PooledObjects.Count == 0)
                    {
                        Debug.Log("Increased " + pool.ObjectId.ToString());
                        IncreasePoolSize(pool, 3);
                    }
                    GameObject gameObj = pool.PooledObjects.Dequeue();
                    gameObj.transform.position = newTransform.position;
                    gameObj.transform.rotation = newTransform.rotation;
                    gameObj.SetActive(true);
                    return gameObj;
                }
            }
            return null;
        }
        public GameObject GetObjectFromPool( PoolObjectId poolId)
        {

            foreach (Pool pool in _objectPools)
            {
                if (pool.ObjectId == poolId)
                {
                    if (pool.PooledObjects.Count == 0)
                    {
                        Debug.Log("Increased " + pool.ObjectId.ToString());
                        IncreasePoolSize(pool, 3);
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

                    pool.PooledObjects.Enqueue(objToSet);
                    objToSet.SetActive(false);
                    objToSet.transform.SetParent(transform);

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

}
