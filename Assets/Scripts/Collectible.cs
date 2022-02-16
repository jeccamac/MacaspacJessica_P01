using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] public AudioSource _soundCollect = null;
    [SerializeField] public float _rotateSpeed = 0.5f;

    private void Start()
    {
        _soundCollect = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(_rotateSpeed, _rotateSpeed, _rotateSpeed, Space.World); // (x, y, z, relative to World)
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();

        // if we have a valid player
        if (playerShip != null)
        {
            Debug.Log("Collected");
            _soundCollect.Play();

            GameInput._score += 1;

            Destroy(gameObject, 0.3f);
        }
    }
}
