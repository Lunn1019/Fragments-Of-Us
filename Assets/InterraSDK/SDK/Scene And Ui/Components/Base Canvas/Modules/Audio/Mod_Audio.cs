using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InTerra.SceneAndUiSDK
{
    public static partial class Comp_BaseCanvas
    {
        public static partial class Mod_Audio
        {
            #region APIs
            public static void Func_RegisterAudioSource(AudioSource audioSource, Slider slider, float maxVolumeMultiplier)
            {
                AudioTypeDictionary[audioSource] = (slider, maxVolumeMultiplier);
            }

            public static void Func_SetVolumeFromSlider(AudioSource audioSource)
            {
                if(AudioTypeDictionary.TryGetValue(audioSource, out (Slider, float) audioAtt))
                {
                    (Slider slider, float maxVolume) = audioAtt;

                    float volume = slider.value;
                    audioSource.volume = Static_AudioAttributes.masterVolume * maxVolume * volume;
                    return;
                }
                
                Debug.LogWarning($"AudioSource {audioSource.name} is not registered. Please register it first using Func_RegisterAudioSource.");
            }

            public static void Func_SetSliderFromVolume(Slider slider, AudioSource audioSource)
            {
                float value = audioSource.volume;
                slider.value = value;
            }

            public static void Func_SetMasterVolumeFromSlider(Slider masterSlider)
            {
                Static_AudioAttributes.masterVolume = masterSlider.value;

                foreach (AudioSource audioSource in AudioTypeDictionary.Keys)
                {
                    (Slider slider, float maxVolume) = AudioTypeDictionary[audioSource];
                    audioSource.volume = Static_AudioAttributes.masterVolume * slider.value * maxVolume;
                }
            }

            public static void Func_RemoveRegistered(AudioSource audioSource)
            {
                NotDestroyedList.Remove(audioSource);
                AudioTypeDictionary.Remove(audioSource);
            }

            public static void Func_ClearAllRegistered()
            {
                NotDestroyedList.Clear();
                AudioTypeDictionary.Clear();
            }

            public static void Func_ClearAllRegisteredExceptDontDestroy()
            {
                List<AudioSource> keyList = new List<AudioSource>();
                List<(Slider, float)> valueList = new List<(Slider, float)>();

                for (int i = (NotDestroyedList.Count - 1); i >= 0; i--)
                {
                    AudioSource audioSource = NotDestroyedList[i];

                    if (audioSource == null)
                    {
                        NotDestroyedList.Remove(audioSource);
                        continue;
                    }

                    keyList.Add(audioSource);
                    valueList.Add(AudioTypeDictionary[audioSource]);
                }

                AudioTypeDictionary.Clear();

                for (int i = 0; i < keyList.Count; i++)
                {
                    AudioTypeDictionary[keyList[i]] = valueList[i];
                }
            }

            public static void Func_DebugCentralController()
            {
                foreach (KeyValuePair<AudioSource, (Slider, float)> pair in AudioTypeDictionary)
                {
                    AudioSource key = pair.Key;
                    (Slider slider, float volume) value = pair.Value;

                    Debug.Log($"[MenuDebug] AudioSource: {key.name}\n" +
                              $"Slider: {value.slider}\n" +
                              $"Volume: {value.volume}\n");
                }
            }

            public static void Func_DebugInstance(AudioSource key)
            {
                if (AudioTypeDictionary.TryGetValue(key, out (Slider slider, float volume) value))
                {
                    Debug.Log($"[MenuDebug] AudioSource: {key.name}\n" +
                              $"Slider: {value.slider}\n" +
                              $"Volume: {value.volume}\n");

                    return;
                }

                Debug.Log($"[MenuDebug] AudioSource: {key.name} is not registered");
            }
            #endregion
        }
    }
}