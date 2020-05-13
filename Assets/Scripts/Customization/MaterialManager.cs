using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D _mainTex;
    private Material _material;

    void Start()
    {
        _material = gameObject.GetComponent<Material>();
        _material.SetTexture("_mainTex", _mainTex);
    }
}
