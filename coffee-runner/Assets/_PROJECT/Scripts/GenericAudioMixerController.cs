using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic; // Required for lists

[System.Serializable]
public class MixerParameter
{
    public Slider slider;
    public string exposedParameterName;
}

public class GenericAudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public List<MixerParameter> mixerParameters = new List<MixerParameter>();

    void Start()
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer not assigned!");
            return;
        }

        foreach (var parameter in mixerParameters)
        {
            if (parameter.slider == null)
            {
                Debug.LogWarning($"Slider for {parameter.exposedParameterName} not assigned!");
                continue;
            }

            float currentValue;
            if (audioMixer.GetFloat(parameter.exposedParameterName, out currentValue))
            {
                parameter.slider.value = Mathf.Pow(10, currentValue / 20f);
            }
            else
            {
                Debug.LogWarning($"Exposed parameter '{parameter.exposedParameterName}' not found in AudioMixer.");
            }

            parameter.slider.onValueChanged.AddListener((value) => OnSliderValueChanged(parameter.exposedParameterName, value));
        }
    }

    public void OnSliderValueChanged(string parameterName, float sliderValue)
    {
        float volumeDB = Mathf.Log10(sliderValue) * 20f;
        audioMixer.SetFloat(parameterName, volumeDB);
    }
}