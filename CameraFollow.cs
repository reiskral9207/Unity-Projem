using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float followLerp = 10f;
    public float rotateLerp = 12f;

   void LateUpdate()
{
    if (!target) return;


    Vector3 desiredPos = target.position + offset; 

    transform.position = Vector3.Lerp(transform.position, desiredPos, followLerp * Time.deltaTime);
 
}

}