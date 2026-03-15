using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player.gameObject)
        {
            return;
        }
        player.GroundState(true);
    }
    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject == player.gameObject)
        {
            return;
        }
        player.GroundState(true);
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject == player.gameObject)
        {
            return;
        }
        player.GroundState(false);
    }
}