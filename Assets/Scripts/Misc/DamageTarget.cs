using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTarget : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int Health { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
    }

    // Update is called once per frame
    public void Hit(int damageAmount)
    {
        Health -= damageAmount;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Hit(damageAmount);
    }
}
