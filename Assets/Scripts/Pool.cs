using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }
    private List<T> pool;
    public Pool(T prefab, int count)
    {
        Prefab = prefab;
        Container = null;
        CreatePool(count);
    }

    public Pool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        pool = new List<T>();
        for(int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var obj in pool)
        {
            if(!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (AutoExpand)
            return CreateObject(true);

        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }
}
