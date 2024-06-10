using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IteractableObjectType
{
    Oven, Fridge, TV, Fan
}
public class InteractableObjectsClickCounter : MonoBehaviour
{
    public static InteractableObjectsClickCounter Instance { get; private set; }

    private Dictionary<IteractableObjectType, int> objectsClickCounter = new Dictionary<IteractableObjectType, int>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterClickOnObject(IteractableObjectType type)
    {
        if(objectsClickCounter.ContainsKey(type))
        {
            objectsClickCounter[type]++;
            FirebaseAppInit.Instance.AnalyticsClicksObjects(type, objectsClickCounter[type]);
        }
    }
}
