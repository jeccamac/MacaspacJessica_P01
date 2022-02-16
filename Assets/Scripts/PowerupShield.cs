using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupShield : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _powerupDuration = 5;
    [SerializeField] public Text _shieldText = null;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null; //visuals ONLY

    Collider _colliderToDeactivate = null;
    bool _shieldUp = false;
    private AudioSource _soundPowerup;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableShield();
        _soundPowerup = GetComponent<AudioSource>();
        _shieldText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
                = other.gameObject.GetComponent<PlayerShip>();
        // if we have a valid player and not already powered up
        if (playerShip != null && _shieldUp == false)
        {
            //start powerup timer, restart if it's already started
            StartCoroutine(PowerupSequence(playerShip));
        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // set boolean for detecting lockout
        _shieldUp = true;
        _shieldText.enabled = true;
        _soundPowerup.Play();

        ActivateShield(playerShip);
        // simulate this object being disabled. We dont REALLY want to disable it bc we still need script behavior to continue functioning
        DisableShield();

        // wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        // reset
        DeactivateShield(playerShip);
        EnableShield();

        // set boolean to release lockout
        _shieldUp = false;
        _shieldText.enabled = false;
    }

    void ActivateShield(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            // powerup player
            playerShip.ActivateShield(true);
            // visuals
            playerShip.SetShield(true);
        }
    }

    void DeactivateShield(PlayerShip playerShip)
    {
        // revert player powerup - will subtract
        playerShip?.ActivateShield(false);
        // visuals
        playerShip?.SetShield(false);
    }

    public void DisableShield()
    {
        // disable collider, so it cant be retriggered
        _colliderToDeactivate.enabled = false;
        // disable visuals, to simulate deactivated
        _visualsToDeactivate.SetActive(false);
        // TODO reactivate particle flash/audio
    }

    public void EnableShield()
    {
        // enable collider, so it can be retriggered
        _colliderToDeactivate.enabled = true;
        // enable visuals again, to draw player attention
        _visualsToDeactivate.SetActive(true);
        // TODO reactivate particle flash/audio
    }
}
