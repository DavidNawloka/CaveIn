using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using CaveIn.Core;
using UnityEngine.UI;

namespace CaveIn.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] Slider sensitivitySlider;
        [SerializeField] AudioMixer rocksAudioMixer;
        [SerializeField] Slider rocksAudioSlider;
        [SerializeField] AudioMixer playerAudioMixer;
        [SerializeField] Slider playerAudioSlider;
        [SerializeField] AudioMixer musicAudioMixer;
        [SerializeField] Slider musicAudioSlider;
        [SerializeField] TMP_Dropdown qualityDropdown;
        [SerializeField] GameObject[] levelButtons;

        ProgressTracker progressTracker;
        private void Start()
        {
            float volume;
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();

            rocksAudioMixer.GetFloat("masterVolume",out volume);
            rocksAudioSlider.value = volume;
            playerAudioMixer.GetFloat("masterVolume", out volume);
            playerAudioSlider.value = volume;
            musicAudioMixer.GetFloat("masterVolume", out volume);
            musicAudioSlider.value = volume;

            progressTracker = FindObjectOfType<ProgressTracker>();
            progressTracker.UpdateProgress(levelButtons);

            sensitivitySlider.value = progressTracker.sensitivity;
            
        }
        public void SetRocksVolume(float volume)
        {
            rocksAudioMixer.SetFloat("masterVolume", volume);
            if (volume == -30f)
            {
                rocksAudioMixer.SetFloat("masterVolume", -80f);
            }
        }
        public void SetPlayerVolume(float volume)
        {
            
            playerAudioMixer.SetFloat("masterVolume", volume);
            if (volume == -30f)
            {
                playerAudioMixer.SetFloat("masterVolume", -80f);
            }
        }
        public void SetMusicVolume(float volume)
        {

            musicAudioMixer.SetFloat("masterVolume", volume);
            if (volume == -30f)
            {
                musicAudioMixer.SetFloat("masterVolume", -80f);
            }
        }
        public void SetSensitivity(float sensitivity)
        {
            progressTracker.sensitivity = sensitivity;
        }
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}
