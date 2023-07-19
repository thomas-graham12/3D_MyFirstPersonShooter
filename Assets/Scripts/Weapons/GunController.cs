using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] int weaponDamage;
    [SerializeField] float range;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] float nextTimeToFire;
    [SerializeField] int ammo;

    PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    private void OnEnable()
    {
        _playerControls.Player.Attack.started += Shoot;
    }

    private void OnDisable()
    {
        _playerControls.Player.Attack.started -= Shoot;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (ammo > 0)
        {
            muzzleFlash.Play();

            RaycastHit hit;
            ammo -= 1;
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
    }
}