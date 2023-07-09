using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SquareData
{
    public MapSystem.SquareValue value;
    public Element elementRef;
    public SquareData(MapSystem.SquareValue _value, Element _elementRef) { value = _value; elementRef = _elementRef; }
}

public class MapSystem : MonoBehaviour
{
    public enum SquareValue { EMPTY, ANIMAL, CAR, OBSTACLE, HOLE, OUTSIDE_MAP }

    public int width;
    public int height;

    internal SquareData[][] squareMatrix;

    void Start()
    {
        InitMatrix();
        //squareMatrix[0][0] = SquareValue.EMPTY;
    }


    void InitMatrix()
    {
        squareMatrix = new SquareData[width][];

        for (int index = 0; index < width; index++)
        {
            squareMatrix[index] = new SquareData[height];

            for (int index2 = 0; index2 < height; index2++)
                squareMatrix[index][index2] = new SquareData(SquareValue.EMPTY, null);
        }
    }

    public void SetSquareValue(int x, int y, SquareData value)
    {
        if(x >= 0 && x < squareMatrix.Length && y >= 0 && y < squareMatrix[x].Length)
            squareMatrix[x][y] = value;
    }

    public SquareData GetSquareData(int x, int y)
    {
        if((x >= 0 && x < width) && (y >= 0 && y < height))
            return squareMatrix[x][y];

        return new SquareData(SquareValue.OUTSIDE_MAP, null);
    }
}
