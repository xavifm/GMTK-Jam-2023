using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CustomVehicleData
{
    public string name;
    public int amount;
    public Sprite img;
    public GameObject prefab;
}


public class CustomizationManager : MonoBehaviour
{
    const float PLACE_ELEMENT_DELAY = 0.5f;

    private static CustomizationManager instance;
    public static CustomizationManager Instance { get { return instance; } }

    public Transform vehiclesContainter;
    public List<CustomVehicleData> customVehiclesData;

    ElementMoveSystem currSelectedElementMS = null;
    Element currSelectedElement = null;
    int currSelectedElementId = -1;
    bool currSelectedElementOriginalYEnabled;
    bool canPlaceElement = true;

    MouseSystem mouseSystem;
    MapSystem map;

    private void Awake()
    {
        if (CustomizationManager.instance == null)
        {
            CustomizationManager.instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mouseSystem = FindObjectOfType<MouseSystem>();
        map = FindObjectOfType<MapSystem>();
    }


    private void Update()
    {
        if (!GameManager.Instance.GameStateEquals(GameState.CUSTOMIZE)) return;

        SquareData squareData = map.GetSquareData((int)mouseSystem.GetMousePos().x, (int)mouseSystem.GetMousePos().y);
        if (HasSelectedElement())
        {
            currSelectedElementMS.destinationVector = mouseSystem.GetSelectedCarPos();
            //Debug.Log("CanPlaceElement: " + mouseSystem.CanPlaceCar(currSelectedElement.elementType));
            //Debug.Log("SquareValue: " + squareData.value.ToString());
            //Debug.Log("MouseDown: " + Input.GetKeyDown(KeyCode.Mouse0));
            Debug.Log("Has element ref: " + squareData.elementRef != null);
            if (squareData.elementRef != null) Debug.Log("Element ref: " + squareData.elementRef.elementType.ToString());
            if (Input.GetKeyDown(KeyCode.Mouse0) && canPlaceElement && mouseSystem.CanPlaceCar(currSelectedElement.elementType)
                && (squareData.value == MapSystem.SquareValue.EMPTY || squareData.value == MapSystem.SquareValue.CAR)
                && !currSelectedElement.collidingWithCar)
            {
                //Element prevElementRef = squareData.elementRef;
                PlaceSelectedElement();
                //if (prevElementRef != null && prevElementRef.elementType == MapSystem.SquareValue.CAR)
                //{
                //    SetSelectedElement(prevElementRef.elementUIId);
                //    Destroy(prevElementRef.gameObject);
                //}
                AudioManager.Instance.Play_SFX("UIClick_SFX");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RemoveElementSelection();
                AudioManager.Instance.Play_SFX("UICancel_SFX");
            }
        }
        else if(canPlaceElement)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && squareData.elementRef != null && squareData.value == MapSystem.SquareValue.CAR)
            {
                squareData.elementRef.ResetMapSquareValue();
                SetSelectedElement(squareData.elementRef.elementUIId);
                Destroy(squareData.elementRef.gameObject);
            }
        }

    }

    public void SetSelectedElement(int _elementId)
    {
        currSelectedElementId = _elementId;
        currSelectedElement = Instantiate(customVehiclesData[_elementId].prefab, vehiclesContainter).GetComponent<Element>();
        currSelectedElement.elementType = MapSystem.SquareValue.EMPTY;
        currSelectedElement.transform.position = mouseSystem.GetSelectedCarPos();
        currSelectedElementMS = currSelectedElement.GetComponent<ElementMoveSystem>();
        currSelectedElementOriginalYEnabled = currSelectedElementMS.enableYMovement;
        currSelectedElementMS.enableYMovement = true;
        StartCoroutine(PlaceElementDelay_Cor());
    }

    public void PlaceSelectedElement()
    {
        currSelectedElementMS.enableYMovement = currSelectedElementOriginalYEnabled;
        currSelectedElement.elementType = MapSystem.SquareValue.CAR;
        map.SetSquareValue((int)mouseSystem.GetMousePos().x, (int)mouseSystem.GetMousePos().z, new SquareData(currSelectedElement.elementType, currSelectedElement));
        currSelectedElement.SetInitDir(mouseSystem.GetPlaceInitDir());
        currSelectedElement = null;
        currSelectedElementMS = null;
        currSelectedElementId = -1;
        StartCoroutine(PlaceElementDelay_Cor());
    }

    public bool HasSelectedElement()
    {
        return currSelectedElement != null && currSelectedElementId >= 0;
    }

    public void RemoveElementSelection()
    {
        customVehiclesData[currSelectedElementId].amount++;
        currSelectedElementMS.enableYMovement = currSelectedElementOriginalYEnabled;
        Destroy(currSelectedElementMS.gameObject);
        currSelectedElement = null;
        currSelectedElementMS = null;
        currSelectedElementId = -1;
    }

    IEnumerator PlaceElementDelay_Cor()
    {
        canPlaceElement = false;
        yield return new WaitForSeconds(PLACE_ELEMENT_DELAY);
        canPlaceElement = true;
    }

}
