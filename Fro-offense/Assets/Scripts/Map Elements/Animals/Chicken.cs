using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Element
{
    const float TIMER_BASE = 2;

    [SerializeField] bool dodging = false;
    int dodgeDir = 0;

    const float POSY_THRESHOLD = 1;

    private void Start()
    {
        base.Start();
        moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, originalYPos, moveSystem.destinationVector.z);
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
            moveTimer = TIMER_BASE;
            MapSystem.SquareValue nextSquareValue = GetNextSquare((int)moveSystem.destinationVector.x, (int)moveSystem.destinationVector.z + 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log(nextSquareValue.ToString());

            switch (nextSquareValue)
            {
                case MapSystem.SquareValue.OUTSIDE_MAP:
                    GameManager.Instance.ChangeGameState(GameState.LOSE_GAME);
                    break;
                case MapSystem.SquareValue.EMPTY:
                    dodging = false;
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, originalYPos, moveSystem.destinationVector.z + 1);
                    break;
                case MapSystem.SquareValue.HOLE:
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x, originalYPos + POSY_THRESHOLD, moveSystem.destinationVector.z + 1);
                    break;
                case MapSystem.SquareValue.OBSTACLE:
                    EnableDodgeMode();
                    break;
                case MapSystem.SquareValue.ANIMAL:
                    EnableDodgeMode();
                    break;
            }

            if (dodging)
            {
                if (!map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OUTSIDE_MAP)
                    && !map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.OBSTACLE)
                    && !map.GetSquareValue((int)(moveSystem.destinationVector.x + dodgeDir), (int)moveSystem.destinationVector.z).Equals(MapSystem.SquareValue.ANIMAL)
                    )
                    moveSystem.destinationVector = new Vector3(moveSystem.destinationVector.x + dodgeDir, moveSystem.destinationVector.y, moveSystem.destinationVector.z);
                else
                    DodgeStateMachine();
            }
        }
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

    MapSystem.SquareValue GetNextSquare(int x, int y)
    {
        return map.GetSquareValue(x, y);
    }
}
