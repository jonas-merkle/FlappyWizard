using UnityEngine;

public class GameObjectPoolHandler : MonoBehaviour
{
    #region public members

    public GameObject GameObjectPrefab;
    public float SpanRate = 1;
    public float SpanProbability = 0.5f;
    public int PoolSize = 10;
    public Vector2 PoolPos =  new Vector2(20, 0);
    public Vector2 SpawnPos = new Vector2(20, 0);
    public float MaxY = 5.0f;
    public float MinY = -5.0f;
    public float EndOfScreenX = -10.0f;

    #endregion

    #region private members 

    private Pool _objectPool;
    private float _lastSpawnTime;
    private Random _random;
    private int _nextFormPool;

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _objectPool = new Pool(GameObjectPrefab, PoolSize, PoolPos);
        _lastSpawnTime = 0;
        _nextFormPool = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if game is not running, do nothing
        if (GameControl.Instance.GameOver && GameControl.Instance.GamePaused)
        {
            // set scrolling speed on all active objects to 0
            foreach (var obj in _objectPool.PoolObjects)
            {
                // check if an object is on screen and has to been moved
                if (obj.IsOnScreen)
                {
                    // set new object speed
                    obj.GameObjectBody.velocity = Vector2.zero;
                }
            }

            return;
        }

        // check if time fo a new object spawn has reached
        if (Time.time > _lastSpawnTime + SpanRate)
        {
            _lastSpawnTime = Time.time;     // reset last spawn time

            // randomly span a object
            if (Random.Range(0.0f, 1.0f) <= SpanProbability)
            {
                // generate a random span height
                _objectPool.PoolObjects[_nextFormPool].GameObject.transform.position = SpawnPos + new Vector2(0, Random.Range(MinY, MaxY)); ;
                _objectPool.PoolObjects[_nextFormPool].IsOnScreen = true;

                // select the next object from pool
                _nextFormPool++;
                if (_nextFormPool >= PoolSize)
                    _nextFormPool = 0;
            }
        }

        // get the current speed form game controller
        float currentSpeed = GameControl.Instance.CurrentGameSpeed;

        // scrolling on screen objects
        foreach (var obj in _objectPool.PoolObjects)
        {
            // check if an object is on screen and has to been moved
            if (obj.IsOnScreen)
            {
                // set new object speed
                obj.GameObjectBody.velocity = new Vector2(currentSpeed, 0);

                // check if an object is on scree
                if (obj.GameObject.transform.position.x < EndOfScreenX)
                {
                    obj.GameObjectBody.velocity = Vector2.zero;
                    obj.GameObject.transform.position = _objectPool.StandardPoolPos;
                    obj.IsOnScreen = false;
                }
            }
        }
    }

    #endregion
}
