using TMPro;
using UnityEngine;

public class Sample07 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private Material _fieldValue;
    [SerializeField] private Material _secondFieldValue;
    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;

    private void Update() {
        Color color = Color.Lerp(_firstColor, _secondColor, 0.5f * (1 + Mathf.Sin(Time.time)));
        Color secondColor = Color.Lerp(_secondColor, _firstColor, 0.5f * (1 + Mathf.Sin(Time.time)));

        _fieldValue.SetColor("_BG_Color", color);
        _secondFieldValue.SetColor("_BG_Color", secondColor);
        _material.SetColor("_Line_Color", color);
        _material.SetColor("_2nd_Line_Color", secondColor);
    }
}
