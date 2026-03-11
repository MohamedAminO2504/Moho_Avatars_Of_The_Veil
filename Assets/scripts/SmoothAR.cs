/*using UnityEngine;

public class SmoothAR : MonoBehaviour
{
    public Transform target;
    public float positionSmooth = 10f;
    public float rotationSmooth = 10f;

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.deltaTime * positionSmooth
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            Time.deltaTime * rotationSmooth
        );
    }
}/*/