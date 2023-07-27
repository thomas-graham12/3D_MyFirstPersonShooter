using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    /// <summary>
    /// Grabs the slider, then makes the slider the health of the player divided by the maxHealth (gives a 0-1 value that the slider can use). 
    /// </summary>

    [SerializeField] PlayerManager _playerManager;
    Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = (float)_playerManager.health / (float)_playerManager.MaxHealth;

        _slider.value = sliderValue;
    }
}
