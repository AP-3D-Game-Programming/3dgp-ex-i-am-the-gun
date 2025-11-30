using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource musicSource;

    void Awake()
    {
        LoadVolume();
        ApplyVolume();
        AddSliderListener();
    }

    void Start()
    {
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }
    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    private void ApplyVolume()
    {
        if (musicSource != null)
            musicSource.volume = volumeSlider.value;
    }
    public void ChangeVolume(float newVolume)
    {
        volumeSlider.value = newVolume;
        ApplyVolume();
        SaveVolume();
    }
    private void AddSliderListener()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }
    private void OnVolumeChanged(float value)
    {
        ChangeVolume(value);
    }
}
