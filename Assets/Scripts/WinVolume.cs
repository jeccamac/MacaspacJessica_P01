using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] public Text winText = null;
    [SerializeField] public Text menuText = null;

    private AudioSource _soundWin;

    public void Awake()
    {
        winText.enabled = false;
        menuText.enabled = false;
        _soundWin = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // detect if it's the player
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we found player, continue
        if (playerShip != null && GameInput._score>= 5)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject, 1f);
            _soundWin.Play();
            winText.enabled = true;
            menuText.enabled = true;
            
        } else 
        { 
            Debug.Log("Not enough Radium");
        }
    }
}

