using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private CharacterMovement characterMovement;
    private float rotationSpeed;

    [SerializeField] float rotationLerpSpeed;

    #region Properties

    public CharacterMovement getCharacterMovement
    {
        get { return characterMovement; }
        private set { characterMovement = value; }
    }

    #endregion

    private void Start()
    {
        characterMovement = GetComponentInParent<CharacterMovement>();
    }

    private void Update()
    {
        rotationSpeed = Mathf.Lerp(rotationSpeed, characterMovement.getCurrentAngularSpeed, rotationLerpSpeed * Time.deltaTime);
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
    }

}
