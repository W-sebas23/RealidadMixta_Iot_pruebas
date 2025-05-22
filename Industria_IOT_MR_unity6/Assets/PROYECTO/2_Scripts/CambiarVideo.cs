using UnityEngine;
using UnityEngine.Video;

public class CambiarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject cerrarIzqBoton;
    public GameObject cerrarDerBoton;
    public GameObject tutorialPanel;

    public AudioSource buttonAudioSource;

    private int currentVideoIndex = 0;

    void Start()
    {
        UpdateButtons();
        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        ReiniciarTutorial();
    }

    public void ReiniciarTutorial()
    {
        currentVideoIndex = 0;

        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        UpdateButtons();

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    public void NextVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex++;
        if (currentVideoIndex >= videoClips.Length)
            currentVideoIndex = videoClips.Length - 1;

        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        UpdateButtons();

        PlayButtonSound();
    }

    public void PreviousVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex--;
        if (currentVideoIndex < 0)
            currentVideoIndex = 0;

        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
        UpdateButtons();

        PlayButtonSound();
    }

    public void CerrarTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        PlayButtonSound();
    }

    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonAudioSource.clip != null)
            buttonAudioSource.PlayOneShot(buttonAudioSource.clip);
    }

    private void UpdateButtons()
    {
        if (previousButton != null)
            previousButton.SetActive(currentVideoIndex > 0);
        if (nextButton != null)
            nextButton.SetActive(currentVideoIndex < videoClips.Length - 1);

        if (cerrarIzqBoton != null)
            cerrarIzqBoton.SetActive(currentVideoIndex == 0); // Primer video

        if (cerrarDerBoton != null)
            cerrarDerBoton.SetActive(currentVideoIndex == videoClips.Length - 1); // Último video
    }
}
