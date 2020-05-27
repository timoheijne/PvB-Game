using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    private GameObject _body, 
                       _player;

    private HairCustomization _playerHair;
    private CharacterCustomization _playerBody;

    private void Start() 
    {
        _player = gameObject;
        _body = _player.transform.Find("Body").gameObject;
        _playerBody = _body.GetComponent<CharacterCustomization>();
        _playerHair = _player.GetComponent<HairCustomization>();

        if (!_playerBody.IsCreatingCharacter && !_playerHair.IsCreatingCharacter) 
        {
            LoadHairStyle();
            LoadSkinValues();
        }
    }

    private void LoadHairStyle() 
    {
        for (int i = 0; i < _playerHair.HairStyles.Length; i++) {
            _playerHair.HairStyles[i].SetActive(false);
        }

        _playerHair.HairStyles[PlayerPrefs.GetInt("hairstyle")].SetActive(true);

        LoadHairColor();
    }

    private void LoadHairColor() 
    {
        if (_playerHair.CurrentHairStyle.name != "Bald") {
            Renderer _targetRenderer = _playerHair.CurrentHairStyle.GetComponent<Renderer>();
            _targetRenderer.sharedMaterial.SetFloat("_samplePos", PlayerPrefs.GetFloat("haircolor"));
        }

        Destroy(_player.GetComponent<HairCustomization>());
    }

    private void LoadSkinValues() 
    {
        Renderer _targetRenderer = _body.GetComponent<Renderer>();

        _targetRenderer.sharedMaterial.SetVector("SkinGradientPos", new Vector2(PlayerPrefs.GetFloat("skincolor"), 0f));
        _targetRenderer.sharedMaterial.SetVector("EyeGradientPos", new Vector2(PlayerPrefs.GetFloat("eyecolor"), 0f));
        _targetRenderer.sharedMaterial.SetVector("ShirtGradientPos", new Vector2(PlayerPrefs.GetFloat("shirtcolor"), 0f));
        _targetRenderer.sharedMaterial.SetVector("GlovesGradientPos", new Vector2(PlayerPrefs.GetFloat("glovecolor"), 0f));
        _targetRenderer.sharedMaterial.SetVector("PantsGradientPos", new Vector2(PlayerPrefs.GetFloat("pantscolor"), 0f));
        _targetRenderer.sharedMaterial.SetVector("ShoesGradientPos", new Vector2(PlayerPrefs.GetFloat("shoecolor"), 0f));

        Destroy(_body.GetComponent<CharacterCustomization>());
    }
}
