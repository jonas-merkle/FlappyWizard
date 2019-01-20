using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnHandler : MonoBehaviour
{
    // Static global instance of the class
    public static SpawnHandler Instance;

    #region public members

    // obstacles pools
    public GameObjectPoolHandler GhostPoolHandler;
    public GameObjectPoolHandler Tower1PoolHandler;
    public GameObjectPoolHandler Tower2PoolHandler;
    public GameObjectPoolHandler Tower3PoolHandler;
    public GameObjectPoolHandler Tower4PoolHandler;

    // item pools
    public GameObjectPoolHandler Item1PoolHandler;
    public GameObjectPoolHandler Item2PoolHandler;
    public GameObjectPoolHandler Item3PoolHandler;
    public GameObjectPoolHandler Item4PoolHandler;

    // spawn rates
    public float MinTowerDistance;
    public float MinItemDistance;
    public float TowerSpawnProbability;
    public float ItemSpawnProbability;
    public float GhostSpawnProbability;
    public float TowerSpawnScattering;
    public float ItemSpawnScattering;
    public float MaxHightOfTowerToSpawnGhost = -4;
    public float DistanceOfGhostToTower = 7.5f;

    // positions
    public Vector2 SpawnPos = new Vector2(15, 0);

    #endregion

    #region private members

    // distances
    public float _totalMovedDistance;
    private float _distanceOfNextTowerSpawn;
    private float _distanceOfNextItemSpawn;

    // flags
    public bool _mainSpawnAreaIsFree;

    // last spawned object
    public GameObject _lastSpawnedObject;

    #endregion

    #region basic unity functions

    // Awake is called before Start()
    void Awake()
    {
        // set the reference to the current instance
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // subscribe to the event stuff
        MovedDistanceTrigger.Instance.UnitMovementDetected += MovementTriggered;
        TriggerPointCollisionHandler.Instance.TriggerPointCollisionDetected += TriggerPointTriggered;

        // set the initial values
        _totalMovedDistance = 0;
        _distanceOfNextItemSpawn = 0;
        _distanceOfNextTowerSpawn = 0;
        _mainSpawnAreaIsFree = true;
        _lastSpawnedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        // set new random seed
        Random.InitState(DateTime.Now.Millisecond);

        // check if a new item could be spawned
        if (_mainSpawnAreaIsFree && _totalMovedDistance > _distanceOfNextItemSpawn)
        {
            _spawnItem();
        }

        // check if a new tower could be spawned
        if (_mainSpawnAreaIsFree && _totalMovedDistance > _distanceOfNextTowerSpawn)
        {
            _spawnTower();
        }
    }

    #endregion

    #region spawn functions

    private void _spawnItem()
    {
        // set distance at which the next item could spawn
        _distanceOfNextItemSpawn = _totalMovedDistance + MinItemDistance + Random.Range(-ItemSpawnScattering, ItemSpawnScattering); 

        // spawn probability 
        if (Random.Range(0, 1.0f) > ItemSpawnProbability)
        {
            // don't spawn an item
            return;
        }

        // select the type of item
        GameObjectPoolHandler currentPool;
        switch (Random.Range(1, 5))
        {
            case 1:
                currentPool = Item1PoolHandler;
                break;
            case 2:
                currentPool = Item2PoolHandler;
                break;
            case 3:
                currentPool = Item3PoolHandler;
                break;
            case 4:
                currentPool = Item4PoolHandler;
                break;
            default:
                Console.Out.Write("Not defined random item pool number");
                return;
        }

        // generate random y pos
        Vector2 spawnVect = SpawnPos + new Vector2(0, Random.Range(currentPool.MinY, currentPool.MaxY));

        // spawn object
        _lastSpawnedObject = currentPool.SpawnNextObject(spawnVect);

        // mark spawn area as occupied
        _mainSpawnAreaIsFree = false;
    }

    private void _spawnTower()
    {
        // set distance at which the next tower could spawn
        _distanceOfNextTowerSpawn = _totalMovedDistance + MinTowerDistance + Random.Range(-TowerSpawnScattering, TowerSpawnScattering);

        // spawn probability 
        if (Random.Range(0, 1.0f) > TowerSpawnProbability)
        {
            // don't spawn an tower
            return;
        }

        // select the type of tower
        GameObjectPoolHandler currentPool;
        switch (Random.Range(1, 5))
        {
            case 1:
                currentPool = Tower1PoolHandler;
                break;
            case 2:
                currentPool = Tower2PoolHandler;
                break;
            case 3:
                currentPool = Tower3PoolHandler;
                break;
            case 4:
                currentPool = Tower4PoolHandler;
                break;
            default:
                Console.Out.Write("Not defined random tower pool number");
                return;
        }

        // generate random y pos
        Vector2 spawnVect = SpawnPos + new Vector2(0, Random.Range(currentPool.MinY, currentPool.MaxY));

        // spawn a ghost
        if (spawnVect.y <= MaxHightOfTowerToSpawnGhost)
        {
            _spawnGhost(spawnVect.y);
        }

        // spawn object
        _lastSpawnedObject = currentPool.SpawnNextObject(spawnVect);

        // mark spawn area as occupied
        _mainSpawnAreaIsFree = false;
    }

    private void _spawnGhost(float heightOfTower)
    {
        // spawn probability 
        if (Random.Range(0, 1.0f) > GhostSpawnProbability)
        {
            // don't spawn an ghost
            return;
        }

        // generate random y pos
        Vector2 spawnVect = SpawnPos + new Vector2(0, Random.Range(heightOfTower + DistanceOfGhostToTower, GhostPoolHandler.MaxY));

        // spawn object
        GhostPoolHandler.SpawnNextObject(spawnVect);
    }

    #endregion

    #region Events

    void TriggerPointTriggered(object sender, TriggerPointEventArgs e)
    {
        if (_mainSpawnAreaIsFree)
            return;

        // wenn trigger was triggerd by last spawned object then set the spawn area to free
        if (_lastSpawnedObject != null && _lastSpawnedObject.GetComponent<PolygonCollider2D>().Equals(e.Collider))
        {
            _mainSpawnAreaIsFree = true;
            _lastSpawnedObject = null;
        }
    }

    void MovementTriggered(object sender, MovedDistanceTriggerEventArgs e)
    {
        // update total moved distance
        _totalMovedDistance += e.MovedDistance;
    }

    #endregion
}
