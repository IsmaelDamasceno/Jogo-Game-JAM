using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private Vector2 sensibility;

    private Transform playerTrs;
    private Vector2 rotation;

    void Start()
    {
        playerTrs = transform.parent;
        rotation = new(0f, 0f);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensibility.x;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensibility.y;

        rotation += new Vector2(-mouseY, mouseX);
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotation.x, transform.localRotation.y, 0f);
        playerTrs.localRotation = Quaternion.Euler(playerTrs.localRotation.x, rotation.y, playerTrs.localRotation.z);
	}
}
