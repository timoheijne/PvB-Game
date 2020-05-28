using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CharacterCustomization : MonoBehaviour 
{
    private Renderer _renderer;

    [SerializeField]
    private bool _hasCharacter, 
                 _isNPC;

    public bool IsCreatingCharacter;

    [SerializeField]
    private GameObject _targetCharacter;

    [SerializeField]
    private Image[] _sliderHandles;

    [SerializeField, Tooltip("0=Skin 1=Eyes 2=Shirt 3=shoes 4=Gloves 5=Pants")]
    private Slider[] _customizationSliders;

    [SerializeField, Range(0, 1)]
    private float _skinColorGradient,
                  _eyeColorGradient,
                  _shirtColorGradient,
                  _shoeColorGradient,
                  _gloveColorGradient,
                  _pantsColorGradient;

    private int _oldSkinColorValue,
                _oldEyeColorValue,
                _oldShirtColorValue,
                _oldShoeColorValue,
                _oldGloveColorValue,
                _oldPantsColorValue;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("Shader Graphs/CharacterCustomization");

        if (!_hasCharacter && IsCreatingCharacter || _isNPC) 
        {
            /*_skinColorGradient = Random.value;
            _eyeColorGradient = Random.value;
            _shirtColorGradient = Random.value;
            _shoeColorGradient = Random.value;
            _gloveColorGradient = Random.value;
            _pantsColorGradient = Random.value;*/

            _customizationSliders[0].value = _skinColorGradient;
            _customizationSliders[1].value = _eyeColorGradient;
            _customizationSliders[2].value = _shirtColorGradient;
            _customizationSliders[3].value = _shoeColorGradient;
            _customizationSliders[4].value = _gloveColorGradient;
            _customizationSliders[5].value = _pantsColorGradient;

            SetColor(gameObject, "Skin", _skinColorGradient);
            SetColor(gameObject, "Eyes", _eyeColorGradient);
            SetColor(gameObject, "Shirt", _shirtColorGradient);
            SetColor(gameObject, "Shoes", _shoeColorGradient);
            SetColor(gameObject, "Gloves", _gloveColorGradient);
            SetColor(gameObject, "Pants", _pantsColorGradient);
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

    public void ShirtColorControl(float value)
    {
        _shirtColorGradient = value;
        _oldShirtColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
        SetColor(gameObject, "Shirt", _shirtColorGradient);
    }

    public void ShoesColorControl(float value)
    {
        _shoeColorGradient = value;
        _oldShoeColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
        SetColor(gameObject, "Shoes", _shoeColorGradient);
    }

    public void GlovesColorControl(float value)
    {
        _gloveColorGradient = value;
        _oldGloveColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
        SetColor(gameObject, "Gloves", _gloveColorGradient);
    }

    public void PantsColorControl(float value)
    {
        _pantsColorGradient = value;
        _oldPantsColorValue = (int)(Mathf.Round(_eyeColorGradient * 100f) / 100f);
        SetColor(gameObject, "Pants", _pantsColorGradient);
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

            case "Shirt":
                _targetImage = _sliderHandles[2].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 1);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

                _targetRenderer.sharedMaterial.SetVector("ShirtGradientPos", new Vector2(_value, 0f));
                break;

            case "Shoes":
                _targetImage = _sliderHandles[3].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 1);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

                _targetRenderer.sharedMaterial.SetVector("ShoesGradientPos", new Vector2(_value, 0f));
                break;

            case "Gloves":
                _targetImage = _sliderHandles[4].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 1);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

                _targetRenderer.sharedMaterial.SetVector("GlovesGradientPos", new Vector2(_value, 0f));
                break;

            case "Pants":
                _targetImage = _sliderHandles[5].GetComponent<Image>();
                _targetImage.material.SetFloat("Bool_IsRainbow", 1);
                _targetImage.material.SetVector("Vector2_Texture_Position", new Vector2(_value, 0f));

                _targetRenderer.sharedMaterial.SetVector("PantsGradientPos", new Vector2(_value, 0f));
                break;

            default:
                break;
        }
    }

    public void CreateCharacter() 
    {
        _hasCharacter = true;
        IsCreatingCharacter = false;

        PlayerPrefs.SetInt("hasCharacter", 1);
        PlayerPrefs.SetFloat("skincolor", _skinColorGradient);
        PlayerPrefs.SetFloat("eyecolor", _eyeColorGradient);
        PlayerPrefs.SetFloat("shirtcolor", _shirtColorGradient);
        PlayerPrefs.SetFloat("glovecolor", _gloveColorGradient);
        PlayerPrefs.SetFloat("pantscolor", _pantsColorGradient);
        PlayerPrefs.SetFloat("shoecolor", _shoeColorGradient);
        PlayerPrefs.Save();

        LevelSystem.Instance.ChangeToMainMenu(true);
    }

    public void ExitCustomizer()
    {
        LevelSystem.Instance.ChangeToMainMenu(true);
    }
}
