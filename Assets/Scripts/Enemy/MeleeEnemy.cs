using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] int damageAmount;
    bool takenDamage;

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;

        collision.transform.TryGetComponent(out PlayerManager playerManager);
        Debug.Log("Collided");

        if (playerManager)
        {
            if (!takenDamage)
            {
                damageable = playerManager.GetComponent<IDamageable>();
                damageable.TakeDamage(damageAmount);
                takenDamage = true;
                StartCoroutine(ITimeBetweenDamage());
            }
        }
    }

    IEnumerator ITimeBetweenDamage()
    {
        yield return new WaitForSeconds(.2f);
        takenDamage = false;
    }
}
