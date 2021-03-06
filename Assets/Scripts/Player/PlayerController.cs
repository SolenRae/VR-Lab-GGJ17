﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Vector3 cameraOffset;
    private Rigidbody rbody;
    private Camera playerCam;
    private float yaw = 0f;
    private float pitch = 0f;

    private Vector3 direction;

    // Use this for initialization
    void Awake () {
        Cursor.visible = false;
        Camera.main.enabled = false;
        rbody = GetComponent<Rigidbody>();
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        cameraOffset = playerCam.transform.position - transform.position;
	}

    void Update() {

        direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        yaw   += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
    }

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        direction = playerCam.transform.TransformDirection(direction);
        if (direction != Vector3.zero) {
            rbody.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
        
        if(direction == Vector3.zero) {
            rbody.velocity        = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;
        }
        
    }
	
    void LateUpdate() {
        playerCam.transform.position = transform.position + cameraOffset;
        playerCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
