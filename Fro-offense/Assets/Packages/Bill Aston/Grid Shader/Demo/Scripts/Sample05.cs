using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Sample05 : MonoBehaviour {
    [SerializeField] private Material _material;
    [SerializeField] private TextMeshPro _fieldValue;

    LocalKeyword _scaleTextureKeyword;
    bool toggle;
    float timeout;

    private void Awake() {
        _scaleTextureKeyword = new LocalKeyword(_material.shader, "_SCALE_TEXTURE_WITH_SIZE");
        toggle = false;
    }

    private void Update() {
        if (Mathf.Sin(Time.time) <= -0.99f && Time.time > timeout) {
            timeout = Time.time + 0.5f;
            toggle = !toggle;
        }

        _fieldValue.text = $"Scale Texture With Size: {toggle}";
        _material.SetKeyword(_scaleTextureKeyword, toggle);

        Vector3 size = Vector3.one * (2.5f + (1.5f * Mathf.Sin(Time.time))); // 1 - 4

        transform.localScale = size;
        transform.position = new Vector3(transform.position.x, size.y / 2f, 0);
    }
}

