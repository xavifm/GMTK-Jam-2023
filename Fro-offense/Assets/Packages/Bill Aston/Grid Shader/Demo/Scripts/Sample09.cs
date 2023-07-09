using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Sample09 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    LocalKeyword preserveTilingXZ;
    LocalKeyword preserveTilingXY;
    LocalKeyword preserveTilingZY;
    bool toggleXZ;
    bool toggleXY;
    bool toggleZY;

    float timeout;

    private void Awake() {
        preserveTilingXZ = new LocalKeyword(_material.shader, "_PRESERVE_TILING_ON_AXIS_XZ");
        preserveTilingXY = new LocalKeyword(_material.shader, "_PRESERVE_TILING_ON_AXIS_XY");
        preserveTilingZY = new LocalKeyword(_material.shader, "_PRESERVE_TILING_ON_AXIS_ZY");
        toggleXZ = true;
        toggleXY = false;
        toggleZY = false;

        _fieldValue.text = $"Preserve Tiling On Axis: XZ";
    }

    private void Update() {
        if (Mathf.Abs(Mathf.Sin(Time.time)) >= 0.99f && Time.time > timeout) {
            timeout = Time.time + 0.5f;

            if (toggleXZ) {
                toggleXZ = false;
                toggleXY = true;
                _fieldValue.text = $"Preserve Tiling On Axis: XY";
            } else if (toggleXY) {
                toggleXY = false;
                toggleZY = true;
                _fieldValue.text = $"Preserve Tiling On Axis: ZY";
            } else if (toggleZY) {
                toggleZY = false;
                toggleXZ = true;
                _fieldValue.text = $"Preserve Tiling On Axis: XZ";
            }
        }

        _material.SetKeyword(preserveTilingXZ, toggleXZ);
        _material.SetKeyword(preserveTilingXY, toggleXY);
        _material.SetKeyword(preserveTilingZY, toggleZY);
    }
}