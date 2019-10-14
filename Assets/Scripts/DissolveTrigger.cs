using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTrigger : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject[] ObjectsToDissolve;
    List<DissolveScript> dissolveScripts;


    // Start is called before the first frame update
    void Start()
    {
        dissolveScripts = new List<DissolveScript>();

        if(ObjectsToDissolve.Length > 0)
        {
            foreach (GameObject item in ObjectsToDissolve)
            {
                dissolveScripts.Add(item.GetComponent<DissolveScript>());
            }     
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            foreach(DissolveScript script in dissolveScripts)
            {
                StartCoroutine(script.Dissolve());
            }
        }
    }
}
