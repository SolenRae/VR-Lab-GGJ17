using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : MonoBehaviour {
    public float speed;
    private float timer = 0.0f;
    private float decisionTimer = 2.0f;
    private Rigidbody rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0.0f, Random.Range(0, 360), 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        RandomMovement();
        Move();

		
	}

    private void RandomMovement() {
        timer += Time.deltaTime;
        if(timer > decisionTimer) {
            transform.eulerAngles = new Vector3(0.0f, Random.Range(0, 360), 0.0f);
            timer = 0.0f;
        }
    }

    private void Move() {
        rbody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
}
