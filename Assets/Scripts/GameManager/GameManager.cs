using System;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class GameManager : Instancable<GameManager>
{
    [Header("Ground")]
    [SerializeField] private Transform _playerTransform;
    [HideInInspector] public GameObject _parent;
    
    [Header("UI Panels")]
    public GameObject nextLevelCanvas;
    public GameObject buttonPanel;
    public GameObject reloadPanel;

    public NavMeshData m_NavMeshData;
    private NavMeshDataInstance m_NavMeshInstance;

    public void SetLevel()
    {
        _parent = new GameObject("PlayGround");
        Instantiate(LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].Map, new Vector3(0, 0, 0), Quaternion.identity,_parent.transform);
        PlayerTransformSet();
        CreateEnemy();
        CreateHostage();
        Zoom.Instance.zoomSlider.value = 60;
    }

    private void GenerateNavmesh()
    {
        if (m_NavMeshInstance.valid)
        {
            NavMesh.RemoveNavMeshData(m_NavMeshInstance);
        }
        
        m_NavMeshInstance = NavMesh.AddNavMeshData(m_NavMeshData);
    }

    private void PlayerTransformSet()
    {
        _playerTransform.position = LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].playerPosition;
        _playerTransform.rotation = LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].playerRotation;
        _playerTransform.localScale=  LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].playerScale; 
    }
    public void CreateHostage()
    {
        for (int i = 0; i < LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].hostages.Count; i++)
        { 
            Instantiate(LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].hostages[i].prefab,
                LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].hostages[i].spawnPoint,
                LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].hostages[i].prefab.transform.rotation ,_parent.transform);
        }
    }

   public void CreateEnemy()
    {
        for (int i = 0; i < LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].enemies.Count; i++)
        { 
            var createdEnemy = Instantiate(LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].enemies[i].prefab,
                LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].enemies[i].spawnPoint,
                LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].enemies[i].prefab.transform.rotation ,_parent.transform);
        }
    }
   
   
   
}
