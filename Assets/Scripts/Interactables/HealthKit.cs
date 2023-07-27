using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    /// <summary>
    /// Script to attach onto a UnityEvent. Grabs players health and adds the health ammount to it
    /// </summary>
 
    [SerializeField] PlayerManager _playerManager;
    public int healthAmount;

    public void HealPlayer()
    {
        _playerManager.health += healthAmount;
        Destroy(gameObject);
    }
}
