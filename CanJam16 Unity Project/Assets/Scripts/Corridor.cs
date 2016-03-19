using UnityEngine;
using System;

// Enum to specify the corridors direction
public enum Direction
{
    North,
    East,
    South,
    West
}

public class Corridor : MonoBehaviour
{
    public int xStart; // The X coordinate where the corridor starts.
    public int yStart; // The X coordinate where the corridor starts.

    public int corridorLength; // How many tiles long the corridor is.

    public Direction direction; // Which direction is the corridor moving

    public int endPositionX
    {
        get
        {
            if(direction == Direction.North || direction == Direction.South)
            {
                return xStart;
            }
            if(direction == Direction.East)
            {
                return xStart + corridorLength - 1;
            }
            return xStart - corridorLength + 1;
        }
    }

    public int endPositionY
    {
        get
        {
            if(direction == Direction.East || direction == Direction.West)
            {
                return yStart;
            }
            if(direction == Direction.North)
            {
                return yStart + corridorLength - 1;
            }
            return yStart - corridorLength + 1;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SetupCorridor(Room room, IntRange corridorLength, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        // Set a random direction
        direction = (Direction)UnityEngine.Random.Range(0, 4);

        /*
        This section of code finds the direction opposite the one at the rooms exit. This then casts the previous direction
        to an integer between 0 and 3. It then adds 2 to this number allowing a number between 2 and 5. It will then calculate 
        the remainder by dividing it by 4 and casts the number back to a direction.
        */

        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);

        // If this direction is not the first corridor and the direction is opposite to the previous direction
        if(!firstCorridor && direction == oppositeDirection)
        {
            //This will rotate the direction 90 degrees clockwise.
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        // Set a random length.
        corridorLength = length.Random;

        int maxLength = length.maximumValue;

        switch(direction)
        {
            case Direction.North:
                xStart = UnityEngine.Random.Range(room.xPosition, room.xPosition + room.roomWidth - 1);

                yStart = room.yPosition + room.roomHeight;

                maxLength = rows - yStart - roomHeight.minimumValue;
                break;

            case Direction.East:
                xStart = room.xPosition + room.roomWidth;
                yStart = UnityEngine.Random.Range(room.yPosition, room.yPosition + room.roomHeight - 1);
                maxLength = columns - xStart - roomWidth.minimumValue;
                break;

            case Direction.West:
                xStart = room.xPosition;
                yStart = UnityEngine.Random.Range(room.yPosition, room.yPosition + room.roomHeight);
                maxLength = xStart - roomWidth.minimumValue;
                break;
        }

        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}
