using TMPro;
using UnityEngine;

public class Sample06 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    private void Update() {
        float thickness = Mathf.Round((Mathf.Sin(Time.time) / 5f + 0.3f) * 100f) / 100f; // 0.1 - 0.5

        _fieldValue.text = $"Line Thickness: {thickness}\n2nd Line Thickness: {thickness}";
        _material.SetFloat("_Line_Thickness", thickness);
        _material.SetFloat("_2nd_Line_Thickness", thickness);
    }
}
