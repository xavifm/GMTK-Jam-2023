using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportCar : Element
{
    int turns = 0;
    int carxDIR = 1;

    int carVerticalDir = 0;

    const float TIMER_BASE = 2;
    const int MAX_HORIZONTAL_TURNS = 2;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    protected override void MoveStateMachine()
    {
        base.MoveStateMachine();

        if (moveTimer <= 0)
        {
            turns++;
            moveTimer = TIMER_BASE;

            if(turns <= MAX_HORIZONTAL_TURNS)
            {
                HorizontalMovement();
                transform.rotation = Quaternion.Euler(0, 90 * carxDIR, 0);
            }
            else
            {
                VerticalMovement();
                transform.rotation = Quaternion.Euler(0, 90 * carVerticalDir, 0);
                turns = 0;
            }
        }
    }

    void HorizontalMovement()
    {
        MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x + carxDIR, (int)moveSystem.destinationVector.z);
        Debug.Log(nextSquareValue.ToString());

        switch (nextSquareValue)
        {
            case MapSystem.SquareValue.EMPTY:
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + carxDIR, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                break;
            case MapSystem.SquareValue.ANIMAL:
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + carxDIR, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                break;
            case MapSystem.SquareValue.OBSTACLE:
                SwitchCarHorizontalDir();
                break;
            case MapSystem.SquareValue.HOLE:
                SwitchCarHorizontalDir();
                break;
            case MapSystem.SquareValue.OUTSIDE_MAP:
                SwitchCarHorizontalDir();
                break;
        }
    }

    void VerticalMovement()
    {
        CarStateMachine();

        MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x, (int)moveSystem.destinationVector.z + carVerticalDir);

        switch (nextSquareValue)
        {
            case MapSystem.SquareValue.EMPTY:
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, moveSystem.destinationVector.y, moveSystem.destinationVector.z + carVerticalDir);
                break;
            case MapSystem.SquareValue.ANIMAL:
                moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, moveSystem.destinationVector.y, moveSystem.destinationVector.z + carVerticalDir);
                break;
        }
    }

    void CarStateMachine()
    {
        if (carVerticalDir == 0)
            carVerticalDir = 1;
        else if (carVerticalDir == 1)
            carVerticalDir = -1;
        else if (carVerticalDir == -1)
            carVerticalDir = 1;
    }

    void SwitchCarHorizontalDir()
    {
        if (carxDIR == 1)
            carxDIR = -1;
        else if (carxDIR == -1)
            carxDIR = 1;
    }

    MapSystem.SquareValue GetNextSquare(int x, int y)
    {
        return map.GetSquareValue(x, y);
    }
}
