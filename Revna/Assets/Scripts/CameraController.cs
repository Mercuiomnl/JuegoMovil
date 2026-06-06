using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform tarjet;
    [SerializeField] private float _cameraVel = 0.025f;
    [SerializeField] private Vector3 _movement;

    private void LateUpdate()
    {
        Vector3 newPosition = tarjet.position + _movement;

        Vector3 softPosition = Vector3.Lerp(transform.position, newPosition, _cameraVel);

        transform.position = newPosition;
    }
}
