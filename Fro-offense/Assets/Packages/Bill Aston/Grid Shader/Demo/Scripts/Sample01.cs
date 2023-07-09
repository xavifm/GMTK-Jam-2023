using UnityEngine;

public class Sample01 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private Material _fieldValue;
    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;

    private void Update() {
        Color color = Color.Lerp(_firstColor, _secondColor, 0.5f * (1 + Mathf.Sin(Time.time)));

        _fieldValue.SetColor("_BG_Color", color);
        _material.SetColor("_BG_Color", color);
    }
}
