using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Instancable<Enemy>
{
    private GameObject _target;
    public Animator _EnemyAnimator;
    private Vector3 _delta;
    
    private bool _hasExecutedAttackSound = false;

   [SerializeField] private GameObject _hips;
    private LevelData _levelData => LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel];
    
    [SerializeField] private GameObject arrowPointer;

    private float distance;
    
    public NavMeshAgent agent;
    
    private bool _isDead;

    public float hipPos;
    
   
    public void SetTarget(GameObject target)
    {
        _target = target;

        if (_target != null)
        {
            _delta = target.transform.position - transform.position;
            _EnemyAnimator.SetBool("Run", true);
        }
        else
        {
            agent.isStopped = true;
        }
    }
    
    public void SetDead(bool isDead)
    {
        _isDead = isDead;
        GetComponent<Collider>().enabled = false;
        
        if (_EnemyAnimator != null)
        {
            _EnemyAnimator.SetBool("Run", false);
            _EnemyAnimator.SetBool("isDead", true);
        }

        arrowPointer.SetActive(false);
    }

    public void PositionSet()=> _hips.transform.localPosition= new Vector3(_hips.transform.localPosition.x,hipPos,_hips.transform.localPosition.z);
   

    private void FixedUpdate()
    {
        if (!LevelManager.IsGameRunning)
            Destroy(gameObject);
        
        if (_hasExecutedAttackSound)
            return;
        
        if (_target && !_isDead && LevelManager.IsGameRunning)
        {
            distance = Math.Abs(Vector3.Distance(_target.transform.position, transform.position));

            agent.SetDestination(_target.transform.position);
            
            if (  distance < _levelData.distance && !AimController.Instance.isBulletFollow)
            {
                _EnemyAnimator.SetBool("Run", false);
                _EnemyAnimator.SetBool("Attack", true);
                
                if (!_hasExecutedAttackSound)
                {
                    AimController.Instance.audio.PlayOneShot(AimController.Instance.clips[0]);
                    _hasExecutedAttackSound = true;
                }
            }
            else
            {
                _EnemyAnimator.SetBool("Attack", false);
                _EnemyAnimator.SetBool("Run", true);
                _hasExecutedAttackSound = false;
            }
            
        }


    }

    public void OnCompleteHit()
    {
        if (!LevelManager.IsGameRunning)
            return;

        LevelManager.IsGameRunning = false;
        
        GameManager.Instance.buttonPanel.SetActive(false);
        GameManager.Instance.reloadPanel.SetActive(true);
    }
}