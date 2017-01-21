using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    private Transform cameraTransform;
    private Vector3 cameraOffset;
    private Rigidbody rbody;
    private float yaw = 0f;
    private float pitch = 0f;


    // Use this for initialization
    void Awake () {
        rbody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform.parent;
        cameraOffset = cameraTransform.position - transform.position;
	}
	
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVeritcal = Input.GetAxis("Mouse Y");

        yaw += rotateHorizontal;
        pitch -= rotateVeritcal;


        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cameraTransform.TransformDirection(movement);
        rbody.AddForce(movement * speed);
    }
	
    void LateUpdate() {
        cameraTransform.position = transform.position + cameraOffset;
        cameraTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
