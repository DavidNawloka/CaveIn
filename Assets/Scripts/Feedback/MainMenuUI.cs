using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace CaveIn.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] AudioMixer rocksAudioMixer;
        [SerializeField] AudioMixer playerAudioMixer;
        [SerializeField] TMP_Dropdown qualityDropdown;
        private void Start()
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
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
