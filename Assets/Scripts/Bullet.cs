using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _canMove = false;
    private float _speed = 0.5f;
    private Transform _targetPoint;
    private Vector3 _startPoint;
    private float lastWaypointSwitchTime;
    private float pathLength;
    private float totalTimeForPath;
    private float currentTimeOnPath;
    private float _time = 0.1f;

    public void StartFly(Transform targetPos)
    {
        transform.position = new Vector3(0, 0, 0);
        _startPoint = transform.position;
        _targetPoint = targetPos;
        gameObject.SetActive(true);
        _canMove = true;
        StartCoroutine(FlyBullet());
    }

    private IEnumerator FlyBullet()
    {
        while (_canMove)
        {
            pathLength = Vector3.Distance(_startPoint, _targetPoint.position);
            totalTimeForPath = pathLength / _speed;
            currentTimeOnPath = Time.time - lastWaypointSwitchTime;
            gameObject.transform.position = Vector3.Lerp(_startPoint, _targetPoint.position, currentTimeOnPath / totalTimeForPath);
            //Debug.Log(_targetPoint.position);
            if (gameObject.transform.position == _targetPoint.position)
            {
                Debug.Log("Kick!");
                _canMove = false;
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(_time);
        }
    }
}
