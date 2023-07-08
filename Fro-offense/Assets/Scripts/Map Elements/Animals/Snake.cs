using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Element
{
    float moveTimer = 0;

    const float TIMER_BASE = 2;

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
        MoveStateMachine();
    }

    void RandomizeSnakeFirstMove()
    {
        int random = Random.Range(0, 10);

        if (random > 5)
            SnakeStateMachine();
    }

    void MoveStateMachine()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            moveTimer = TIMER_BASE;
            horizontalMovement = !horizontalMovement;

            if(!horizontalMovement)
            {
                VerticalMovementStateMachine();
            }
            else
            {
                HorizontalMovementStateMachine();
            }

            if (dodging)
            {
                if (!map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OUTSIDE_MAP)
                    && !map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OBSTACLE)
                    && !map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.HOLE)
                    && !map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.ANIMAL)
                    )
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + dodgeDir, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                else
                    DodgeStateMachine();
            }
        }
    }

    void VerticalMovementStateMachine()
    {
        MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x, (int)moveSystem.destinationVector.z + 1);

        switch (nextSquareValue)
        {
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
    }

    void EnableDodgeMode()
    {
        if (!dodging)
        {
            dodging = true;
            DodgeStateMachine();
            SnakeStateMachine();
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
        return map.GetSquareValue(x, y);
    }
}
