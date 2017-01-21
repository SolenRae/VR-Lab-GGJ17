using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, ITakeDamage {
    public float maxHealth;
    private float curHealth;
    private bool dead = false;
    private Text textReference;
    
	// Use this for initialization
	void Start () {
        curHealth = maxHealth;
        textReference = GameObject.Find("HealthText").GetComponent<Text>();
	}
	
    public void TakeDamage(float damage) {
        if(dead) {
            return;
        }
        curHealth -= damage;
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

            //Set text and alignment canvas, but idk why it doesn't work properly with position
            textReference.alignment = TextAnchor.MiddleCenter;
            textReference.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textReference.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textReference.rectTransform.position.Set(0.0f, 0.0f, 0.0f);
            textReference.text = "YOU DEAD SON";
        }

        if(!dead) {
            textReference.alignment = TextAnchor.LowerCenter;
            textReference.rectTransform.anchorMin = new Vector2(0.5f, 0.0f);
            textReference.rectTransform.anchorMax = new Vector2(0.5f, 0.0f);
            textReference.rectTransform.position.Set(0.0f, 40.0f, 0.0f);
            textReference.text = "HEALTH: " + curHealth;
        }
        
    }
}
