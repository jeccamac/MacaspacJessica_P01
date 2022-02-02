using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] public Text winText = null;
    [SerializeField] public Text menuText = null;

    private void Awake()
    {
        winText.enabled = false;
        menuText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we found player, continue
        if (playerShip != null)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            winText.enabled = true;
            menuText.enabled = true;
        }
    }
}
