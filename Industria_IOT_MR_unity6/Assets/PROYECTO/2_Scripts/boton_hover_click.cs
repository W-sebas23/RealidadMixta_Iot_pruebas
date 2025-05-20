using UnityEngine;

public class boton_hover_click : MonoBehaviour
{
    private AudioSource sonido;

    [SerializeField] private AudioClip clickAudio;
    [SerializeField] private AudioClip hoverAudio;

    void Start()
    {
        sonido = GetComponent<AudioSource>();
        if (sonido == null)
        {
            Debug.LogWarning("No AudioSource component found on this GameObject!");
        }
    }

    public void clickAudioOn()
    {
        if (clickAudio != null && sonido != null)
            sonido.PlayOneShot(clickAudio);
        else
            Debug.LogWarning("clickAudio or sonido is missing!");
    }

    public void hoverAudioOn()
    {
        if (hoverAudio != null && sonido != null)
            sonido.PlayOneShot(hoverAudio);
        else
            Debug.LogWarning("hoverAudio or sonido is missing!");
    }
}
