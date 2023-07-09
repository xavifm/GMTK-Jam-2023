using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] Transform vehiclesContainer;
    [SerializeField] GameObject vehicleUIPrefab;

    private void Start()
    {
        InitVehiclesUI();
    }


    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void InitVehiclesUI()
    {
        for(int i = 0; i < CustomizationManager.Instance.customVehiclesData.Count; i++)
        {
            VehicleUI_Data newVehicleUI = Instantiate(vehicleUIPrefab, vehiclesContainer).GetComponent<VehicleUI_Data>();
            newVehicleUI.InitUI(CustomizationManager.Instance.customVehiclesData[i], i);
        }
    }

}
