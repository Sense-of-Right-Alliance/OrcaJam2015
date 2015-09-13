using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ballbusters: legend of the silver surfer
public enum Direction
{
    North,
    East,
    South,
    West,
}

public static class Extensions
{
    public static Direction RotateClockwise(this Direction dir, int times)
    {
        return (Direction)(((int)dir + times) % 4);
    }

    public static Direction Reverse(this Direction dir)
    {
        return dir.RotateClockwise(2);
    }
}