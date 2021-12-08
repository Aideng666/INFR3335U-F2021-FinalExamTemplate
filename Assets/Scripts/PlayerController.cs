using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] Joystick moveStick;
    [SerializeField] Transform cam;

    float jHorizontal = 0f;
    float jVertical = 0f;
    Vector3 direction;
    Vector3 moveDir;

    // Update is called once per frame
    void Update()
    {
        JoystickMove();
    }

    void JoystickMove()
    {
        jHorizontal = moveStick.Horizontal;
        jVertical = moveStick.Vertical;
        direction = new Vector3(jHorizontal, 0f, jVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.position += (moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
}
