using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring settings")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    void Update()
    {
        // Calculate movement velocity as a 3D vector
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        // Final movement vector
        Vector3 _velocity = (_movementHorizontal + _movementVertical).normalized * speed;

        motor.Move(_velocity);

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        float _xRotation = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRotation * lookSensitivity;

        motor.RotateCamera(_cameraRotationX);

        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump")) {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        } else {
            SetJointSettings(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings (float _jointSpring) {
        joint.yDrive = new JointDrive{
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
