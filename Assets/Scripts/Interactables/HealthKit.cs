using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    PlayerManager _playerManager;
    public int healthAmount;

    public void HealPlayer()
    {
        _playerManager.health += healthAmount;
    }
}
