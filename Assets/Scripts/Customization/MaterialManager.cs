using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    private enum _objectTypes {Model, Handle}
    [SerializeField]
    private _objectTypes _objectType;

    [Header("Model only")]
    [SerializeField]
    private Texture2D _mainTex;

    [Header("UI Handle only")]
    [SerializeField]
    private Texture2D _gradientTex;
    [SerializeField]
    private Texture2D _handleTex;

    private Image _sliderHandleImage;
    private Renderer _renderer;

    void Start()
    {
        if (_objectType == _objectTypes.Model) 
        {
            _renderer = gameObject.GetComponent<Renderer>();
            _renderer.sharedMaterial.SetTexture("_mainTex", _mainTex);
            _renderer.sharedMaterial.SetTexture("_secondTex", _gradientTex);
        }

        if (_objectType == _objectTypes.Handle) 
        {
            _sliderHandleImage = gameObject.GetComponent<Image>();
            _sliderHandleImage.material = Instantiate(_sliderHandleImage.material);
            _sliderHandleImage.material.SetTexture("_mainTex", _handleTex);
        }
    }

    private void Update() 
    {
        if (_objectType == _objectTypes.Model) 
        {
            if (_renderer.sharedMaterial.GetTexture("_mainTex") != _mainTex) 
            {
                _renderer.sharedMaterial.SetTexture("_mainTex", _mainTex);
            }
        }
    }
}
