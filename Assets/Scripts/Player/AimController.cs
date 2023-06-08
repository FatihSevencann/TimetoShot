using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AimController : Instancable<AimController>
{
    private float _distanceThreshold = 1f;
    private GameObject _target;
    private GameObject _hostage;
    private bool _isAiming;
    private Enemy _targetEnemy;
    private Bullet _bullet;
    private HostageEscapeNavmesh _targetHostage;
    
    [Header("Sounds")]
    public AudioSource audio;
    public List<AudioClip> clips;
    private float volume = 1;
    
    
    
    [SerializeField] private GameObject crosshair; 
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private TMP_Text bulletCount;

    public bool bulletMove=false;
    public bool _shootEnabled = true;
    public bool isBulletMove=true;
    public bool isPlay = false;

    public bool isBulletFollow = false;

    public Button zoomHandle;

    public static bool isDrag;

    public SliderClickController SliderClickController;

    public ButtonClickController ButtonClickController;

    private void Start()
    {
        bulletCount.text = LevelManager.Instance.bulletCount.ToString();
        audio = GetComponent<AudioSource>();
    }
    public bool IsAiming
    {
        get => _isAiming;
        set
        {
            //Cursor.visible = !value;
            _isAiming = value;
            crosshair.SetActive(value);
        }
    }
    
    void Update()
    {
        if (IsAiming)
        {
            if (!SliderClickController.clicking && !ButtonClickController.clicking&& Input.GetMouseButton(0))
            {
                
                    StaticObjects.MainCamera.transform.eulerAngles +=
                        new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
                    var rot = StaticObjects.MainCamera.transform.eulerAngles;
                
                    rot.y = Mathf.Clamp(rot.y,
                        LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].minRotationAngleY-10f,
                        LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].maxRotationAngleY+6f);
                
                    rot.x=
                        Mathf.Clamp(rot.x,
                            LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].minRotationAngleX,
                            LevelManager.Instance.LevelDatas[LevelManager.Instance.CurrentLevel].maxRotationAngleX);
                
                    StaticObjects.MainCamera.transform.eulerAngles = rot;
            }
            
            if (Physics.Raycast(
                    new Ray(StaticObjects.MainCamera.transform.position, StaticObjects.MainCamera.transform.forward),
                    out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    print("hit");
                    _target = hit.collider.gameObject;
                    _targetEnemy = _target.GetComponent<Enemy>();
                }
                else if (hit.collider.CompareTag($"Hostage"))
                {
                    print("hostageHit");
                    _hostage = hit.collider.gameObject;
                    _targetHostage = _hostage.GetComponent<HostageEscapeNavmesh>();
                }
                else
                {
                    _target = null;
                    _hostage = null;
                }
            }
            else
            {
                _target = null;
                _hostage=null;
            }
        }
        
        bulletCount.text = LevelManager.Instance.bulletCount.ToString();
    }
    
    public void Shoot()
    {
        bool hasHostage = _hostage;
        bool hasTarget = _target;
        
        
        if (!isBulletMove || !hasTarget && !hasHostage)
        {
            print("obje secilmedi");

            return;
        }
        isBulletMove = false;
        IsAiming = false;
        var cameraTransform = StaticObjects.MainCamera.transform;

        if (hasTarget)
        {
            
            crosshair.SetActive(false);
            if (LevelManager.Instance._targetCount < 2)
            {
                var bullet = Instantiate(arrowPrefab, cameraTransform.position, cameraTransform.rotation);
                _bullet = bullet.GetComponent<Bullet>();
                isBulletFollow = true;
                BulletFollowController.Instance.Follow(bullet, _target);
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100);
              
               
                Zoom.Instance.zoomSlider.value = 61;
                
                foreach (var hostage in FindObjectsOfType<HostageEscapeNavmesh>())
                {
                    hostage.agent.isStopped = true;
                }   
            }
            else
            {
                audio.PlayOneShot(clips[1]);
                StartCoroutine(KillDirectly());
            }
            Zoom.Instance.isShootin = false;
            
            _targetEnemy.SetTarget(null);

        }
        else if ((hasHostage || LevelManager.Instance.bulletCount<1 ) && !isBulletFollow)
        {
            audio.PlayOneShot(clips[0]);
            
            StartCoroutine(ReloadPanel());
        }

        isBulletFollow = false;
        
        LevelManager.Instance.bulletCount--;
        LevelManager.Instance._targetCount--;
        
        StaticObjects.MainCamera.transform.DOShakePosition(0.8f, new Vector3(0.8f, 0.8f, 0.8f));

        if (_bullet)
        {
            _bullet.SetHostage(hasHostage); 
        }
        
        _target = null;
    }

    IEnumerator KillDirectly()
    {
        if (_target.CompareTag("Target"))
                {
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
                    _target.GetComponent<Enemy>().SetDead(true);
                }
                else if (_target.CompareTag("Hostage"))
                {
                   
                    BulletFollowController.Instance.follow = false;
                }

                bool a = _target.CompareTag("Target") && LevelManager.Instance._targetCount < 1;
                yield return new WaitForSeconds(a ? 2 : 0.5f);
      
                if (a)
                {
                    StartCoroutine(NextPanel());
                    CameraSwitchAnimator.Instance.SwitchToThirdPersonPos();
                }

                Zoom.Instance.isShootin = true;
                DOTween.Kill(StaticObjects.MainCamera.gameObject);
                isBulletMove = true;
    }
    #region Panels
   public IEnumerator ReloadPanel()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.buttonPanel.SetActive(false);
        GameManager.Instance.reloadPanel.SetActive(true);
    }
    public IEnumerator NextPanel()
   {
       yield return new WaitForSeconds(0.5f);
       GameManager.Instance.buttonPanel.SetActive(false);
       GameManager.Instance.nextLevelCanvas.SetActive(true);
   }
   #endregion
}