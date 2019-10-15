using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    [SerializeField] float RotationMultiplier = 2.0f;

    void Update()
    {
        transform.Rotate(Random.rotation.eulerAngles * Time.deltaTime * RotationMultiplier);
    }
}
