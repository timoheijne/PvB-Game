using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    private enum _objectTypes {Model, Handle}
    [SerializeField]
    private _objectTypes _objectType;

    [SerializeField]
    private Texture2D _mainTex, _gradientTex, _handleTex;

    [SerializeField]
    private Material _sliderHandleMaterial;
    private Image _sliderHandle;
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
            _sliderHandle = gameObject.GetComponent<Image>();
            _sliderHandle.material = _sliderHandleMaterial;
            _sliderHandle.material.SetTexture("_mainTex", _handleTex);
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
