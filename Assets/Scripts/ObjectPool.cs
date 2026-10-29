using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize =10;


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool() 
    {
        for (int i = 0; i < poolSize;i++)
        {
            CreateNewObject();
        }
    }


    private List<GameObject> objectPoolList = new List<GameObject>();

    /// <summary>
    /// Instantiate new Object and add it to the pool list
    /// </summary>
    /// <returns>GameObject</returns>
    private GameObject CreateNewObject() 
    {
        GameObject newObject = Instantiate(objectPrefab);

        newObject.SetActive(false);

        objectPoolList.Add(newObject);

        return newObject;
    }


    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>GameObject</returns>

    public GameObject GetObjectFromPool() 
    {
        GameObject theObject = objectPoolList.Find(obj => !obj.activeInHierarchy);

        if (theObject == null)
        {
            theObject = CreateNewObject();
        }

        theObject.SetActive(true);
        return theObject;
    }
}
