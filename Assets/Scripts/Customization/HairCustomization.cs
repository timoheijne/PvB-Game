using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HairCustomization : MonoBehaviour 
{
    public static HairCustomization instance;

    [SerializeField]
    private GameObject[] _hairStyles;
    private GameObject _currentHairStyle;
    private Material _hairMaterial;
    
    public void SetHairStyle(float _sliderValue) 
    {
        for (int i = 0; i < _hairStyles.Length; i++) 
        {
            _hairStyles[i].SetActive(false);
        }

        _hairStyles[(int)_sliderValue].SetActive(true);
        _currentHairStyle = _hairStyles[(int)_sliderValue];
    }

    public void SetHairColor(float _sliderValue) 
    {
        if (_currentHairStyle.name != "Bald") 
        {
            Renderer _targetRenderer = _currentHairStyle.GetComponent<Renderer>();
            _targetRenderer.sharedMaterial.SetFloat("_samplePos", _sliderValue);
        }
    }

}