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

    private Vector3 direction;

    // Use this for initialization
    void Awake () {
        Cursor.visible = false;
        rbody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform.parent;
        cameraOffset = cameraTransform.position - transform.position;
	}

    void Update() {

        direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        yaw   += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
    }

    void FixedUpdate() {
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        direction = cameraTransform.TransformDirection(direction);
        //rbody.AddForce(direction * speed);
        if (direction != Vector3.zero) {
            rbody.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
        
        if(direction == Vector3.zero) {
            rbody.velocity        = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;
        }
        
    }
	
    void LateUpdate() {
        cameraTransform.position = transform.position + cameraOffset;
        cameraTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
