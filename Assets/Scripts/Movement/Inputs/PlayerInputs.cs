using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : CharacterInputs
{
    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        #region Movement Inputs

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalInput, 0f, verticalInput);

        movingDirection = direction.normalized;

        #endregion

        isUsingPowerup = Input.GetButton("Fire1");
        isBoostingRotation = Input.GetButton("Fire2");
    }
}
