using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PistolGunController : MonoBehaviour
{
    [SerializeField] int weaponDamage;
    [SerializeField] float range;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;

    PlayerControls _playerControls;
    PlayerManager _playerManager;

    [SerializeField] TextMeshProUGUI allPistolAmmoText;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerManager = transform.root.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        allPistolAmmoText.text = _playerManager.pistolAmmoInGun + "/" + _playerManager.pistolAmmoInStock;
    }

    private void OnEnable()
    {
        _playerControls.Player.Attack.started += Shoot;
        _playerControls.Player.Reload.started += Reload;
    }

    private void OnDisable()
    {
        _playerControls.Player.Attack.started -= Shoot;
        _playerControls.Player.Reload.started -= Reload;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (_playerManager.pistolAmmoInGun > 0)
        {
            muzzleFlash.Play();

            RaycastHit hit; // Stores raycast info in this variable.
            _playerManager.pistolAmmoInGun -= 1;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) // if the the raycast does hit
            {
                if (hit.transform.TryGetComponent(out IDamageable damageable)) // Checks for IDamageable
                {
                    damageable.TakeDamage(weaponDamage);
                }
            }
        }

        if (_playerManager.pistolAmmoInGun <= 0)
        {
            StartCoroutine(IReloadAmmoWhenEmpty());
        }
    }

    public void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine(IReloadAnytime());
    }

    IEnumerator IReloadAmmoWhenEmpty() // Used for when the gun is empty.
    {
        _playerControls.Player.Attack.started -= Shoot; // Takes away the ability to shoot
        yield return new WaitForSeconds(1f);
        if (_playerManager.pistolAmmoInStock > 0 && _playerManager.pistolAmmoInStock < _playerManager.maxPistolAmmo) 
        {
            // if the ammoInStock is greater than 0, but less than maxPistolAmmo, add whatever is left in the stock to the gun and make the stock 0
            _playerManager.pistolAmmoInGun += _playerManager.pistolAmmoInStock;
            _playerManager.pistolAmmoInStock = 0;
        }
        else if (_playerManager.pistolAmmoInStock > 0)
        {
            // else if the ammo in stock is above 0, take away the maxPistolAmmo from the stock and add it to the gun 
            _playerManager.pistolAmmoInStock -= _playerManager.maxPistolAmmo;
            _playerManager.pistolAmmoInGun += _playerManager.maxPistolAmmo;
        }
        _playerControls.Player.Attack.started += Shoot;
    }

    IEnumerator IReloadAnytime()
    {
        int difference = _playerManager.maxPistolAmmo - _playerManager.pistolAmmoInGun; // Grabs the number to add into the gun
        _playerControls.Player.Attack.started -= Shoot;
        yield return new WaitForSeconds(1f);
        if (_playerManager.pistolAmmoInStock > 0 && difference < _playerManager.pistolAmmoInStock)
        {
            // If the ammo in stock is greater than 0, but the difference is lesser than the ammo in stock, add the difference to the gun and take away the difference from the ammo in stock
            Debug.Log("If");
            _playerManager.pistolAmmoInGun += difference;
            _playerManager.pistolAmmoInStock -= difference;
        }
        else if (difference > _playerManager.pistolAmmoInStock)
        {
            // else if the difference is greater than the ammo in stock, add whatever is left in the stock to the gun then remove whatever is left in the stock.
            Debug.Log("Else if");
            _playerManager.pistolAmmoInGun += _playerManager.pistolAmmoInStock;
            _playerManager.pistolAmmoInStock -= _playerManager.pistolAmmoInStock;
        }
        _playerControls.Player.Attack.started += Shoot;
    }
}