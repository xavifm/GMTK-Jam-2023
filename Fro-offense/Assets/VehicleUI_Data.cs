using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VehicleUI_Data : MonoBehaviour
{
    [SerializeField] Image bg, vehicleImg;
    [SerializeField] TextMeshProUGUI vehicleAmount;
    public Button clickableArea;
    [SerializeField] Color baseColor, cancelColor;

    CustomVehicleData vehicleData;
    int vehicleId;

    private void Start()
    {
        bg.color = baseColor;
    }

    public void InitUI(CustomVehicleData _vehicleData, int _vehicleId)
    {
        vehicleData = _vehicleData;
        vehicleId = _vehicleId;
        vehicleImg.sprite = vehicleData.img;
        RefreshUI();
    }
    public void RefreshUI()
    {
        vehicleAmount.text = vehicleData.amount.ToString();
    }

    public void PressButton()
    {
        if (!GameManager.Instance.GameStateEquals(GameState.CUSTOMIZE)) return;

        if (vehicleData.amount > 0)
        {
            if (CustomizationManager.Instance.HasSelectedElement())
            {
                CustomizationManager.Instance.RemoveElementSelection();
            }
            CustomizationManager.Instance.SetSelectedElement(vehicleId);
            vehicleData.amount--;
            AudioManager.Instance.Play_SFX("UIClick_SFX");
        }
        else
        {
            StartCoroutine(CancelBgLerp());
            AudioManager.Instance.Play_SFX("UICancel_SFX");
        }

    }

    private void LateUpdate()
    {
        RefreshUI();
    }


    IEnumerator CancelBgLerp(float _lerpTime = 0.2f)
    {
        yield return LerpImgColor(bg, baseColor, cancelColor, _lerpTime);
        yield return LerpImgColor(bg, cancelColor, baseColor, _lerpTime);
    }

    IEnumerator LerpImgColor(Image _img, Color _initColor, Color _targetColor, float _lerpTime = 0.3f)
    {
        _img.color = _initColor;
        float timer = 0;
        while(timer < _lerpTime)
        {
            yield return null;
            timer += Time.deltaTime;
            _img.color = Color.Lerp(_initColor, _targetColor, timer / _lerpTime);
        }
    }

}
