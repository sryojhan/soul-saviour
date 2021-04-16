using UnityEngine;

public class DisableOnExit : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
