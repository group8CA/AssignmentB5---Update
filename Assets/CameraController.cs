using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;
	
	 public float sensitivity = 10f;
     public float maxYAngle = 80f;
     private Vector2 currentRotation;
     void Update()
     {
		         if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }
         currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
         currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
         currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
         currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
         Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);
        // if (Input.GetMouseButtonDown(0))
         //    Cursor.lockState = CursorLockMode.Locked;
     }

}