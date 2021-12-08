using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] Joystick moveStick;
    
    Transform cam;

    public PhotonView view;

    float jHorizontal = 0f;
    float jVertical = 0f;
    Vector3 direction;
    Vector3 moveDir;

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            JoystickMove();
        }
    }

    void JoystickMove()
    {
        jHorizontal = moveStick.Horizontal;
        jVertical = moveStick.Vertical;
        direction = new Vector3(jHorizontal, 0f, jVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.GetComponentInChildren<Camera>().gameObject.transform.eulerAngles.y;
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(moveDir);
            transform.position += (moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick tempJoystick = camera.GetComponentInChildren<Joystick>();

        moveStick = tempJoystick;

        cam = camera.transform;
        //cam = camera.GetComponentInChildren<Camera>().gameObject.transform;

        cam.GetComponentInChildren<CinemachineFreeLook>().Follow = transform;
        cam.GetComponentInChildren<CinemachineFreeLook>().LookAt = GameObject.Find("Ribs").transform;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
}
