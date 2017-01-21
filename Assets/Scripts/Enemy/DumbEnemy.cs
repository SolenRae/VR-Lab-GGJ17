using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbEnemy : MonoBehaviour, ITakeDamage {
    public float touchDamage;
    public float speed;
    public float lookDistance;
    public float safetyDistance;
    private float timer = 0.0f;
    private float decisionTimer = 10.0f;
    private Rigidbody rbody;
    private float damageCooldown = 1f;
    private float cooldownTimer = 0f;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
        transform.eulerAngles = new Vector3(0.0f, Random.Range(0, 360), 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer += Time.deltaTime;
        RandomMovement();
        Move();

		
	}

    public void TakeDamage(float damage) {

    }

    void FixedUpdate() {
        /**
        *Use raycasting to check for a collision with a given range
        *If collision has happened and the collider is tagged as a boundary collider
        *find a new direction to target from the colliders normal at raycast hit point
        *safety check for rare cases where it's parallell with the normal
        *rotate enemy towards point (along the line of the wall)
        */
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookDistance)) {
            if (hit.collider.CompareTag("Boundary")) {
                Vector3 targetPosition = hit.point + hit.normal * safetyDistance;
                Vector3 cross = Vector3.Cross(rbody.velocity, hit.normal);
                if (cross.magnitude < float.Epsilon) {
                    targetPosition = hit.point - hit.normal * safetyDistance;
                }
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 25);
            }
        }
    }

    void OnCollisionStay(Collision other) {
        Debug.Log("SUP");

        if (other.gameObject.CompareTag("Player") && cooldownTimer > damageCooldown) {
            Debug.Log("SUP");
            ITakeDamage itdref = other.gameObject.GetComponent<ITakeDamage>();
            itdref.TakeDamage(touchDamage);
            cooldownTimer = 0f;
        }
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
