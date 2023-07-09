using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] protected Transform model;
    public int elementUIId;

    Vector2 elementPos;
    public MapSystem.SquareValue elementType;
    protected MapSystem map;
    protected float originalYPos;

    internal ElementMoveSystem moveSystem;
    protected float moveTimer = 1f;
    bool canMove = false;

    protected new void Start()
    {
        originalYPos = transform.position.y;
        moveSystem = GetComponent<ElementMoveSystem>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();

        if (elementType == MapSystem.SquareValue.ANIMAL) GameManager.Instance.AddRemainingAnimal();
    }

    protected new void Update()
    {
        if (GameManager.Instance.GameStateEquals(GameState.PLAY))
        {
            ResetMapSquareValue();
            elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
            SetMapSquareValue();
        }
        else
        {
            elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
        }

        CheckCanMove();
        if (canMove)
        {
            MoveStateMachine();
        }
    }

    protected void SetMapSquareValue()
    {
        map.SetSquareValue((int) elementPos.x, (int) elementPos.y, new SquareData(elementType, this));
    }

    protected void ResetMapSquareValue()
    {
        map.SetSquareValue((int)elementPos.x, (int)elementPos.y, new SquareData(MapSystem.SquareValue.EMPTY, null));
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

    void CheckCanMove()
    {
        if (GameManager.Instance.GameStateEquals(GameState.PLAY) && !canMove)
        {
            canMove = true;
            Collider col = GetComponent<Collider>();
            if(col != null) col.enabled = true;
        }
        else if (!GameManager.Instance.GameStateEquals(GameState.PLAY) && canMove)
        {
            canMove = false;
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(elementType == MapSystem.SquareValue.CAR)
        {
            Element otherElement = other.GetComponent<Element>();
            if (otherElement != null && otherElement.elementType == MapSystem.SquareValue.ANIMAL)
                otherElement.KillAnimal();
        }
    }


    protected void RotateTo(Quaternion _targetRot, float _rotTime = 0.2f)
    {
        StartCoroutine(RotateTo_Cor(_targetRot, _rotTime));
    }
    private IEnumerator RotateTo_Cor(Quaternion _targetRot, float _lerpTime = 0.2f)
    {
        Quaternion initRot = model.rotation;
        float timer = 0;

        while (timer < _lerpTime)
        {
            yield return null;
            timer += Time.deltaTime;
            model.rotation = Quaternion.Lerp(initRot, _targetRot, timer / _lerpTime);
        }
    }

    public virtual void SetInitDir(Vector2Int _initDir)
    {
        if(_initDir.x != 0) model.rotation = Quaternion.Euler(0, 90 * _initDir.x, 0);
    }


    //private void OnMouseDown()
    //{
    //    if (elementType == MapSystem.SquareValue.CAR && GameManager.Instance.GameStateEquals(GameState.CUSTOMIZE))
    //    {
    //        MouseSystem mouseSystem = FindObjectOfType<MouseSystem>();
    //        if (map.GetSquareData((int)mouseSystem.GetMousePos().x, (int)mouseSystem.GetMousePos().z) != MapSystem.SquareValue.EMPTY) return;

    //        if (CustomizationManager.Instance.HasSelectedElement())
    //        {
    //            return;
    //            //CustomizationManager.Instance.RemoveElementSelection();
    //        }
    //        CustomizationManager.Instance.SetSelectedElement(elementUIId);
    //        Destroy(gameObject);
    //    }
    //}

}
