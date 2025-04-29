using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUp currentPowerUp;

    public Image powerUpIcon;

    // Update is called once per frame
    void Update()
    {
        if (currentPowerUp != null && Input.GetButtonDown("Fire1"))
        {
            currentPowerUp.Activate();
            currentPowerUp = null;
            powerUpIcon.enabled = false; // Desliga a visibilidade do icone de powerup
            powerUpIcon.sprite = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            currentPowerUp = other.GetComponent<PowerUp>();
            powerUpIcon.enabled = true; // Liga a visibilidade do icone de powerup
            powerUpIcon.sprite = currentPowerUp.icon;

            StartCoroutine(currentPowerUp.Collect());
        }
    }
}
