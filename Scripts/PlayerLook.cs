using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 5.0f;
    [SerializeField] private float sensitivityY = 5.0f;
    private float mouseX;
    private float mouseY;
    private float multiplier = 2.0f;
    private float xRotation;
    private float yRotation;

    private bool cursorVisible;

    public bool whileTalking;

    [SerializeField] Transform cam;
    [SerializeField] Transform camOrientation;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        CursorDisabled();
        whileTalking = false;
    }

    private void Update()
    {
        if (whileTalking == false && !gameManager.isPaused)
        {
            LookInput();
        }
        else if (whileTalking == true && !gameManager.isPaused)
        {
            LookInputDuringDialogue();
        }
        else
        {
            return;
        }

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        camOrientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        if (cursorVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CursorEnabled();
        }

        if (!cursorVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CursorDisabled();
        }
    }

    void LookInput()
    {
        sensitivityX = 5.0f;
        sensitivityY = 5.0f;

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multiplier;
        xRotation -= mouseY * sensitivityY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    void LookInputDuringDialogue()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multiplier;
        xRotation -= mouseY * sensitivityY * multiplier;

        sensitivityX = 0.1f;
        sensitivityY = 0.2f;

        xRotation = Mathf.Clamp(xRotation, -5f, 5f);
    }

    void CursorDisabled()
    {
        cursorVisible = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CursorEnabled()
    {
        cursorVisible = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
