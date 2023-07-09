using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private float mainSpeed = 15f;
    [SerializeField] private float shiftSpeed = 40f;
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private float maxYAngle = 80f;

    private Vector2 _currentRotation;

    void Start() {
        transform.position = new Vector3(0, 4, -10);
        transform.eulerAngles = Vector3.zero;
    }

    void Update() {
        RotateCamera();
        MoveCamera();
    }

    private void RotateCamera() {
        _currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        _currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        _currentRotation.x = Mathf.Repeat(_currentRotation.x, 360);
        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -maxYAngle, maxYAngle);
        transform.rotation = Quaternion.Euler(_currentRotation.y, _currentRotation.x, 0);
    }

    private void MoveCamera() {
        float speed = Input.GetKey(KeyCode.LeftShift) ? shiftSpeed : mainSpeed;
        transform.Translate(GetBaseInput() * speed * Time.deltaTime);
        transform.position += GetHeightInput() * speed * Time.deltaTime;
    }

    private Vector3 GetBaseInput() {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            velocity += new Vector3(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.S)) {
            velocity += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.A)) {
            velocity += new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            velocity += new Vector3(1, 0, 0);
        }

        return velocity;
    }

    private Vector3 GetHeightInput() {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.E)) {
            velocity += new Vector3(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.Q)) {
            velocity += new Vector3(0, -1, 0);
        }

        return velocity;
    }
}