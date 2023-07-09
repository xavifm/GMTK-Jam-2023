using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Element
{
    [SerializeField] bool dodging = false;
    int dodgeDir = 0;
    int snakeXDIR = 0;

    bool horizontalMovement = false;

    private void Start()
    {
        RandomizeSnakeFirstMove();

        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    void RandomizeSnakeFirstMove()
    {
        int random = Random.Range(0, 10);

        if (random > 5)
            SnakeStateMachine();
    }

    protected override void MoveStateMachine()
    {
        base.MoveStateMachine();

        if (moveTimer <= 0)
        {
            moveTimer = TIMER_BASE;
            horizontalMovement = !horizontalMovement;

            if(!horizontalMovement)
            {
                VerticalMovementStateMachine();
                RotateTo(Quaternion.Euler(0, 0, 0));
            }
            else
            {
                HorizontalMovementStateMachine();
                RotateTo(Quaternion.Euler(0, 90 * -snakeXDIR, 0));
            }

            if (dodging)
            {
                if (!map.GetSquareData((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OUTSIDE_MAP)
                    && !map.GetSquareData((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OBSTACLE)
                    && !map.GetSquareData((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.HOLE)
                    && !map.GetSquareData((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.ANIMAL)
                    )
                {
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + dodgeDir, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                    RotateTo(Quaternion.Euler(0, 90 * dodgeDir, 0));
                }
                else
                {
                    DodgeStateMachine();
                }
            }
        }
    }

    void VerticalMovementStateMachine()
    {
        MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x, (int)moveSystem.destinationVector.z + 1);

        switch (nextSquareValue)
        {
            case MapSystem.SquareValue.OUTSIDE_MAP:
                GameManager.Instance.ChangeGameState(GameState.LOSE_GAME);
                break;
            case MapSystem.SquareValue.EMPTY:
                dodging = false;
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, moveSystem.destinationVector.y, moveSystem.destinationVector.z + 1);
                break;
            case MapSystem.SquareValue.OBSTACLE:
                EnableDodgeMode();
                break;
            case MapSystem.SquareValue.HOLE:
                EnableDodgeMode();
                break;
            case MapSystem.SquareValue.ANIMAL:
                EnableDodgeMode();
                break;
        }
    }

    void HorizontalMovementStateMachine()
    {
        MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x + snakeXDIR, (int)moveSystem.destinationVector.z);

        switch (nextSquareValue)
        {
            case MapSystem.SquareValue.EMPTY:
                dodging = false;
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + snakeXDIR, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                break;
            case MapSystem.SquareValue.OBSTACLE:
                EnableDodgeMode();
                break;
            case MapSystem.SquareValue.HOLE:
                EnableDodgeMode();
                break;
            case MapSystem.SquareValue.ANIMAL:
                EnableDodgeMode();
                break;
            case MapSystem.SquareValue.OUTSIDE_MAP:
                EnableDodgeMode();
                break;
        }
        SnakeStateMachine();
    }

    void EnableDodgeMode()
    {
        if (!dodging)
        {
            dodging = true;
            DodgeStateMachine();
        }
    }

    void DodgeStateMachine()
    {
        if (dodgeDir == 0)
            dodgeDir = 1;
        else if (dodgeDir == 1)
            dodgeDir = -1;
        else if (dodgeDir == -1)
            dodgeDir = 1;
    }

    void SnakeStateMachine()
    {
        if (snakeXDIR == 0)
            snakeXDIR = 1;
        else if (snakeXDIR == 1)
            snakeXDIR = -1;
        else if (snakeXDIR == -1)
            snakeXDIR = 1;
    }

    MapSystem.SquareValue GetNextSquare(int x, int y)
    {
        return map.GetSquareData(x, y).value;
    }
}
