using UnityEngine;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.Hands
{
    /// <summary>
    /// Toggles the active state of a GameObject and positions/rotates it when activated.
    /// </summary>
    public class ToggleGameObject : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("El GameObject al que se le alternará el estado activo.")]
        GameObject m_ActivationGameObject;

        /// <summary>
        /// El GameObject al que se le alternará el estado activo.
        /// </summary>
        public GameObject activationGameObject
        {
            get => m_ActivationGameObject;
            set => m_ActivationGameObject = value;
        }

        [SerializeField]
        [Tooltip("Indica si el GameObject está actualmente activo.")]
        bool m_CurrentlyActive;

        /// <summary>
        /// Indica si el GameObject está actualmente activo.
        /// </summary>
        public bool currentlyActive
        {
            get => m_CurrentlyActive;
            set
            {
                m_CurrentlyActive = value;
                // Llama al método interno que maneja la activación y posicionamiento
                SetActivationState(m_CurrentlyActive);
            }
        }

        [Header("Posicionamiento al ser Visible")]
        [Tooltip("Arrastra aquí el componente Transform de la cámara que representa la cabeza del usuario en VR.")]
        public Transform userHeadCamera;
        [Tooltip("Distancia en metros desde la cámara a la que aparecerá el objeto.")]
        public float spawnDistance = 2.0f; 
        [Tooltip("Desfase opcional respecto al frente de la cámara (ej: Vector3.up * 0.2 para que aparezca un poco más arriba).")]
        public Vector3 positionOffset = Vector3.zero;

        void Awake()
        {
            // Asegura que el objeto objetivo esté en el estado inicial correcto al iniciar la escena
            if (m_ActivationGameObject != null)
            {
                m_ActivationGameObject.SetActive(m_CurrentlyActive);
            }
        }

        public void ToggleActiveState()
        {
            m_CurrentlyActive = !m_CurrentlyActive;
            SetActivationState(m_CurrentlyActive);
        }

        // Método interno para establecer el estado, posicionar y rotar el GameObject
        void SetActivationState(bool isVisible)
        {
            if (activationGameObject != null)
            {
                activationGameObject.SetActive(isVisible);

                // Si el GameObject se está haciendo visible, establece su posición y rotación
                if (isVisible)
                {
                    if (userHeadCamera == null)
                    {
                        Debug.LogWarning("ToggleGameObject en " + gameObject.name + ": ¡El Transform de la cámara/cabeza del usuario no está asignado para posicionamiento!");
                    }
                    else
                    {
                        // --- Calcular la Posición ---
                        Vector3 targetPosition = userHeadCamera.position;
                        targetPosition += userHeadCamera.forward * spawnDistance;
                        targetPosition += userHeadCamera.TransformDirection(positionOffset);

                        // Establece la posición del panel
                        activationGameObject.transform.position = targetPosition;

                        // --- Calcular la Rotación (Horizontalmente hacia la cámara - Asumiendo que el frente visual es Z- local) ---
                        Vector3 directionToCameraHorizontal = userHeadCamera.position - activationGameObject.transform.position;
                        directionToCameraHorizontal.y = 0;

                        if (directionToCameraHorizontal.sqrMagnitude > 0.0001f) 
                        {
                            directionToCameraHorizontal.Normalize();
                            Quaternion lookRotation = Quaternion.LookRotation(-directionToCameraHorizontal);
                            activationGameObject.transform.rotation = lookRotation;
                        }
                        // Si la dirección horizontal es casi cero, la rotación horizontal no cambia.
                    }
                }
            }
            else
            {
                Debug.LogWarning("ToggleGameObject en " + gameObject.name + ": ¡Activation Game Object no asignado en el Inspector!");
            }
        }
    }
}