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

    [SerializeField]
    private Material[] _materials;

    [SerializeField]
    private Texture2D _skinTexture;

    [SerializeField]
    private Image[] _sliderHandles;

    [SerializeField]
    private Slider[] _customizationSliders;

    [SerializeField, Range(0, 1)]
    private float _skinColorGradient,
                  _eyeColorGradient,
                  _hairColorGradient = 0.2f;

    private int _hairstyle;

    private int _oldHairColorValue,
                _oldSkinColorValue,
                _oldEyeColorValue,
                _oldShirtColorValue,
                _oldShoetColorValue,
                _oldGloveColorValue,
                _oldPantsColorValue;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("Shader Graphs/CharacterCustomization");

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
        if (_oldHairColorValue != Mathf.Round(_hairColorGradient * 100f) / 100f) {
            if (!_currentHairStyle == _hairStyles[0]) {
                SetColor(_currentHairStyle, "Hair", _hairColorGradient);
                Debug.Log("SetColor: Hair");
            }
        }
    }

    public void SkinColorControl(float _value)
    {
        _skinColorGradient = _value;
        _oldSkinColorValue = (int)(Mathf.Round(_skinColorGradient * 100f) / 100f);
        SetColor(gameObject, "Skin", _skinColorGradient);
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
        SetColor(gameObject, "Eyes", _eyeColorGradient);
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
        Renderer _targetRenderer;
        Image _sliderHandle;
        Material _sliderMaterial;

        _targetRenderer = _target.GetComponent<Renderer>();

        switch (_feature) {
            case "Hair":
                _sliderHandle = _sliderHandles[0].GetComponent<Image>();
                _targetRenderer.sharedMaterial.SetFloat("_samplePos", _value);
                break;

            case "Skin":
                _sliderHandle = _sliderHandles[1].GetComponent<Image>();
                _sliderMaterial = _sliderHandle.GetComponent<Image>().material;
                //_sliderHandle.color = _color;
                _sliderMaterial.SetFloat("Bool_IsRainbow", 0);
                _sliderMaterial.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));
                _targetRenderer.sharedMaterial.SetVector("SkinGradientPos", new Vector2(_value, 0f));
                break;

            case "Eyes":
                _sliderHandle = _sliderHandles[2].GetComponent<Image>();
                _targetRenderer.sharedMaterial.SetVector("EyeGradientPos", new Vector2(_value, 0f));
                break;

            default:
                break;
        }
    }

    private void CreateCharacter() 
    {
        _hasCharacter = true;
        _isCreatingCharacter = false;

        //if playerthe save the character options
    }
}
