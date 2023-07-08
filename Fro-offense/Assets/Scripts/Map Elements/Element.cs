using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    MapSystem map;
    Vector2 elementPos;

    public MapSystem.SquareValue elementType;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();
    }

    private void Update()
    {
        ResetMapSquareValue();
        elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
        SetMapSquareValue();
    }

    private void SetMapSquareValue()
    {
        map.SetSquareValue((int) elementPos.x, (int) elementPos.y, elementType);
    }

    private void ResetMapSquareValue()
    {
        map.SetSquareValue((int)elementPos.x, (int)elementPos.y, MapSystem.SquareValue.EMPTY);
    }
}
