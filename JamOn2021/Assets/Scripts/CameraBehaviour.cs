using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    Vector3 cameraPosition;
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
        cameraPosition = playerTr.position;
        cameraPosition.z = transform.position.z;
        transform.position = cameraPosition;
    }
}
