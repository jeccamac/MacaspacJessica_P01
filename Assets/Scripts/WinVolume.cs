using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] public Text winText = null;
    [SerializeField] public Text menuText = null;

    private AudioSource _soundWin;
    private void Awake()
    {
        winText.enabled = false;
        menuText.enabled = false;
        _soundWin = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we found player, continue
        if (playerShip != null)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject, 1f);
            _soundWin.Play();
            winText.enabled = true;
            menuText.enabled = true;
        }
    }
}
