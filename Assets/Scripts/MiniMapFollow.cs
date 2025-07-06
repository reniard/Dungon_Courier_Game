using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform target; // drag player ke sini di Inspector

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position;
            newPosition.z = transform.position.z; // biar kamera tetap di atas
            transform.position = newPosition;
        }
    }
}
