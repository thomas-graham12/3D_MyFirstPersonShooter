using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoPickupAmount;
    [SerializeField] PlayerManager _playerManager;

    public void PistolAmmoPickup()
    {
        _playerManager.pistolAmmoInStock += ammoPickupAmount;

        Destroy(gameObject);
    }
}
