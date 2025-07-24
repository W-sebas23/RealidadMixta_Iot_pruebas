using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class CambiarVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public TextMeshProUGUI numeroText;
    public TextMeshProUGUI descripcionText;
    public string[] descripciones;

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
        UpdateUI();
        ReiniciarTutorial();
    }

    private void UpdateUI()
    {
        if (numeroText != null)
            numeroText.text = (currentVideoIndex + 1).ToString() + "/" + videoClips.Length;

        if (descripcionText != null && descripciones.Length > currentVideoIndex)
            descripcionText.text = descripciones[currentVideoIndex];
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
        UpdateUI();

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
        UpdateUI();

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
        UpdateUI();

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

        //if (cerrarIzqBoton != null)
        //    cerrarIzqBoton.SetActive(currentVideoIndex == 0); // Primer video

        if (cerrarDerBoton != null)
            cerrarDerBoton.SetActive(currentVideoIndex == videoClips.Length - 1); // �ltimo video
    }
}
