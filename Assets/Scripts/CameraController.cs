using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraController : MonoBehaviour
{
    Vector2 _axis = new Vector2(1,1);

    [Header("Camera Settings")]
    [SerializeField] Vector2 _sensitivity = new Vector2(1, 1);
    [SerializeField] float _topClamp = 90f;
    [SerializeField] float _bottomClamp = -90f;
    [SerializeField] float _fieldOfView = 70;

    [Header("Attachments")]
    //GameObject _cameraRoot;

    float _targetPitch = 0.0f;
    float _targetYaw = 0.0f;

    GameObject _mainCamera;
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _targetPitch = 0.0f;
        _targetYaw = transform.rotation.eulerAngles.y;
        _camera = _mainCamera.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        _camera.fieldOfView = _fieldOfView;
        _mainCamera.transform.parent = transform;
        _mainCamera.transform.localPosition = new Vector3(0, 1.5f, 0);
        _mainCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        _axis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        
            Pitch(); // Handles the rotations up and down.
            Yaw(); // Handles the rotations left and right.
        
    }

    private void Pitch()
    {
        _targetPitch -= _axis.y * _sensitivity.y;
        _targetPitch = Mathf.Clamp(_targetPitch, _bottomClamp, _topClamp);

        _mainCamera.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);
    }

    private void Yaw()
    {
        _targetYaw += _axis.x * _sensitivity.x;

        transform.rotation = Quaternion.Euler(0, _targetYaw, 0);
    }
}
