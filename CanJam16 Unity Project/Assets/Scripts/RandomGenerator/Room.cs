using UnityEngine;

public class Room : MonoBehaviour
{
    // These are the X and Y coordinates of the rooms lower left tile
    public int yPosition;
    public int xPosition;

    // This is the size of the room based on the amount of tiles.
    public int roomWidth;
    public int roomHeight;

    // Direction of the corridor entering the room.
    public Direction enteringCorridor;

    // This function sets up the first room.
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        // Set the random width and height.
        roomWidth = widthRange.randomNumber;
        roomHeight = heightRange.randomNumber;

        // Set the x and y coords so the room is roughly in the middle of the game board.
        xPosition = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPosition = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
    }

    // Overload of the SetupRoom function that implements corridors
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        // Set the starting corridors direction
        enteringCorridor = corridor.direction;

        // Set random values for the width and height.
        roomWidth = widthRange.randomNumber;
        roomHeight = heightRange.randomNumber;

        switch(corridor.direction)
        {
            // If the corridor is north  bound
            case Direction.North:
                // Stop the room height going past the board 
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.endPositionY);

                // Make sure the rooms y coordinate is at the end of the corridor.
                yPosition = corridor.endPositionY;

                // Whislt the x coordinate can be random, make sure the left most possibility is no more than the width.
                xPosition = Random.Range(corridor.endPositionX - roomWidth + 1, corridor.endPositionX);

                // Clamp this to prevent the room going off the board.
                xPosition = Mathf.Clamp(xPosition, 0, columns - roomWidth);

                break;

            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.endPositionX);

                xPosition = corridor.endPositionX;

                yPosition = Random.Range(corridor.endPositionY - roomHeight + 1, corridor.endPositionY);
                yPosition = Mathf.Clamp(yPosition, 0, rows - roomHeight);

                break;

            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.endPositionY);

                yPosition = corridor.endPositionY - roomHeight + 1;

                xPosition = Random.Range(corridor.endPositionX - roomWidth + 1, corridor.endPositionX);
                xPosition = Mathf.Clamp(xPosition, 0, columns - roomWidth);

                break;

            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.endPositionX);
                xPosition = corridor.endPositionX - roomWidth + 1;

                yPosition = UnityEngine.Random.Range(corridor.endPositionY - roomHeight + 1, corridor.endPositionY);
                yPosition = Mathf.Clamp(yPosition, 0, rows - roomHeight);

                break;
        }
    }
}
