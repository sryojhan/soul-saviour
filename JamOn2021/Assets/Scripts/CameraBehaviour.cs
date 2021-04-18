using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    Vector3 cameraPosition;
    Vector3 vel = Vector3.zero;
    void Start()
    {
        setCameraPosition();
    }

    void Update()
    {
        setCameraPosition();
    }
    void setCameraPosition()
    {
        cameraPosition = Vector3.SmoothDamp(transform.position, playerTr.position, ref vel, 0.3f);
        //cameraPosition = playerTr.position;
        cameraPosition.z = transform.position.z;
        transform.position = cameraPosition;
    }
}
