using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, ITakeDamage {
    public float maxHealth;
    private float curHealth;
    private bool dead = false;
    private Text textReference;
    //List<Sprite> HealthSprites = new List<Sprite>();

    [SerializeField]
    public Sprite[] healthSprites;

    // Use this for initialization
    void Start () {
        curHealth = maxHealth;
        textReference = GameObject.Find("Text").GetComponent<Text>();
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
            GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[0];
            Destroy(gameObject, 5);
            Destroy(GameObject.Find("Healthbar"), 5);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;

            //Set text and alignment canvas, but idk why it doesn't work properly with position
            textReference.alignment = TextAnchor.MiddleCenter;
            textReference.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textReference.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textReference.rectTransform.position.Set(0.0f, 0.0f, 0.0f);
            textReference.text = "YOU DEAD SON";
        }

        if(!dead) {
            GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[(int)(curHealth / 10)];
        }

    }
}
