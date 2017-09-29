using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool<T> where T : IReusable
{
    public delegate GameObject FactoryDelegate();

    private Stack<GameObject> _objects;
    private FactoryDelegate _factory;
    private GameObject _containerGameObject;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="factory">Delegate de creacion</param>
    /// <param name="containerN">Nombre del TAG del contenedor para almacenar los objetos</param>
    /// <param name="quantity">Cantidad de objetos inicial</param>
    public ObjectPool(FactoryDelegate factory, string containerN, int quantity = 5)
    {
        _objects = new Stack<GameObject>();
        _factory = factory;
        _containerGameObject = GameObject.FindGameObjectWithTag(containerN);
        if (quantity >= 0)
        {
            for (int i = 0; i < quantity; i++)
            {
                _objects.Push(Create());
            }
        }
    }

    /// <summary>
    /// Obtencion de gameobject del pool. Ejecuta OnAcquire().
    /// </summary>
    /// <returns>Gameobject del pool</returns>
    public GameObject GetObject()
    {
        GameObject elem;
        if (_objects.Count > 0)
        {
            elem = _objects.Pop();
        }
        else
        {
            elem = Create();
        }
        elem.GetComponent<T>().OnAcquire();
        return elem;
    }

    /// <summary>
    /// Almacenar gameobject en el pool. Ejecuta OnRelease().
    /// </summary>
    /// <param name="obj">Objeto para guardar</param>
    public void PutBackObject(GameObject obj)
    {
        obj.GetComponent<T>().OnRelease();
        _objects.Push(obj);
    }

    /// <summary>
    /// Crea los objetos del pool. Ejecuta OnCreate().
    /// </summary>
    /// <returns>objeto del pool</returns>
    private GameObject Create()
    {
        var elem = _factory();
        elem.GetComponent<T>().OnCreate();
        elem.gameObject.transform.parent = _containerGameObject.transform;
        return elem;
    }
}