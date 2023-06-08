using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using Helpers;
using Random = UnityEngine.Random;

public class Bullet : Instancable<Bullet>
{
   [SerializeField] private Camera mainCamera;
   
   private IEnumerator _enumerator;

   private Enemy enemyTarget;

   private bool _hasHostage;

   public void SetHostage(bool hasHostage)
   {
      _hasHostage = hasHostage;
   }
   private void Start()
   {
      mainCamera = Camera.main;
   }
   private void OnTriggerEnter(Collider other)
   {
      StartCoroutine(IsTrigger(other));
   }
   IEnumerator IsTrigger( Collider other)
   {
      if (other.CompareTag("Target"))
      {
         gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
         gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
         
         var hostages = FindObjectsOfType<HostageEscapeNavmesh>();
         
         var enemies=FindObjectsOfType<Enemy>();

         foreach (var hostage in hostages)
         {  
            hostage.Run();
         }
         var target = hostages[Random.Range(0, hostages.Length)];
         foreach (var enemy in enemies)
         {
            enemy.SetTarget(target.gameObject);
         }
         
         BulletFollowController.Instance.follow = false;
         other.GetComponent<Enemy>().SetDead(true);
      }
      else if (other.CompareTag("Hostage"))
      {
      
         gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
         gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
         BulletFollowController.Instance.follow = false;
      }

      bool a = other.CompareTag("Target") && LevelManager.Instance._targetCount < 1;
      yield return new WaitForSeconds(a ? 2 : 0.5f);
      
      if (a)
      {
         AimController.Instance.StartCoroutine(AimController.Instance.NextPanel());
         CameraSwitchAnimator.Instance.SwitchToThirdPersonPos();
      }
      else if (_hasHostage ||  LevelManager.Instance.bulletCount<1 )
      {
         StartCoroutine(AimController.Instance.ReloadPanel());
      }

      Zoom.Instance.isShootin = true;
      Destroy(gameObject);
      DOTween.Kill(StaticObjects.MainCamera.gameObject);
      AimController.Instance.isBulletMove = true;
   }

   
  
}
