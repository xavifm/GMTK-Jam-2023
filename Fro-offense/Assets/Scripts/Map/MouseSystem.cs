using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{
    Plane plane = new Plane(Vector3.up, RAYCAST_POSY);
    Vector3 worldPosition = Vector3.zero;
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
            worldPosition = ray.GetPoint(distance);

        worldPosition = new Vector3(Mathf.Round(worldPosition.x), worldPosition.y, Mathf.Round(worldPosition.z));
        transform.position = Vector3.Lerp(transform.position, worldPosition, Time.deltaTime * LERP_SPEED);
    }

    void CheckMapTile()
    {
        MapSystem.SquareValue tileValue = map.GetSquareValue((int)worldPosition.x, (int)worldPosition.z);

        if (tileValue.Equals(MapSystem.SquareValue.ANIMAL) || 
            tileValue.Equals(MapSystem.SquareValue.OBSTACLE) || 
            tileValue.Equals(MapSystem.SquareValue.OUTSIDE_MAP) || 
            tileValue.Equals(MapSystem.SquareValue.CAR) || 
            tileValue.Equals(MapSystem.SquareValue.HOLE)
            )
            colorMat.color = Color.red;
        else
            colorMat.color = Color.green;

        Color newColor = colorMat.color;
        newColor.a = COLOR_MARK_ALPHA;

        colorMat.color = newColor;

        Debug.Log(map.GetSquareValue((int) worldPosition.x, (int) worldPosition.z).ToString());
    }
}
