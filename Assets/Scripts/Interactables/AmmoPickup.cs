using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    /// <summary>
    /// Script to attach onto a UnityEvent. Grabs pistolAmmoInStock and adds the pickup ammount to it
    /// </summary>

    [SerializeField] int ammoPickupAmount;
    [SerializeField] PlayerManager _playerManager;

    public void PistolAmmoPickup()
    {
        _playerManager.pistolAmmoInStock += ammoPickupAmount;

        Destroy(gameObject);
    }
}
