using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _timeShot;
    [SerializeField] private Bullet _bulletPref;
    [SerializeField] private List<Enemy> _enemys;
    private bool _canShot = false;

    private void OnDisable()
    {
        _canShot = false;
    }

    private IEnumerator Shot()
    {
        while(_canShot)
        {
            var bullet = Instantiate(_bulletPref, transform);
            bullet.StartFly(_enemys[0].gameObject.transform);
            yield return new WaitForSeconds(_timeShot);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            _enemys.Add(enemy);
            if (_enemys.Count == 1)
            {
                _canShot = true;
                StartCoroutine(Shot());
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            _enemys.Remove(enemy);
            if(_enemys.Count == 0)
            {
                _canShot = false;
            }
        }
    }
}
