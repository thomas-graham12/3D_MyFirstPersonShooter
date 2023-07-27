using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    /// <summary>
    /// Melee enemy that deals damage if player collides with it.
    /// </summary>

    [SerializeField] int damageAmount;
    bool takenDamage = false;

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;

        collision.transform.TryGetComponent(out PlayerManager playerManager);

        if (playerManager)
        {
            Debug.Log("Collided");
            if (!takenDamage)
            {
                Debug.Log("Damage Taken");
                damageable = playerManager.GetComponent<IDamageable>();
                damageable.TakeDamage(damageAmount);
                takenDamage = true;
                StartCoroutine(ITimeBetweenDamage());
            }
        }
    }


    IEnumerator ITimeBetweenDamage() // Used to give time before player can take damage again
    {
        yield return new WaitForSeconds(.2f);
        takenDamage = false;
    }
}
