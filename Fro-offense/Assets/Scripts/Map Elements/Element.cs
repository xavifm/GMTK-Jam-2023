using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    Vector2 elementPos;
    public MapSystem.SquareValue elementType;
    protected MapSystem map;

    internal ElementMoveSystem moveSystem;
    protected float moveTimer = 0;

    protected new void Start()
    {
        moveSystem = GetComponent<ElementMoveSystem>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();
        if (elementType == MapSystem.SquareValue.ANIMAL) GameManager.Instance.AddRemainingAnimal();
    }

    protected new void Update()
    {
        ResetMapSquareValue();
        elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
        SetMapSquareValue();
        if(GameManager.Instance.GameStateEquals(GameState.PLAY)) MoveStateMachine();
    }

    protected void SetMapSquareValue()
    {
        map.SetSquareValue((int) elementPos.x, (int) elementPos.y, elementType);
    }

    protected void ResetMapSquareValue()
    {
        map.SetSquareValue((int)elementPos.x, (int)elementPos.y, MapSystem.SquareValue.EMPTY);
    }

    protected virtual void MoveStateMachine()
    {
        moveTimer -= Time.deltaTime;
    }

    public void KillAnimal()
    {
        if(elementType == MapSystem.SquareValue.ANIMAL)
        {
            GameManager.Instance.KillRemainingAnimal();
            Destroy(gameObject);
        }
    }
}
