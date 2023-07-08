using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Element
{
    float moveTimer = 0;
    int carxDIR = 1;

    const float TIMER_BASE = 2;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
        MoveStateMachine();
    }

    void MoveStateMachine()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            moveTimer = TIMER_BASE;
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
                    SwitchCarDir();
                    break;
                case MapSystem.SquareValue.HOLE:
                    SwitchCarDir();
                    break;
                case MapSystem.SquareValue.OUTSIDE_MAP:
                    SwitchCarDir();
                    break;
            }
        }
    }

    void SwitchCarDir()
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
