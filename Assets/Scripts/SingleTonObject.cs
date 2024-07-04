using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SingleTonObject : MonoBehaviour
{
    [SerializeField] private string id;

    public static Dictionary<string, string> singletonDictionary = new Dictionary<string, string>();

    private static SingleTonObject instance = null;

    private void Awake()
    {
        if (string.IsNullOrEmpty(id)) throw new System.Exception("Please enter a valid id");

        if (singletonDictionary.ContainsKey(id) ) Destroy(gameObject);

        instance = this;
        singletonDictionary.Add(id, id);
        DontDestroyOnLoad(gameObject);

    }
}
