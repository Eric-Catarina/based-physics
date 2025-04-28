using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public abstract class CharacterInputs : MonoBehaviour
{
    protected Vector3 movingDirection = new Vector3(0,0,0);
    protected bool isUsingPowerup = false;
    protected bool isBoostingRotation = false;

    #region Properties

    public Vector3 getMovingDirection
    {
        get { return movingDirection; }
        set { movingDirection = value; }
    }

    public bool getIsUsingPowerup
    { 
        get { return isUsingPowerup; }
        set { isUsingPowerup = value; }
    }

    public bool getIsBoostingRotation
    {
        get { return isBoostingRotation; }
        set { isBoostingRotation = value; }
    }

    #endregion
}
