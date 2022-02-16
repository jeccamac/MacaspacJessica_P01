using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSpeed : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerupDuration = 5;
    [SerializeField] public Text _speedText = null;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;
    private AudioSource _soundPowerup;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
        _soundPowerup = GetComponent<AudioSource>();
        _speedText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
                = other.gameObject.GetComponent<PlayerShip>();
        // if we have a valid player and not already powered up
        if (playerShip != null && _poweredUp == false)
        {
            //start powerup timer, restart if it's already started
            StartCoroutine(PowerupSequence(playerShip));
        }        
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // set boolean for detecting lockout
        _poweredUp = true;
        _speedText.enabled = true;
        _soundPowerup.Play();

        ActivatePowerup(playerShip);
        // simulate this object being disabled. We dont REALLY want to disable it bc we still need script behavior to continue functioning
        DisableObject();
        

        // wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        // reset
        DeactivatePowerup(playerShip);
        EnableObject();

        // set boolean to release lockout
        _poweredUp = false;
        _speedText.enabled = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if(playerShip != null)
        {
            // powerup player
            playerShip.SetSpeed(_speedIncreaseAmount);
            // visuals
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        // revert player powerup - will subtract
        playerShip?.SetSpeed(-_speedIncreaseAmount);
        // visuals
        playerShip?.SetBoosters(false);
    }

    public void DisableObject()
    {
        // disable collider, so it cant be retriggered
        _colliderToDeactivate.enabled = false;
        // disable visuals, to simulate deactivated
        _visualsToDeactivate.SetActive(false);
        // TODO reactivate particle flash/audio
    }

    public void EnableObject()
    {
        // enable collider, so it can be retriggered
        _colliderToDeactivate.enabled = true;
        // enable visuals again, to draw player attention
        _visualsToDeactivate.SetActive(true);
        // TODO reactivate particle flash/audio
    }
}
