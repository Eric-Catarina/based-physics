using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 2;
    public float rotationSpeed = 270;
    public float additionalRotationSpeed = 180;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 diretion = new Vector3(horizontalInput, 0, verticalInput);
        diretion.Normalize();

        transform.Translate(diretion * speed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up * (rotationSpeed + additionalRotationSpeed * diretion.magnitude) * Time.deltaTime, Space.Self); // Aumenta a velocidade de rotação quando há input do jogador
    }
}
