using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Instancable<LevelManager>
{
    public static bool IsGameRunning = true;
    
    public List<LevelData> LevelDatas;
    public int CurrentLevel
    {
        get => PlayerPrefs.GetInt("currentLevel", 0);
        set => PlayerPrefs.SetInt("currentLevel", value);
    }
    public LevelData CurrentLevelData { get; set; }
    public UnityAction onLevelLoaded;

    public int _targetCount;
    public int bulletCount;
    public float minRotate;
    public float maxRotate;
    
    public float minRotateX;
    public float maxRotateX;

    private void Awake()
    {
        CurrentLevel = 0;
        
        _targetCount = LevelDatas[Instance.CurrentLevel].enemyCount;
        bulletCount = LevelDatas[Instance.CurrentLevel].bulletCount;
        
        minRotate = LevelDatas[Instance.CurrentLevel].minRotationAngleY;
        maxRotate=LevelDatas[Instance.CurrentLevel].maxRotationAngleY;
        
        minRotateX = LevelDatas[Instance.CurrentLevel].minRotationAngleX;
        maxRotateX=LevelDatas[Instance.CurrentLevel].minRotationAngleX;
    }
    private void Start()
    {
        CurrentLevelData = LevelDatas[CurrentLevel];
        onLevelLoaded += GameManager.Instance.SetLevel;
        onLevelLoaded();
    }
    public void LevelUp()
    {
        CurrentLevel++;

        if (CurrentLevel > 5)
        {
            CurrentLevel = 0;
        }
        CurrentLevelData = LevelDatas[CurrentLevel];
        _targetCount = LevelDatas[Instance.CurrentLevel].enemyCount;
        bulletCount = LevelDatas[Instance.CurrentLevel].bulletCount;
        
        minRotate = LevelDatas[Instance.CurrentLevel].minRotationAngleY;
        maxRotate=LevelDatas[Instance.CurrentLevel].maxRotationAngleY;
        
        minRotateX = LevelDatas[Instance.CurrentLevel].minRotationAngleX;
        maxRotateX=LevelDatas[Instance.CurrentLevel].minRotationAngleX;
        
        AimController.Instance._shootEnabled = true;
        
        AimController.Instance.isBulletMove = true;
    }

    public void Reload()
    {
        IsGameRunning = true;
        
        CurrentLevelData = LevelDatas[CurrentLevel];
        _targetCount = LevelDatas[Instance.CurrentLevel].enemyCount;
        bulletCount = LevelDatas[Instance.CurrentLevel].bulletCount;
        
        minRotate = LevelDatas[Instance.CurrentLevel].minRotationAngleY;
        maxRotate=LevelDatas[Instance.CurrentLevel].maxRotationAngleY;
        
        minRotateX = LevelDatas[Instance.CurrentLevel].minRotationAngleX;
        maxRotateX=LevelDatas[Instance.CurrentLevel].minRotationAngleX;
        
        AimController.Instance._shootEnabled = true;
        AimController.Instance.isBulletMove = true;
    }
}
