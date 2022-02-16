using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShield : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _powerupDuration = 5;

    [Header("Setup")]
    [SerializeField] GameObject _shieldToDeactivate = null; //visuals ONLY

    Collider _colliderToDeactivate = null;
    bool _shieldUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableShield();
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
        _shieldToDeactivate.SetActive(false);
        // TODO reactivate particle flash/audio
    }

    public void EnableShield()
    {
        // enable collider, so it can be retriggered
        _colliderToDeactivate.enabled = true;
        // enable visuals again, to draw player attention
        _shieldToDeactivate.SetActive(true);
        // TODO reactivate particle flash/audio
    }
}
