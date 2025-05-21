using UnityEngine;
using UnityEngine.Video;

public class CambiarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public GameObject previousButton; // Asigna aquí tu botón 

    private int currentVideoIndex = 0;

    void Start()
    {
        UpdatePreviousButton(); // Asegúrate que esté en el estado correcto al iniciar
        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }
    }

    public void NextVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex++;
        if (currentVideoIndex >= videoClips.Length)
            currentVideoIndex = videoClips.Length - 1; // No pasar del último

        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        UpdatePreviousButton();
    }

    public void PreviousVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex--;
        if (currentVideoIndex < 0)
            currentVideoIndex = 0; // No bajar más

        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        UpdatePreviousButton();
    }

    // Activa o desactiva el botón de "anterior" según el índice actual
    private void UpdatePreviousButton()
    {
        if (previousButton != null)
            previousButton.SetActive(currentVideoIndex > 0);
    }
}
