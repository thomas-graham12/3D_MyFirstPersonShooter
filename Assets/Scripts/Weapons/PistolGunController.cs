using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

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
            StartCoroutine(IReloadAmmoWhenEmpty());
        }
    }

    public void Reload(InputAction.CallbackContext context)
    {
        StartCoroutine(IReloadAnytime());
    }

    IEnumerator IReloadAmmoWhenEmpty()
    {
        _playerControls.Player.Attack.started -= Shoot;
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
        _playerControls.Player.Attack.started += Shoot;
    }

    IEnumerator IReloadAnytime()
    {
        int difference = _playerManager.maxPistolAmmo - _playerManager.pistolAmmoInGun;
        _playerControls.Player.Attack.started -= Shoot;
        yield return new WaitForSeconds(1f);
        if (_playerManager.pistolAmmoInStock > 0 && difference < _playerManager.pistolAmmoInStock)
        {
            Debug.Log("If");
            _playerManager.pistolAmmoInGun += difference;
            _playerManager.pistolAmmoInStock -= difference;
        }
        else if (difference > _playerManager.pistolAmmoInStock)
        {
            Debug.Log("Else if");
            _playerManager.pistolAmmoInGun += _playerManager.pistolAmmoInStock;
            _playerManager.pistolAmmoInStock -= _playerManager.pistolAmmoInStock;
        }
        _playerControls.Player.Attack.started += Shoot;
    }
}