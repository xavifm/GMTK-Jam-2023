using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public enum SquareValue { EMPTY, ANIMAL, CAR, OBSTACLE, OUTSIDE_MAP }

    public int width;
    public int height;

    internal SquareValue[][] squareMatrix;

    void Start()
    {
        InitMatrix();
        squareMatrix[0][0] = SquareValue.ANIMAL;
    }


    void InitMatrix()
    {
        squareMatrix = new SquareValue[width][];

        for (int index = 0; index < width; index++)
        {
            squareMatrix[index] = new SquareValue[height];

            for (int index2 = 0; index2 < height; index2++)
                squareMatrix[index][index2] = SquareValue.EMPTY;
        }
    }

    public void SetSquareValue(int x, int y, SquareValue value)
    {
        squareMatrix[x][y] = value;
    }

    public SquareValue GetSquareValue(int x, int y)
    {
        if((x >= 0 && x < width) && (y >= 0 && y < height))
            return squareMatrix[x][y];

        return SquareValue.OUTSIDE_MAP;
    }
}
