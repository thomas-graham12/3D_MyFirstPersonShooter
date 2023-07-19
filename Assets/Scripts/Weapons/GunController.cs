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

    PlayerControls _playerControls;
    PlayerManager _playerManager;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerManager = transform.root.GetComponent<PlayerManager>();
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
        if (_playerManager.pistolAmmo > 0)
        {
            muzzleFlash.Play();

            RaycastHit hit;
            _playerManager.pistolAmmo -= 1;
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