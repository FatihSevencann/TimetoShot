using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefs
{
    public GameObject prefab;
    public Vector3 spawnPoint;
    public float hips;
}
public class HostagePrefs
{
    public GameObject prefab;
    public Vector3 spawnPoint;
}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField] private int _levelCount;

    public int bulletCount;
    public int enemyCount;
    public int hostageCount;
    
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerScale;
    
    public GameObject Map;
    
    public List<Vector3> enemySpawnPoints;
    public List<Vector3> hostageSpawnPoints;

    public List<EnemyPrefs> enemies;
    public List<EnemyPrefs> hostages;

    public float distance;
    
    public float minRotationAngleY; 
    public float maxRotationAngleY;
    
    public float minRotationAngleX; 
    public float maxRotationAngleX;
    
    public int LevelCount
    {
        get => _levelCount;
        set => _levelCount = value;
    }
}
