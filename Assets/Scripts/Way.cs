using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] _pointToWay;

    private void Start()
    {
        Enemy.GetNextPoint += GetPoint;
    }

    private void OnDestroy()
    {
        Enemy.GetNextPoint -= GetPoint;
    }

    private Transform GetPoint(int numPoint)
    {
        if (numPoint > _pointToWay.Length-1)
            return null;
        return _pointToWay[numPoint];
    }
}
