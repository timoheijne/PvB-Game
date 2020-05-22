using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HairCustomization : MonoBehaviour 
{
    public static HairCustomization instance;

    [SerializeField]
    private GameObject[] _hairStyles;
    private GameObject _currentHairStyle;

    [SerializeField]
    private Slider _hairColorSlider, _hairStyleSlider;
    [SerializeField]
    private Image _sliderHandle;

    private void Start()
    {
        SetHairStyle(_hairStyleSlider.value);
        SetHairColor(_hairColorSlider.value);
    }

    public void SetHairStyle(float _sliderValue) 
    {
        for (int i = 0; i < _hairStyles.Length; i++) 
        {
            _hairStyles[i].SetActive(false);
        }

        _hairStyles[(int)_sliderValue].SetActive(true);
        _currentHairStyle = _hairStyles[(int)_sliderValue];
        SetHairColor(_hairColorSlider.value);
    }

    public void SetHairColor(float _sliderValue) 
    {
        Image _targetImage;
        if (_currentHairStyle.name != "Bald") 
        {
            Renderer _targetRenderer = _currentHairStyle.GetComponent<Renderer>();
            _targetRenderer.sharedMaterial.SetFloat("_samplePos", _sliderValue);
        }

        _targetImage = _sliderHandle;
        _targetImage.material.SetFloat("Bool_IsRainbow", 1);
        _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_sliderValue, 0f));
    }

}