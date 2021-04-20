using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX * _speed, 0.0f, inputY * _speed);

        if (inputX != 0 || inputY != 0)
            transform.Translate(movement * _speed * Time.fixedDeltaTime);
    }
}
