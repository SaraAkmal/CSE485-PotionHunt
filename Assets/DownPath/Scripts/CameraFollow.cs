using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    // How long the object should shake for.
    public float shakeDuration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;
    public float offsetY;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;
    private Vector3 newPosition;
    private float orginalYPos;

    private Vector3 originalPos;

    private void Awake()
    {
        orginalYPos = transform.position.y;
        Cursor.visible = false;
        if (camTransform == null) camTransform = GetComponent(typeof(Transform)) as Transform;
    }

    private void Update()
    {
        newPosition.x = Target.position.x;
        newPosition.y = (offsetY) + Target.position.y;
        //newPosition.y = Target.position.y;
        newPosition.z = -10;

        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void ShakeCamera()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 0.2f;
    }
}