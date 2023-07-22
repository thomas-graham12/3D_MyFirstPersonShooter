using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public int health = 100;
    public int MaxHealth { get; private set; } = 100;
    public int pistolAmmoInStock;
    public int pistolAmmoInGun;
    public int maxPistolAmmo;

    public bool allowDamage = true;
    
    // Start is called before the first frame update
    void Start()
    {
        maxPistolAmmo = 10;
        pistolAmmoInGun = maxPistolAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }
}
