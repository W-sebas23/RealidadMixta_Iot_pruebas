using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Un XR grab transformer que permite bloquear ejes de rotación específicos
    /// manteniendo a la vez el comportamiento de escalado esférico del XRGeneralGrabTransformer.
    /// </summary>
    public class AgarreEsferico : XRGeneralGrabTransformer
    {
        [SerializeField]
        [Tooltip("Ejes de rotación permitidos al agarrar. Los ejes no seleccionados mantendrán su rotación inicial.")]
        XRGeneralGrabTransformer.ManipulationAxes m_PermittedRotationAxis = XRGeneralGrabTransformer.ManipulationAxes.All;

        Vector3 m_InitialEulerRotation;

        protected override RegistrationMode registrationMode => RegistrationMode.SingleAndMultiple;

        public override void OnLink(XRGrabInteractable grabInteractable)
        {
            base.OnLink(grabInteractable);
            // Guarda la rotación inicial al iniciar el agarre
            m_InitialEulerRotation = grabInteractable.transform.rotation.eulerAngles;
        }

        public override void Process(
            XRGrabInteractable grabInteractable,
            XRInteractionUpdateOrder.UpdatePhase updatePhase,
            ref Pose targetPose,
            ref Vector3 localScale)
        {
            // Primero aplica el comportamiento estándar (incluye escala y traslación)
            base.Process(grabInteractable, updatePhase, ref targetPose, ref localScale);

            // Luego ajusta la rotación para bloquear ejes no permitidos
            Vector3 newEuler = targetPose.rotation.eulerAngles;

            if ((m_PermittedRotationAxis & XRGeneralGrabTransformer.ManipulationAxes.X) == 0)
                newEuler.x = m_InitialEulerRotation.x;
            if ((m_PermittedRotationAxis & XRGeneralGrabTransformer.ManipulationAxes.Y) == 0)
                newEuler.y = m_InitialEulerRotation.y;
            if ((m_PermittedRotationAxis & XRGeneralGrabTransformer.ManipulationAxes.Z) == 0)
                newEuler.z = m_InitialEulerRotation.z;

            targetPose.rotation = Quaternion.Euler(newEuler);
        }
    }
}
