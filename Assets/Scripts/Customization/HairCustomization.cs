using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HairCustomization : MonoBehaviour 
{
    public bool IsCreatingCharacter;
    
    public GameObject[] HairStyles;
    public GameObject CurrentHairStyle;

    [SerializeField]
    private Slider _hairColorSlider,
                   _hairStyleSlider;

    [SerializeField]
    private Image _sliderHandle;

    private float _hairColorValue;

    private int _index;

    private void Start()
    {
        if (IsCreatingCharacter) 
        {
            _hairStyleSlider.value = Mathf.RoundToInt(Random.Range(0, 4));
            _index = (int)_hairStyleSlider.value;
            _hairColorSlider.value = Random.Range(0, 1);

            SetHairStyleSlider(_hairStyleSlider.value);
            SetHairColor(_hairColorSlider.value);
        }
    }

    public void SetHairStyleSlider(float _sliderValue) 
    {
        for (int i = 0; i < HairStyles.Length; i++) 
        {
            HairStyles[i].SetActive(false);
        }

        HairStyles[(int)_sliderValue].SetActive(true);
        CurrentHairStyle = HairStyles[(int)_sliderValue];
        SetHairColor(_hairColorSlider.value);
    }

    public void SetHairStyleButton(int _value) 
    {
        _index += _value;

        if (_index < 0) 
        {
            _index = HairStyles.Length - 1;
        }

        if (_index > HairStyles.Length - 1) 
        {
            _index = 0;
        }

        for (int i = 0; i < HairStyles.Length; i++) 
        {
            HairStyles[i].SetActive(false);
        }

        HairStyles[_index].SetActive(true);
        CurrentHairStyle = HairStyles[_index];
        SetHairColor(_hairColorSlider.value);
    }

    public void SetHairColor(float _sliderValue) 
    {
        Image _targetImage;

        _hairColorValue = _sliderValue;

        if (CurrentHairStyle.name != "Bald") 
        {
            Renderer _targetRenderer = CurrentHairStyle.GetComponent<Renderer>();
            _targetRenderer.sharedMaterial.SetFloat("_samplePos", _sliderValue);
        }

        _targetImage = _sliderHandle;
        _targetImage.material.SetFloat("Bool_IsRainbow", 1);
        _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_sliderValue, 0f));
    }

    public void SaveHair() 
    {
        PlayerPrefs.SetInt("hairstyle", _index);
        PlayerPrefs.SetFloat("haircolor", _hairColorValue);
        PlayerPrefs.Save();
    }
}