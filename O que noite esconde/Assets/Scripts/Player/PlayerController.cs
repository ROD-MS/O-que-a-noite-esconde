using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    public List<GameObject> inventory;

    public float speed = 5f;
    public Camera camera;

    public float mouseSensibility = 20f;

    private float xRotation;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float deltaSpeed = speed * Time.deltaTime;

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 movePlayer = new Vector3(xMove * deltaSpeed, 0, zMove * deltaSpeed);

        transform.Translate(movePlayer);

        lockUnlockMouse();
    }

    void LateUpdate()
    {

        float mouseX = Input.mousePositionDelta.x * Time.deltaTime * mouseSensibility;
        float mouseY = Input.mousePositionDelta.y * Time.deltaTime * mouseSensibility;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

    }

    void lockUnlockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void addItemInventory(GameObject new_item)
    {
        inventory.Add(new_item);
        new_item.SetActive(false);
    }

    public void subItemInventory(GameObject sub_item)
    {
        inventory.Remove(sub_item);
    }
}
