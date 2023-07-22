using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public int healthAmount;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out PlayerManager playerManager);
        if (playerManager)
        {
            playerManager.health += healthAmount;
            Destroy(gameObject);
        }
    }
}
