using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public bool playerInRange;

    [SerializeField] PlayerControls _playerControls;

    [SerializeField] UnityEvent onInteractEvent;

    private void OnEnable()
    {
        if (_playerControls != null)
        {
            _playerControls.Player.Interact.started += OnInteract;
        }
    }    
    
    private void OnDisable()
    {
        if (_playerControls != null)
        {
            _playerControls.Player.Interact.started -= OnInteract;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
        if (playerInRange)
        {
            onInteractEvent.Invoke();
        }
    }
}
