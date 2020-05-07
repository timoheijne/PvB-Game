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
    private float _colorGradient = 0.2f;
    private float _oldColorValue;

    private Renderer _renderer;

    private void Start() 
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.material.shader = Shader.Find("PVB/ColorGrading");
    }

    public void SkinColorControl(float value)
    {
        Debug.Log("skin color value: " + value);
        _colorGradient = value;
        _oldColorValue = Mathf.Round(_colorGradient * 100f) / 100f;
    }
    private void Update()
    {
        if (_oldColorValue != Mathf.Round(_colorGradient * 100f) / 100f)
        {
            _renderer.material.SetFloat("_SamplePos", _colorGradient);
        }
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
