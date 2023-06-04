using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    [SerializeField]
    protected double Health = 10.0;
    protected double MaxHealth;

    [SerializeField]
    protected GameObject HealthGameObject;
    protected TextMeshProUGUI HealthText;

    protected GameObject ZombieHealthGreenBar;

    public void TakeDamage(double damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            if(gameObject.tag != "Player")
            {
                GetComponent<LootBag>().InstantiateLoot(transform.position);
            }
            SoundManagerScript.PlaySound ("zombie_death");
            Destroy(gameObject);
            Debug.Log($"Object {gameObject.name} destroyed due to health below 0");
        }

        if(gameObject.tag == "Player")
            UpdateHealthCounter();

        if(gameObject.tag == "Enemy")
            UpdateZombieHealthCounter();
    }

    public double GetHealth()
    {
        return Health;
    }

    public void GainHealth(double health)
    {
        if((Health + health) <= MaxHealth)
        {
            Health += health;
            Debug.Log($"Gained {health} health");
        }

        if (gameObject.tag == "Player")
            UpdateHealthCounter();
    }

    protected void UpdateHealthCounter()
    {
        HealthText.SetText(Health.ToString());
    }

    protected void UpdateZombieHealthCounter()
    {
        if (!HealthGameObject.active)
            HealthGameObject.SetActive(true);

        float scale = (float)(Health / MaxHealth);
        ZombieHealthGreenBar.transform.localScale = new Vector3(scale, 1, 1);
    }
}
