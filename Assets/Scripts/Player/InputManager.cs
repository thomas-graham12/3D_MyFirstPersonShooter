using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// Holds all the Player's actions that are strictly tied to how the player moves.
    /// </summary>

    PlayerControls _playerControls;
    PlayerControls.PlayerActions _playerActions;
    PlayerMovement _playerMovement;
    PlayerLook _playerLook;

    // Start is called before the first frame update
    void Awake()
    {
        _playerControls = new PlayerControls(); 
        _playerActions = _playerControls.Player;

        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();

        _playerActions.Crouch.performed += ctx => _playerMovement.Crouch();

        _playerActions.SprintStart.performed += ctx => _playerMovement.SprintPressed();
        _playerActions.SprintFinish.performed += ctx => _playerMovement.SprintReleased();

    }

    void FixedUpdate() // Used for player movement
    {
        // tell the playermovement to move using the value from our movement action.
        _playerMovement.ProcessMove(_playerActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate() // Used for camera movement
    {
        _playerLook.ProcessLook(_playerActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _playerActions.Enable(); // Lets us use the playerActions mappings
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
