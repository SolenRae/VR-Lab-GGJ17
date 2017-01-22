using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, ITakeDamage {
    public float maxHealth;
    public float numFlashes;
    public float deathDelay;
    private float curHealth;
    private bool dead = false;
    private Text textReference;
    private bool hasDied = false; // check if the player have run the die corutine once.

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
        StartCoroutine(DamageFlash());
        if (curHealth <= 0) {
            curHealth = 0;
            dead = true;
        }
    }

	// Update is called once per frame
	void Update () {
        if(dead && !hasDied) {
            GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[0];
            GetComponent<PlayerController>().enabled = false;
            StartCoroutine(HandleDeath(deathDelay));
        }
        if(!dead) {
            if(curHealth == maxHealth) {
                GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[(int)(curHealth / 10)];
            }
        }

    }

    IEnumerator HandleDeath(float delay) {
        yield return new WaitForSeconds(delay);
        GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;

        textReference.alignment = TextAnchor.MiddleCenter;
        textReference.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textReference.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textReference.rectTransform.position.Set(0.0f, 0.0f, 0.0f);
        textReference.text = "YOU DEAD SON";
        textReference.enabled = true;

        Destroy(gameObject);
        Destroy(GameObject.Find("Healthbar"));
    }

    IEnumerator DamageFlash() {
        for(int i = 0; i < numFlashes; ++i) {
            GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[(int)(curHealth / 10) + 1];
            yield return new WaitForSeconds(0.25f);
            GameObject.Find("Healthbar").GetComponent<Image>().sprite = healthSprites[(int)(curHealth / 10)];
            yield return new WaitForSeconds(0.25f);

        }
    }
}
