using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage {
    public float maxHealth;
    private float curHealth;
    private bool dead = false;

    
	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
	}
	
    public void TakeDamage(float damage) {
        if(dead) {
            return;
        }
        curHealth -= damage;
        Debug.Log(curHealth);
        if(curHealth <= 0) {
            curHealth = 0;
            dead = true;
            GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>().enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
        if(dead) {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        }
    }
}
