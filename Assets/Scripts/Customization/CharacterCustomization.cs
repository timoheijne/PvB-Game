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
    private Image[] _sliderHandles;

    [SerializeField, Tooltip("1=Skin 2=Eyes 3=Shirt 4=shoes 5=Gloves 6=Pants")]
    private Slider[] _customizationSliders;

    [SerializeField, Range(0, 1)]
    private float _skinColorGradient,
                  _eyeColorGradient;

    private int _oldSkinColorValue,
                _oldEyeColorValue,
                _oldShirtColorValue,
                _oldShoesColorValue,
                _oldGloveColorValue,
                _oldPantsColorValue;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("Shader Graphs/CharacterCustomization");

        if (!_hasCharacter && _isCreatingCharacter || _isNPC) 
        {
            _skinColorGradient = Random.value;
            _eyeColorGradient = Random.value;

            SetColor(gameObject, "Skin", _skinColorGradient);
            SetColor(gameObject, "Eyes", _eyeColorGradient);

            _customizationSliders[0].value = _skinColorGradient;
            _customizationSliders[1].value = _eyeColorGradient;
        }
    }

    public void SkinColorControl(float _value)
    {
        _skinColorGradient = _value;
        _oldSkinColorValue = (int)(Mathf.Round(_skinColorGradient * 100f) / 100f);
        SetColor(gameObject, "Skin", _skinColorGradient);
    }

    public void EyeColorControl(float value) 
    {
        _eyeColorGradient = value;
        _oldEyeColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
        SetColor(gameObject, "Eyes", _eyeColorGradient);
    }

    public void SetColor(GameObject _target, string _feature, float _value) 
    {
        Renderer _targetRenderer;
        Image _targetImage;

        _targetRenderer = _target.GetComponent<Renderer>();

        switch (_feature) {
            case "Skin":
                _targetImage = _sliderHandles[0].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 0);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

                _targetRenderer.sharedMaterial.SetVector("SkinGradientPos", new Vector2(_value, 0f));
                break;

            case "Eyes":
                _targetImage = _sliderHandles[1].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 1);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

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
