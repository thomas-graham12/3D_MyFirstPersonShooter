using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] int weaponDamage;
    [SerializeField] float range;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] float nextTimeToFire;
    [SerializeField] int ammo;

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                IDamageable damageable;

                hit.transform.TryGetComponent(out DamageTarget damageTarget);
                damageable = damageTarget.GetComponent<IDamageable>();
                damageable.TakeDamage(weaponDamage);
            }
        }
        else
        {
            Debug.Log("Cannot shoot anymore! Out of bullets!");
        }
        ammo -= 1;
    }
}
