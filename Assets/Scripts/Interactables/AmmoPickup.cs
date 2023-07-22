using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoPickupAmount;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out PlayerManager playerManager);

        if (playerManager)
        {
            playerManager.pistolAmmoInGun += ammoPickupAmount;

            Destroy(gameObject);
        }
    }
}
