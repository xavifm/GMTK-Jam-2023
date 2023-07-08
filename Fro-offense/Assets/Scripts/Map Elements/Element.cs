using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    Vector2 elementPos;
    public MapSystem.SquareValue elementType;
    protected MapSystem map;
    protected float originalYPos;

    internal ElementMoveSystem moveSystem;

    protected new void Start()
    {
        originalYPos = transform.position.y;
        moveSystem = GetComponent<ElementMoveSystem>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();
    }

    protected new void Update()
    {
        ResetMapSquareValue();
        elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
        SetMapSquareValue();
    }

    protected void SetMapSquareValue()
    {
        map.SetSquareValue((int) elementPos.x, (int) elementPos.y, elementType);
    }

    protected void ResetMapSquareValue()
    {
        map.SetSquareValue((int)elementPos.x, (int)elementPos.y, MapSystem.SquareValue.EMPTY);
    }
}
