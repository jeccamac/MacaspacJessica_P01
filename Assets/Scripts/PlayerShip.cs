using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // require a RigidBody, will create one if there is none
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trail = null;

    Rigidbody _rb = null; // variable of Rigidbody to store data

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>(); // search gameObject it's attached to for Rigidbody component

        _trail.enabled = false;
    }

    // Physics requires a consistent time-step for calculations, FixedUpdate is called every X times a second, mo matter current workload, Update called asap (every frame)
    // This is ideal for Physics calculations, though does NOT happen every frame - so don't use to detect if(keypress) that tracks exact frame key was pressed (for Update)
    // Work-around for this is teh Input.GetAxis() system
    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    // Use forces to build momentum forward/backward
    void MoveShip()
    {
        // S-down = -1, W-up = 1, None = 0. Scale by movespeed
        float moveAmoutThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine direction with calculated amount
        Vector3 moveDirection = transform.forward * moveAmoutThisFrame;
        // apply movement to the physics object
        _rb.AddForce(moveDirection);
    }

    // Don't use forces, we want rotations to be precise
    void TurnShip()
    {
        // A-left = -1, D-right = 1, None = 0. Scale by turnspeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0); // only rotate on y axis
        // spin the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
        // maybe add particle effects?

    }

    public void SetSpeed(float speedChange)
    {
        _moveSpeed += speedChange;
        // TODO audio/visuals
    }

    public void SetBoosters(bool activeState)
    {
        _trail.enabled = activeState;
    }
}
