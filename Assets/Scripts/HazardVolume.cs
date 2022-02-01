using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we found player, continue
        if (playerShip != null)
        {
            // kill player
            playerShip.Kill();
        }
    }
}
