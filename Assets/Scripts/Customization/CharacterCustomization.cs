using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterCustomization : MonoBehaviour
{
    public static CharacterCustomization instance;

    [SerializeField]
    private GameObject[] _hairStyles;

    [Range(0, 1)]
    private float _colorGradient = 0.25f;

    private Material _material;

    private void Start() 
    {
        _material = gameObject.GetComponent<Material>();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) 
    {
        if (_material == null) 
        {
            return;
        }

        _colorGradient = Mathf.Round(_colorGradient * 100f) / 100f;
        _material.SetFloat("_SamplePos", _colorGradient);


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
