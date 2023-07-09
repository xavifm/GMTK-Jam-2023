using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Sample04 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    LocalKeyword preserveTextureXZ;
    LocalKeyword preserveTextureXY;
    LocalKeyword preserveTextureZY;
    bool toggleXZ;
    bool toggleXY;
    bool toggleZY;

    float timeout;

    private void Awake() {
        preserveTextureXZ = new LocalKeyword(_material.shader, "_PRESERVE_TEXTURE_ON_AXIS_XZ");
        preserveTextureXY = new LocalKeyword(_material.shader, "_PRESERVE_TEXTURE_ON_AXIS_XY");
        preserveTextureZY = new LocalKeyword(_material.shader, "_PRESERVE_TEXTURE_ON_AXIS_ZY");
        toggleXZ = true;
        toggleXY = false;
        toggleZY = false;

        _fieldValue.text = $"Preserve Texture On Axis: XZ";
    }

    private void Update() {
        if (Mathf.Abs(Mathf.Sin(Time.time)) >= 0.99f && Time.time > timeout) {
            timeout = Time.time + 0.5f;

            if (toggleXZ) {
                toggleXZ = false;
                toggleXY = true;
                _fieldValue.text = $"Preserve Texture On Axis: XY";
            } else if (toggleXY) {
                toggleXY = false;
                toggleZY = true;
                _fieldValue.text = $"Preserve Texture On Axis: ZY";
            } else if (toggleZY) {
                toggleZY = false;
                toggleXZ = true;
                _fieldValue.text = $"Preserve Texture On Axis: XZ";
            }
        }

        _material.SetKeyword(preserveTextureXZ, toggleXZ);
        _material.SetKeyword(preserveTextureXY, toggleXY);
        _material.SetKeyword(preserveTextureZY, toggleZY);
    }
}