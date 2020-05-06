using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _hairStyles;

    [Range(0, 1)]
    public float ColorGradient = 0.25f;
    
    private Material _material;

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (_material == null) {
            return;
        }
        _material.SetFloat("_SamplePos", ColorGradient);


        Graphics.Blit(source, destination, _material);
    }

    public void SetHairStyle(float value) 
    {
        for (int i = 0; i < _hairStyles.Length; i++) 
        {
            _hairStyles[i].SetActive(false);
        }
        _hairStyles[(int)value].SetActive(true);
    }

}
