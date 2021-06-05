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
        [SerializeField] AudioMixer rocksAudioMixer;
        [SerializeField] Slider rocksAudioSlider;
        [SerializeField] AudioMixer playerAudioMixer;
        [SerializeField] Slider playerAudioSlider;
        [SerializeField] TMP_Dropdown qualityDropdown;
        [SerializeField] GameObject[] levelButtons;

        private void Start()
        {
            float volume;
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();

            rocksAudioMixer.GetFloat("masterVolume",out volume);
            rocksAudioSlider.value = volume;
            playerAudioMixer.GetFloat("masterVolume", out volume);
            playerAudioSlider.value = volume;

            FindObjectOfType<ProgressTracker>().UpdateProgress(levelButtons);
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
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
    }
}
