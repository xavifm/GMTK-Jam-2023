using TMPro;
using UnityEngine;

public class Sample08 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    private void Update() {
        Vector2 tiling = Vector2.one * (1.5f + 0.5f * Mathf.Sin(Time.time)); // 1 - 2
        Vector2 secondTiling = tiling * 2f;

        _fieldValue.text = $"Line Tiling: {tiling}\n2nd Line Tiling: {secondTiling}";
        _material.SetVector("_Line_Tiling", tiling);
        _material.SetVector("_2nd_Line_Tiling", secondTiling);
    }
}
