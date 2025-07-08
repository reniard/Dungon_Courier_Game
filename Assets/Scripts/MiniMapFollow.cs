using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform target;
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}
