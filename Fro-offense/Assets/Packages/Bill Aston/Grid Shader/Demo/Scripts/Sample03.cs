using TMPro;
using UnityEngine;

public class Sample03 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    private void Update() {
        Vector2 tiling = Vector2.one * (1.5f + Mathf.Sin(Time.time) - (0.25f * (1 + Mathf.Sin(Time.time)))); // 0.5 - 2

        _fieldValue.text = $"BG Texture Tiling: {tiling}";
        _material.SetVector("_BG_Texture_Tiling", tiling);
    }
}
