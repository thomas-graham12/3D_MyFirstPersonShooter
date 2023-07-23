using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public bool playerInRange;

    [SerializeField] PlayerInput PlayerInput;

    [SerializeField] UnityEvent onInteractEvent;

    private void OnEnable()
    {
        if (PlayerInput != null)
        {
            InputAction interactKeyPressed = PlayerInput.actions["Interact"];

            interactKeyPressed.started += OnInteract;
        }
    }    
    
    private void OnDisable()
    {
        if (PlayerInput != null)
        {
            InputAction interactKeyPressed = PlayerInput.actions["Interact"];

            interactKeyPressed.started -= OnInteract;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out PlayerInput playerInput);
        if(playerInput)
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
        if (playerInRange)
        {
            onInteractEvent.Invoke();
        }
    }
}
