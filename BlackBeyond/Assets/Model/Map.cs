﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    private readonly Space[][] map;
    private int rows;
    private int columns;

    // Create a map. TODO nothing in this map yet.
    public Map(int rows, int columns, ModelLink link)
    {
        this.rows = rows;
        this.columns = columns;
        map = new Space[rows][];
        Space tempSpace;
        for (int row = 0; row < rows; row++)
        {
            map[row] = new Space[columns];
            if(row%2==1)
            {
                for (int column = 0; column < columns; column += 2)
                {
                    if(UnityEngine.Random.Range(1, 11) == 1)
                    {
                        tempSpace = new NebulaSpace(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpace)tempSpace);
                    }
                    else
                    {
                        tempSpace = new Space(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                    }
                }
            }
            else
            {
                for (int column = 1; column < columns; column += 2)
                {
                    if (UnityEngine.Random.Range(1, 11) == 1)
                    {
                        tempSpace = new NebulaSpace(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateNebulaSpace((NebulaSpace)tempSpace);
                    }
                    else
                    {
                        tempSpace = new Space(row, column, this);
                        map[row][column] = tempSpace;
                        link.CreateSpaceView(tempSpace);
                    }
                }
            }
        }
        // Setting each space's adjacent spaces. Must be done after the map is generated
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if(map[row][column] != null)
                {
                    map[row][column].SetAdjacentSpaces();
                }
            }
        }
    }

    // Gets spaces by cooridinates. Avoid this method
    public Space GetSpace(int row, int column)
    {
        return map[row][column];
    }

    // Gives the map. Avoid this method
    public Space[][] getMap()
    {
        return map;
    }

    // Movement Methods. Only used in Space generation.
    public Space GetNE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row - 1;
        int newSpaceColumn = startSpace.Column + 1;
        if(newSpaceRow < 0 || newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public Space GetE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column + 2;
        if (newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }

    public Space GetSE(Space startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column + 1;
        if (newSpaceRow >= rows  || newSpaceColumn >= columns)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space GetSW(Space startSpace)
    {
        int newSpaceRow = startSpace.Row + 1;
        int newSpaceColumn = startSpace.Column - 1;
        if (newSpaceRow >= rows || newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space GetW(Space startSpace)
    {
        int newSpaceRow = startSpace.Row;
        int newSpaceColumn = startSpace.Column - 2;
        if (newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
    public Space GetNW(Space startSpace)
    {
        int newSpaceRow = startSpace.Row - 1;
        int newSpaceColumn = startSpace.Column - 1;
        if (newSpaceRow < 0 || newSpaceColumn < 0)
        {
            return null;
        }
        return map[newSpaceRow][newSpaceColumn];
    }
}
