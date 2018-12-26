using UnityEngine;

class Pool : Object
{   
    #region public member

    public GameObject GameObjectType;       // variable to store the prefab type of the pool objects
    public int PoolSize;                    // the size of the pool
    public PoolObject[] PoolObjects;        // the array of pool objects
    public Vector2 StandardPoolPos;         // the standard span pos of the pool objects

    #endregion

    #region Constructor

    public Pool(GameObject gameObjectType, int poolSize, Vector2 standardPoolPos)
    {
        GameObjectType = gameObjectType;
        PoolSize = poolSize;
        StandardPoolPos = standardPoolPos;

        // init the pool objects
        PoolObjects = new PoolObject[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            PoolObjects[i] = new PoolObject(GameObjectType, StandardPoolPos);
        }
    }

    #endregion
}