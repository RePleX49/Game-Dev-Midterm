using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Vector3 CurrentCheckpoint;

    public void SetCheckpoint(Vector3 CheckPointPos)
    {
        CurrentCheckpoint = CheckPointPos;
        Debug.Log("CheckPoint Set");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Player.GetComponent<MovementController>().SettingCheckpoint = true;
            Player.transform.position = CurrentCheckpoint;
            Invoke("ResetCheckpointBool", 0.35f);
        } 
    }

    private void ResetCheckpointBool()
    {
        Player.GetComponent<MovementController>().SettingCheckpoint = false;
    }
}
