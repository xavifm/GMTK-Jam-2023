using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    protected const float TIMER_BASE = 0.7f;

    [SerializeField] protected Transform model;
    enum AnimState { IDLE = 0, MOVING = 1, NONE = 2 }

    Vector2 elementPos;
    public MapSystem.SquareValue elementType;
    protected MapSystem map;
    protected float originalYPos;

    internal ElementMoveSystem moveSystem;
    protected float moveTimer = 0;
    bool canMove = false;

    Animator animalAnim;

    const float RUN_DISTANCE_DETECTION = 0.2f;
    const float SMOOTH_DAMP_SPEED = 0.1f;
    float lerpSpeed = 0;

    [SerializeField] float state = 0;

    protected new void Start()
    {
        originalYPos = transform.position.y;
        moveSystem = GetComponent<ElementMoveSystem>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<MapSystem>();

        if (elementType == MapSystem.SquareValue.ANIMAL) GameManager.Instance.AddRemainingAnimal();
    }

    protected new void Update()
    {
        ResetMapSquareValue();
        elementPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));
        SetMapSquareValue();

        CheckCanMove();
        if (canMove) MoveStateMachine();

        AnimationStateMachine();
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

    void AnimationStateMachine()
    {
        if(elementType.Equals(MapSystem.SquareValue.ANIMAL))
        {
            if (animalAnim == null)
                animalAnim = model.GetComponent<Animator>();
            
            if(animalAnim != null)
            {
                Debug.Log(transform.name + " " + Vector3.Distance(transform.position, moveSystem.destinationVector));
                if(Vector3.Distance(transform.position, moveSystem.destinationVector) > RUN_DISTANCE_DETECTION)
                    SetAnimation(AnimState.MOVING);
                else
                    SetAnimation(AnimState.IDLE);
            }
        }
    }

    void SetAnimation(AnimState _animState, float _animSpeed = 1f)
    {
        if (_animState.Equals(AnimState.MOVING))
            state = Mathf.SmoothDamp(state, 0.5f, ref lerpSpeed, SMOOTH_DAMP_SPEED);
        else
            state = Mathf.SmoothDamp(state, 0, ref lerpSpeed, SMOOTH_DAMP_SPEED);

        animalAnim.speed = _animSpeed;
        animalAnim.SetFloat("state", state);
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

}
