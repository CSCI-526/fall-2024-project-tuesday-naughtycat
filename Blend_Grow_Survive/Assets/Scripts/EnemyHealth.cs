using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public TextMeshProUGUI healthText;
    public bool hasBeenShot = false;


    void Start()
    {
        currentHealth = transform.localScale.x;
        //currentHealth = Mathf.RoundToInt(transform.localScale.x);

        healthText = GetComponentInChildren<TextMeshProUGUI>(true);
        UpdateHealthText();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void UpdateHealthText()
    {
        if (healthText != null)
        {
            float enemyHp = currentHealth * 10;
            healthText.text = Mathf.RoundToInt(enemyHp).ToString();
            //healthText.fontSize =  Mathf.RoundToInt(enemyHp);
        }
    }



    void Die()
    {
        //Debug.Log("Enemy has died");
        ObjectGenerator.ins.RemoveObject(gameObject, ObjectGenerator.ins.created_enemies);
        //checking if this enemy was the last enemy of the current wave or not
        if (GameManager.instance != null)
        {
            //Debug.Log("Coming in here");
            GameManager.instance.CheckWaveCompletion();
        }
        else
        {
            //Debug.LogError("GameManager instance not found!");
        }
        Destroy(gameObject);
        
    }
}

