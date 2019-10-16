using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] CheckPointSystem CPS;

    void OnTriggerEnter(Collider other)
    {
        CPS.SetCheckpoint(transform.position);
        Debug.Log("Set CheckPoint");
    }
}
