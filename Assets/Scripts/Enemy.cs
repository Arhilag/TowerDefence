using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _canMove = false;
    private float _speed = 2;
    private Transform _targetPoint;
    private Vector3 _startPoint;
    private float lastWaypointSwitchTime;
    private int _numPoint = 0;
    private float pathLength;
    private float totalTimeForPath;
    private float currentTimeOnPath;
    private float _time = 0.05f;

    public delegate Transform EventWay(int numPointNow);
    public static EventWay GetNextPoint;

    private void Start()
    {
        lastWaypointSwitchTime = Time.time;
        _startPoint = transform.position;
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        _targetPoint = GetNextPoint(_numPoint);
        if(_targetPoint == null)
            Debug.Log("End Game!");
        else
        {
             _canMove = true;
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        while (_canMove)
        {
            pathLength = Vector3.Distance(_startPoint, _targetPoint.position);
            totalTimeForPath = pathLength / _speed;
            currentTimeOnPath = Time.time - lastWaypointSwitchTime;
            gameObject.transform.position = Vector3.Lerp(_startPoint, _targetPoint.position, currentTimeOnPath / totalTimeForPath);
            if (gameObject.transform.position == _targetPoint.position)
            {
                _numPoint++;
                _startPoint = _targetPoint.position;
                lastWaypointSwitchTime = Time.time;
                _canMove = false;
                MoveEnemy();
            }
            yield return new WaitForSeconds(_time);
        }
    }
}
