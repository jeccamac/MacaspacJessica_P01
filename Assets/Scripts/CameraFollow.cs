using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;

    private void Awake()
    {
        // create an offset btwn this position and obj position
        _objectOffset = this.transform.position - _objectToFollow.position; //calculate offset here, then apply every frame (LateUpdate)
    }

    // happens after Update, Camera should always move last
    private void LateUpdate()
    {
        // apply the offset every frame, to reposition this object
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
}
