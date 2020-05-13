using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterCustomizerV2 : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] _hairStyles;
    private Material _hairMaterial;

    private void Start() 
    {
        
    }

    public void SetHairStyle(float _sliderValue) 
    {
        for (int i = 0; i < _hairStyles.Length; i++) 
        {
            _hairStyles[i].SetActive(false);
        }

        _hairStyles[(int)_sliderValue].SetActive(true);
    }

    public void SetHairColor(float _sliderValue) 
    {

    }

}