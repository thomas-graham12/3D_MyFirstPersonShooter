using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] int damageAmount;
    bool takenDamage = false;

    private void OnCollisionEnter(Collision collision)
    {

        

        collision.transform.TryGetComponent(out PlayerManager playerManager);


    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IDamageable damageable;

        hit.transform.TryGetComponent(out PlayerManager playerManager);

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

    IEnumerator ITimeBetweenDamage()
    {
        yield return new WaitForSeconds(.2f);
        takenDamage = false;
    }
}
