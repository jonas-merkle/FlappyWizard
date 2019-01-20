using UnityEngine;

public class GameObjectPoolHandler : MonoBehaviour
{
    #region public members

    public GameObject GameObjectPrefab;
    //public float SpawnDistance = 0.05f;
    //public float SpawnScattering = 0.0f;
    //public float SpawnProbability = 0.1f;
    public int PoolSize = 10;
    public Vector2 PoolPos = new Vector2(20, 0);
    //public Vector2 SpawnPos = new Vector2(20, 0);
    //public Vector2 SpawnCollisionBoxSize = new Vector2(2, 5);
    //public Vector2 SpawnCollisionBoxOffset = new Vector2(0, 0);
    public float MaxY = 5.0f;
    public float MinY = -5.0f;
    public float OnTurboY = -2.0f;
    //public float StartOfScreenX = +10.0f;
    public float EndOfScreenX = -10.0f;

    #endregion

    #region private members 

    private Pool _objectPool;
    //private float _distanceOfNextObject;
    //private float _totalMovedDistance;
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
        //MovedDistanceTrigger.Instance.UnitMovementDetected += MovementTriggered;
        _objectPool = new Pool(GameObjectPrefab, PoolSize, PoolPos);
        //_distanceOfNextObject = SpawnDistance + Random.Range(-SpawnScattering, +SpawnScattering);
        //_totalMovedDistance = 0;
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

        // get the current speed form game controller
        float currentVelocity = GameControl.Instance.CurrentGameSpeed;

        // scrolling on screen objects
        foreach (var obj in _objectPool.PoolObjects)
        {
            // check if an object is on screen and has to been moved
            if (obj.IsOnScreen)
            {
                // set new object speed
                obj.GameObjectBody.velocity = new Vector2(currentVelocity, 0);

                // check if turbo mode is active
                if (GameControl.Instance.Turbo)
                {
                    obj.GameObject.transform.position = new Vector2(obj.GameObject.transform.position.x, OnTurboY);
                }

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

    public GameObject SpawnNextObject(Vector2 spawnPos)
    {
        GameObject currentGameObject = _objectPool.PoolObjects[_nextFormPool].GameObject;

        // spawn object
        currentGameObject.transform.position = spawnPos;

        // set on screen flag
        _objectPool.PoolObjects[_nextFormPool].IsOnScreen = true;

        // select the next object from pool
        _nextFormPool++;
        if (_nextFormPool >= PoolSize)
            _nextFormPool = 0;

        return currentGameObject;
    }

    /*private void SpawnObject()
    {
        // randomly span a object
        if (Random.Range(0.0f, 1.0f) <= SpawnProbability)
        {
            Vector2 initialPos;
            GameObject currentGameObject = _objectPool.PoolObjects[_nextFormPool].GameObject;

            // check if Turbo effect is activated
            if (GameControl.Instance.Turbo)
            {
                // set object to a fixed position
                initialPos = SpawnPos + new Vector2(0, OnTurboY);
            }
            else
            {
                // generate a random span height
                initialPos = SpawnPos + new Vector2(0, Random.Range(MinY, MaxY));
            }

            // collision detection on spawn
            var collisions = Physics2D.OverlapBox(SpawnCollisionBoxOffset + initialPos, SpawnCollisionBoxSize, 0.0f);
            if (collisions != null)
            {
                // collision occured

                // spawn object to default pos
                currentGameObject.transform.position = PoolPos;

                // set on screen flag
                _objectPool.PoolObjects[_nextFormPool].IsOnScreen = false;
            }
            else
            {
                // no collision occured

                // spawn object
                currentGameObject.transform.position = initialPos;

                // set on screen flag
                _objectPool.PoolObjects[_nextFormPool].IsOnScreen = true;
            }

            // select the next object from pool
            _nextFormPool++;
            if (_nextFormPool >= PoolSize)
                _nextFormPool = 0;
        }
    }

    void MovementTriggered(object sender, MovedDistanceTriggerEventArgs e)
    {
        _totalMovedDistance += e.MovedDistance;

        // check if distance for a new object spawn has reached
        if (_totalMovedDistance >= _distanceOfNextObject)
        {
            _totalMovedDistance = 0;
            _distanceOfNextObject = SpawnDistance + Random.Range(-SpawnScattering, +SpawnScattering);   // set new spawn distance

            SpawnObject();
        }
    }*/
}