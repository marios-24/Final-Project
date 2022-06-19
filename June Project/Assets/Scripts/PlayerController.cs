using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCam = null;
    [SerializeField] float mouseSens = 4f;
    [SerializeField] float speed = 7f;
    [SerializeField] float garvity = -13f;
    [SerializeField,Range (0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField,Range (0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float camPitch = 0f;
    float velocityY = 0f;
    CharacterController cc = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        camPitch -= targetMouseDelta.y * mouseSens;
        camPitch = Mathf.Clamp(camPitch, -90f, 90f);

        playerCam.localEulerAngles = Vector3.right * camPitch;
        transform.Rotate(Vector3.up * targetMouseDelta.x * mouseSens);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(cc.isGrounded)
            velocityY = 0f;

        velocityY += garvity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        cc.Move(velocity * Time.deltaTime);
    }
}
