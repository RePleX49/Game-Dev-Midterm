using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    [SerializeField] float RotationMultiplier = 2.0f;

    DissolveScript dScript;

    private void Start()
    {
        dScript = GetComponent<DissolveScript>();
        if (dScript != null)
        {
            InvokeRepeating("FlipFlop", 0.0f, Random.Range(1.25f, 12.25f));
        }
    }

    void Update()
    {
        transform.Rotate(Random.rotation.eulerAngles * Time.deltaTime * RotationMultiplier);
    }

    void FlipFlop()
    {
        dScript.FlipFlopEffect();
    }
}
