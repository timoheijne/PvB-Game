using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterCustomization : MonoBehaviour
{
    public static CharacterCustomization instance;

    [SerializeField]
    private GameObject[] _hairStyles;
    private int _hairstyle;

    [Range(0, 1)]
    private float _colorGradient = 0.2f;
    private float _oldColorValue;

    private Renderer _renderer;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.sharedMaterial.shader = Shader.Find("PVB/ColorGrading");
    }

    private void Update()
    {
        if (_oldColorValue != Mathf.Round(_colorGradient * 100f) / 100f)
        {
            _renderer.sharedMaterial.SetFloat("_SamplePos", _colorGradient);
        }
    }

    public void SkinColorControl(float value)
    {
        _colorGradient = value;
        _oldColorValue = Mathf.Round(_colorGradient * 100f) / 100f;
    }

    public void HairStyleControl(float value)
    {
        _hairstyle = Mathf.RoundToInt(value);
        SetHairStyle(_hairstyle);
    }

    public void SetHairStyle(int value) 
    {
        for (int i = 0; i < _hairStyles.Length; i++)
        {
            _hairStyles[i].SetActive(false);
        }
            _hairStyles[(int)value].SetActive(true);
    }
}
