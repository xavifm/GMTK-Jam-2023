using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct PlaceData
{
    public Vector2Int pos;
    public Vector2Int initDir;
}

public class MouseSystem : MonoBehaviour
{
    const int ANIMALS_MARGIN = 2;

    [SerializeField] List<PlaceData> availablePlaces;

    Plane plane = new Plane(Vector3.up, RAYCAST_POSY);
    Vector3 mouseWorldPosition = Vector3.zero, vehicleWorldPosition = Vector3.zero;
    Camera cameraMain;

    const float RAYCAST_POSY = -0.1f;
    const float LERP_SPEED = 20;
    const float COLOR_MARK_ALPHA = 0.3f;

    MapSystem map;

    [SerializeField] Material colorMat;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();
        cameraMain = Camera.main;
    }

    void Update()
    {
        SetMarkPositionWithRaycast();
        CheckMapTile();
    }

    void SetMarkPositionWithRaycast()
    {
        float distance;
        Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
            mouseWorldPosition = ray.GetPoint(distance);

        vehicleWorldPosition = mouseWorldPosition = new Vector3(Mathf.Round(mouseWorldPosition.x), mouseWorldPosition.y, Mathf.Round(mouseWorldPosition.z));
        vehicleWorldPosition.y += 1f;
        transform.position = Vector3.Lerp(transform.position, mouseWorldPosition, Time.deltaTime * LERP_SPEED);
    }

    void CheckMapTile()
    {
        SquareData tileData = map.GetSquareData((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);

        if (CanPlaceCar(tileData.value)) colorMat.color = Color.green;
        else colorMat.color = Color.red;

        Color newColor = colorMat.color;
        newColor.a = COLOR_MARK_ALPHA;

        colorMat.color = newColor;

        Debug.Log(map.GetSquareData((int) mouseWorldPosition.x, (int) mouseWorldPosition.z).value.ToString());
    }

    public bool CanPlaceCar(MapSystem.SquareValue _tileValue)
    {
        //Debug.Log("Mouse Pos: (" + (int)mouseWorldPosition.x + ", " + (int)mouseWorldPosition.z + ")");
        return availablePlaces.FindIndex(_place => _place.pos.x == (int)mouseWorldPosition.x && _place.pos.y == (int)mouseWorldPosition.z) >= 0 &&
            !_tileValue.Equals(MapSystem.SquareValue.ANIMAL) &&
            !_tileValue.Equals(MapSystem.SquareValue.OBSTACLE) &&
            !_tileValue.Equals(MapSystem.SquareValue.OUTSIDE_MAP) &&
            !_tileValue.Equals(MapSystem.SquareValue.CAR) &&
            !_tileValue.Equals(MapSystem.SquareValue.HOLE);

        //return ((int)mouseWorldPosition.x == 0 || (int)mouseWorldPosition.x == map.width - 1) && 
        //    (int)mouseWorldPosition.z > ANIMALS_MARGIN && (int)mouseWorldPosition.z < map.height - ANIMALS_MARGIN - 1 &&
        //    !_tileValue.Equals(MapSystem.SquareValue.ANIMAL) &&
        //    !_tileValue.Equals(MapSystem.SquareValue.OBSTACLE) &&
        //    !_tileValue.Equals(MapSystem.SquareValue.OUTSIDE_MAP) &&
        //    !_tileValue.Equals(MapSystem.SquareValue.CAR) &&
        //    !_tileValue.Equals(MapSystem.SquareValue.HOLE);
    }

    public Vector2Int GetPlaceInitDir()
    {
        int placeIdx = availablePlaces.FindIndex(_place => _place.pos.x == (int)mouseWorldPosition.x && _place.pos.y == (int)mouseWorldPosition.z);
        if (placeIdx < 0) return Vector2Int.one;
        else return availablePlaces[placeIdx].initDir;
    }

    public Vector3 GetMousePos()
    {
        return mouseWorldPosition;
    }
    public Vector3 GetSelectedCarPos()
    {
        return vehicleWorldPosition;
    }


}
