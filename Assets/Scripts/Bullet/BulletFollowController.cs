using System.Collections;
using DG.Tweening;
using UnityEngine;
using Helpers;

public class BulletFollowController : Instancable<BulletFollowController>
{
    private GameObject _targetArrow;
    private Transform _targetArrowOffsetObject;
    private GameObject _target;

    public float _totalDistance;
    public float currentDistance;
    public AnimationCurve bulletVelocityCurve;
    public float percentage;
    public Vector3 direction = Vector3.zero;
    public bool follow = false;
    
    public void Follow(GameObject targetArrow,GameObject target)
    {
        follow = false;
        _target = target;
        _totalDistance = Vector3.Distance(_target.transform.position, targetArrow.transform.position);
        Zoom.Instance.isShootin = false;
        
        _targetArrow = targetArrow;
        _targetArrowOffsetObject = _targetArrow.transform.GetChild(0);
        follow = true;
        StartCoroutine(BulletFollow());
    }
    private void Update()
    {
        if (_targetArrow != null && follow)
        {
            StaticObjects.MainCamera.transform.DOMove(_targetArrowOffsetObject.transform.position, .25f);
            StaticObjects.MainCamera.transform.DOLookAt(_targetArrow.transform.position, .5f);
        }
    }
    IEnumerator BulletFollow()
    {
        yield return new WaitForFixedUpdate();
        direction = _targetArrow.GetComponent<Rigidbody>().velocity.normalized;

        while (follow && _targetArrow != null && Vector3.Distance(_targetArrow.transform.position, _target.transform.position) > 0.1f)
        {
            currentDistance = Vector3.Distance(_targetArrow.transform.position, _target.transform.position);
            percentage = 1-currentDistance / _totalDistance;
            _targetArrow.GetComponent<Rigidbody>().velocity = _targetArrow.transform.forward*(1 + 30*bulletVelocityCurve.Evaluate(percentage));

            yield return new WaitForEndOfFrame();
            if (_targetArrow == null)
            {
                yield break;
            }
        }
    }
}
