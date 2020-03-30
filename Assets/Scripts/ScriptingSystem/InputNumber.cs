using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : NumberContainer
{
    [SerializeField]InputField input = null;

    [SerializeField] bool debugging = false;

    private void Start()
    {
        input.onValueChanged.AddListener(delegate { UpdateValue(); });
    }

    private void UpdateValue()
    {
        float.TryParse(input.text, NumberStyles.Float, CultureInfo.InvariantCulture, out Value);
        if (debugging) { Debug.Log(GetValue()); }
    }

}
