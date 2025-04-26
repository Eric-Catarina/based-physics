using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUp currentPowerUp;

    // Update is called once per frame
    void Update()
    {
        if (currentPowerUp != null && Input.GetMouseButtonDown(0))
        {
            currentPowerUp.Activate();
        }
    }
}
