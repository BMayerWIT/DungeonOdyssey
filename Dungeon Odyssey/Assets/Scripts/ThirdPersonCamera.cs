using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;

    private GameObject gameInputObject;
    private GameInput gameInput;

    public static ThirdPersonCamera singleton;

    [SerializeField] private float lookSpeed = 0.1f;
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private float pivotSpeed = 0.3f;

    [SerializeField] private float defaultPosition;
    [SerializeField] private float lookAngle;
    [SerializeField] private float pivotAngle;
    [SerializeField] private float minimumPivotAngle = -35;
    [SerializeField] private float maximumPivotAngle = 35;

    [SerializeField] private float lookSensitivity = 10f;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        gameInputObject = GameObject.Find("GameInput");
        gameInput = gameInputObject.GetComponent<GameInput>();
    }

    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        
        
    }
    private void Update()
    {
        mouseX = gameInput.GetMouseX() * lookSensitivity;
        mouseY = gameInput.GetMouseY() * lookSensitivity;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        FollowTarget(delta);
        HandleCameraRotation(delta, mouseX, mouseY);
        
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        myTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
}
