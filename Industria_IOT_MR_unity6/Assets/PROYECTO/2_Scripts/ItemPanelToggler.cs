using UnityEngine;

public class ItemPanelToggler : MonoBehaviour
{
    public GameObject targetPanel;
    [Header("Posicionamiento al ser Visible")]
    // ¡IMPORTANTE! Arrastra aquí el componente Transform de la cámara que representa la cabeza del usuario en VR
    public Transform userHeadCamera;
    public float spawnDistance = 2.0f;
    public Vector3 positionOffset = Vector3.zero;

    // Esta función será llamada por el evento On Value Changed (Boolean) del Toggle en ESTE MISMO GameObject
    public void TogglePanel(bool isVisible)
    {

        // Verifica si el panel objetivo está asignado y actívalo/desactívalo
        if (targetPanel != null)
        {
            targetPanel.SetActive(isVisible);

            if (isVisible)
            {
                if (userHeadCamera == null)
                {
                    Debug.LogWarning("ItemPanelToggler en " + gameObject.name + ": ¡El Transform de la cámara/cabeza del usuario no está asignado para posicionamiento!");
                    return;
                }

                Vector3 targetPosition = userHeadCamera.position;
                targetPosition += userHeadCamera.forward * spawnDistance;
                targetPosition += userHeadCamera.TransformDirection(positionOffset);

                // Establece la posición del panel
                targetPanel.transform.position = targetPosition;

                targetPanel.transform.LookAt(userHeadCamera.position);
                targetPanel.transform.Rotate(0, 180, 0, Space.Self);
            }
        }
        else
        {
            // Muestra una advertencia si el panel objetivo no está asignado para este ítem
            Debug.LogWarning("ItemPanelToggler en " + gameObject.name + ": ¡Panel objetivo no asignado!");
        }
    }
}
