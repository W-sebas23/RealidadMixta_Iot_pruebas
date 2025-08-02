using UnityEngine;

public class SeguimientoPanel : MonoBehaviour
{
    public Transform targetCamera;
    public float distanceFromCamera = 2f;
    public float followSpeed = 5f; // Suavidad del seguimiento
    public float rotationSpeed = 5f;


    void LateUpdate()
    {
        if (targetCamera == null)
            return;

        // Posición deseada frente a la cámara
        Vector3 desiredPosition = targetCamera.position + targetCamera.forward * distanceFromCamera;

        // Interpolación suave hacia la nueva posición
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // Hacer que el panel mire hacia la cámara suavemente (sin inclinarse)
        Quaternion desiredRotation = Quaternion.LookRotation(transform.position - targetCamera.position);
        Quaternion horizontalRotation = Quaternion.Euler(0, desiredRotation.eulerAngles.y, 0); // Solo rotación Y
        transform.rotation = Quaternion.Slerp(transform.rotation, horizontalRotation, Time.deltaTime * rotationSpeed);
    }
}
