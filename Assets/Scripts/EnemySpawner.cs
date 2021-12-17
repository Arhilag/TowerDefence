using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private float _yieldTime;
    [SerializeField] private Enemy _prefab;
    private Pool<Enemy> _enemyPool;

    private void Start()
    {
        _enemyPool = new Pool<Enemy>(_prefab, _poolCount, transform);
        _enemyPool.AutoExpand = _autoExpand;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < _poolCount; i++)
        {
            CreateEnemy();
            yield return new WaitForSeconds(_yieldTime);
        }
    }

    private void CreateEnemy()
    {
        var enemy = _enemyPool.GetFreeElement();
        enemy.gameObject.SetActive(true);
    }
}
