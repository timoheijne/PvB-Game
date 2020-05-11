using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterCustomization : MonoBehaviour
{
    public static CharacterCustomization instance;

    private Renderer _renderer;

    [SerializeField]
    private bool _isCreatingPlayer = false;

    [SerializeField]
    private GameObject[] _hairStyles;
    private GameObject _currentHairStyle;
    private GameObject _playerEyes;

    [SerializeField]
    private Material[] _materials;

    private int _hairstyle;

    [SerializeField]
    private Slider[] _customizationSliders;

    [Range(0, 1)]
    private float _skinColorGradient = 0.2f;
    [Range(0, 1)]
    private float _hairColorGradient = 0.2f;
    [Range(0, 1)]
    private float _eyeColorGradient = 0.2f;

    private int _oldHairColorValue;
    private int _oldSkinColorValue;
    private int _oldEyeColorValue;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("PVB/ColorGrading");

        _playerEyes = GameObject.Find("PlayerEyes");

        for (int i = 0; i < _hairStyles.Length; i++)
        {
            _hairStyles[i].SetActive(false);
        }

        SetHairStyle(Mathf.RoundToInt(Random.Range(0,_hairStyles.Length)));
        _skinColorGradient = Random.value;
        _hairColorGradient = Random.value;
        _eyeColorGradient = Random.value;

        _customizationSliders[0].value = System.Array.IndexOf(_hairStyles, _currentHairStyle);
        _customizationSliders[1].value = _hairColorGradient;
        _customizationSliders[2].value = _skinColorGradient;
        _customizationSliders[3].value = _eyeColorGradient;
    }

    private void Update()
    {
        if (_oldSkinColorValue != Mathf.Round(_skinColorGradient * 100f) / 100f)
        {
            SetColor(gameObject, "Skin", _skinColorGradient);
        }

        if (_oldHairColorValue != Mathf.Round(_hairColorGradient * 100f) / 100f)
        {
            SetColor(_currentHairStyle, "Hair", _hairColorGradient);
        }

        if (_oldEyeColorValue != Mathf.Round(_hairColorGradient * 100f) / 100f) 
        {
            SetColor(_playerEyes, "Eyes", _eyeColorGradient);
        }
    }

    public void SkinColorControl(float value)
    {
        _skinColorGradient = value;
        _oldSkinColorValue = (int)(Mathf.Round(_skinColorGradient * 100f) / 100f);
    }

    public void HairStyleControl(float value)
    {
        _hairstyle = Mathf.RoundToInt(value);
        SetHairStyle(_hairstyle);
    }

    public void HairColorControl(float value)
    {
        _hairColorGradient = value;
        _oldHairColorValue = (int)(Mathf.Round(_hairColorGradient * 100f) / 100f);
    }

    public void EyeColorControl(float value) 
    {
        _eyeColorGradient = value;
        _oldEyeColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
    }

    public void SetHairStyle(int value) 
    {
        for (int i = 0; i < _hairStyles.Length; i++)
        {
            _hairStyles[i].SetActive(false);
        }
        _hairStyles[(int)value].SetActive(true);
        _currentHairStyle = _hairStyles[value];
    }

    public void SetColor(GameObject target, string feature, float value) 
    {
        Debug.Log("Setting Color of target: " + target.name + ", Body Feature: " + feature + ", To Value: " + value);
        Debug.Log("Creating Character: " + _isCreatingPlayer);

        Renderer _targetRenderer;

        _targetRenderer = target.GetComponent<Renderer>();

        if (_isCreatingPlayer) 
        {
            Debug.Log("Creating Character: " + _isCreatingPlayer);
            Slider _featureColorSlider = null;
            Image _sliderHandle;

            foreach (Slider _slider in _customizationSliders) 
            {
                if (_slider.name == "Slider_" + feature + "_Color") 
                {
                    _featureColorSlider = _slider;
                }
            }

            foreach (Material _material in _materials) 
            {
                if (_material.name == feature) 
                {
                    _sliderHandle.material = _material;
                }
            }
            _sliderHandle = _featureColorSlider.transform.Find("Handle").gameObject.GetComponent<Image>();
        }

        _targetRenderer.sharedMaterial.SetFloat("_SamplePos", value);
    }
}
