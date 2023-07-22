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
        if (_playerManager.pistolAmmoInGun > 0)
        {
            muzzleFlash.Play();

            RaycastHit hit;
            _playerManager.pistolAmmoInGun -= 1;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    //damageable = damageTarget.GetComponent<IDamageable>();
                    damageable.TakeDamage(weaponDamage);
                }
            }
        }

        if (_playerManager.pistolAmmoInGun <= 0)
        {
            StartCoroutine(IReloadAmmo());
        }
    }

    IEnumerator IReloadAmmo()
    {
        yield return new WaitForSeconds(1f);
        if (_playerManager.pistolAmmoInStock > 0 && _playerManager.pistolAmmoInStock < _playerManager.maxPistolAmmo)
        {
            _playerManager.pistolAmmoInGun += _playerManager.pistolAmmoInStock;
            _playerManager.pistolAmmoInStock = 0;
        }
        else if (_playerManager.pistolAmmoInStock > 0)
        {
            _playerManager.pistolAmmoInStock -= _playerManager.maxPistolAmmo;
            _playerManager.pistolAmmoInGun += _playerManager.maxPistolAmmo;
        }
    }
}