﻿using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform target;

    void LateUpdate ()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}
}