using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private Portal m_portalObject;

    [SerializeField] private Transform cameraGGO;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Portal instPortalObj = Instantiate(m_portalObject, Vector3.zero, Quaternion.identity);
        instPortalObj.CameraTransform = cameraGGO;

        Vector3 spawnedObjPosition = instPortalObj.transform.position;
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 direction = cameraPosition - spawnedObjPosition;
        Vector3 targetRotation = Quaternion.LookRotation(direction).eulerAngles;
        Vector3 scaleTargetRotation = Vector3.Scale(targetRotation, instPortalObj.transform.up.normalized);
        Quaternion targetQuaternion = Quaternion.Euler(scaleTargetRotation);
        instPortalObj.transform.rotation = instPortalObj.transform.rotation * targetQuaternion;
    }
}
