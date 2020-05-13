using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterCustomization : MonoBehaviour {
    public static CharacterCustomization instance;

    private Renderer _renderer;

    [SerializeField]
    private bool _isCreatingCharacter, _hasCharacter, _isNPC;

    [SerializeField]
    private GameObject _targetCharacter;

    [SerializeField]
    private GameObject[] _hairStyles;
    private GameObject _currentHairStyle;
    private GameObject _eyes;

    [SerializeField]
    private Material[] _materials;

    [SerializeField]
    private Image[] _sliderHandles;

    [SerializeField]
    private Slider[] _customizationSliders;

    [Range(0, 1)]
    private float _skinColorGradient,
                  _eyeColorGradient,
                  _hairColorGradient = 0.2f;

    private int _hairstyle;

    private int _oldHairColorValue,
                _oldSkinColorValue,
                _oldEyeColorValue;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("PVB/ColorGrading");

        _eyes = GameObject.Find("Eyes");

        if (!_hasCharacter && _isCreatingCharacter || _isNPC) 
        {
            for (int i = 0; i < _hairStyles.Length; i++) 
            {
                _hairStyles[i].SetActive(false);
            }

            SetHairStyle(Mathf.RoundToInt(Random.Range(0, _hairStyles.Length)));
            _skinColorGradient = Random.value;
            _hairColorGradient = Random.value;
            _eyeColorGradient = Random.value;

            _customizationSliders[0].value = System.Array.IndexOf(_hairStyles, _currentHairStyle);
            _customizationSliders[1].value = _hairColorGradient;
            _customizationSliders[2].value = _skinColorGradient;
            _customizationSliders[3].value = _eyeColorGradient;
        }
    }

    private void Update()
    {
        if (_oldSkinColorValue != Mathf.Round(_skinColorGradient * 100f) / 100f)
        {
            SetColor(_targetCharacter.transform.Find("Body").gameObject, "Skin", _skinColorGradient);
        }

        if (_oldHairColorValue != Mathf.Round(_hairColorGradient * 100f) / 100f)
        {
            if (!_currentHairStyle == _hairStyles[0]) 
            {
                SetColor(_currentHairStyle, "Hair", _hairColorGradient);
            }
        }

        if (_oldEyeColorValue != Mathf.Round(_hairColorGradient * 100f) / 100f) 
        {
            SetColor(_eyes, "Eyes", _eyeColorGradient);
        }
    }

    public void SkinColorControl(float _value)
    {
        _skinColorGradient = _value;
        _oldSkinColorValue = (int)(Mathf.Round(_skinColorGradient * 100f) / 100f);
    }

    public void HairStyleControl(float _value)
    {
        _hairstyle = Mathf.RoundToInt(_value);
        SetHairStyle(_hairstyle);
    }

    public void HairColorControl(float _value)
    {
        _hairColorGradient = _value;
        _oldHairColorValue = (int)(Mathf.Round(_hairColorGradient * 100f) / 100f);
    }

    public void EyeColorControl(float value) 
    {
        _eyeColorGradient = value;
        _oldEyeColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
    }

    public void SetHairStyle(int _value) 
    {
        for (int i = 0; i < _hairStyles.Length; i++)
        {
            _hairStyles[i].SetActive(false);
        }
        _hairStyles[(int)_value].SetActive(true);
        _currentHairStyle = _hairStyles[_value];
    }

    public void SetColor(GameObject _target, string _feature, float _value) 
    {
        Material _material;
        Renderer _targetRenderer;
        Image _sliderHandle;

        _targetRenderer = _target.GetComponent<Renderer>();

        switch (_feature) {
            case "Hair":
                _sliderHandle = _sliderHandles[0];
                _material = _materials[0];
                _sliderHandle.material = _material;
                break;

            case "Skin":
                _sliderHandle = _sliderHandles[1];
                _material = _materials[1];
                _sliderHandle.material = _material;
                break;

            case "Eyes":
                _sliderHandle = _sliderHandles[2];
                _material = _materials[2];
                _sliderHandle.material = _material;
                break;

            default:
                break;
        }
        _targetRenderer.sharedMaterial.SetFloat("_SamplePos", _value);
    }

    private void CreateCharacter() 
    {
        _hasCharacter = true;
        _isCreatingCharacter = false;

        //if playerthe save the character options
    }
}
