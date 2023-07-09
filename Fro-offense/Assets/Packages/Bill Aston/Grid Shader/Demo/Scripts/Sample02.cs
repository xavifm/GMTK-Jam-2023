using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Sample02 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    LocalKeyword useBGKeyword;
    bool toggle;
    float timeout;

    private void Awake() {
        useBGKeyword = new LocalKeyword(_material.shader, "_USE_BG_TEXTURE");
        toggle = false;
        _fieldValue.text = $"Use BG Texture: False";
    }

    private void Update() {
        if (Mathf.Abs(Mathf.Sin(Time.time)) >= 0.99f && Time.time > timeout) {
            timeout = Time.time + 0.5f;
            toggle = !toggle;

            _fieldValue.text = $"Use BG Texture: {toggle}";
            _material.SetKeyword(useBGKeyword, toggle);
        }
    }
}