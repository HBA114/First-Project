using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector3 rotation;

    private void Update()
    {
        transform.Rotate(rotation);
    }
}
