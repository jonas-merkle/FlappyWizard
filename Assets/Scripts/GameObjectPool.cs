using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    #region public members

    public GameObject GameObjectPrefab;
    public double SpanRate;
    public static int PoolSize = 10;

    #endregion

    #region private members 

    private GameObject[] ObjectPool = new GameObject[PoolSize];
    private bool[] ObjectStatus = new bool[PoolSize];

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
