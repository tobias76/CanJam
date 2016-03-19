using UnityEngine;

public class BoardCreator : MonoBehaviour {


    public enum TileType
    {
        Wall,
        Floor
    }

    // This is the number of columns and rows on the board (How tall and wide it'll be)
    public static int columns = 100;
    public static int rows = 100;

    //These numbers are variables regarding the number, width and height of a room.
    public IntRange numberOfRooms = new IntRange(15, 20);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10);

    // Prefab Arrays
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWalls;

    public GameObject[] player;

    private TileType[][] tile;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;


    // Use this for initialization
    void Start ()
    {
        //On start create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();
        CreateRoomsAndCorridors();
        roomTileValueSetter();
        corridorTileValueSetter();

        instantiateTiles();
        instantiateOuterWalls();
    }


    private void instantiateOuterWalls()
    {

        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both of the vertical walls (LOL WORLDS FLAT CONSPIRACY)
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both of the horizontal walls, y'know the ones at the left and right
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    private void SetupTilesArray()
    {
        // Makes sure that the tiles are the correct width
        tile = new TileType[columns][];

        // Goes through all the tile arrays
        for(int i = 0; i < tile.Length; i++)
        {
            tile[i] = new TileType[rows];
        }

    }

    private void CreateRoomsAndCorridors()
    {
        // Create an array for the rooms with a random size
        rooms[0] = new Room();
        corridors[0] = new Corridor();

        // Setup the first room
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        // Setup the first corridor
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for(int i = 1; i < rooms.Length; i++)
        {
            // Create a room
            rooms[i] = new Room();

            // Setup the created based on the previous corridor.
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            // If there are still items in the corridors array
            if(i < corridors. Length)
            {
                // Create a corridor
                corridors[i] = new Corridor();

                // Setup aforementioned corridor based on the previously created room.
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            if(i == rooms.Length * .5f)
            {
                Vector3 playerPosition = new Vector3(rooms[i].xPosition, rooms[i].yPosition, 0);
                Instantiate(player, Quaternion.identity);
            }
        }
    }

    private void corridorTileValueSetter()
    {
        // Go through the corridor array
        for(int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            // Check the length
            for(int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.xStart;
                int yCoord = currentCorridor.yStart;

                // Add / Subtract from the co-ords dependent on state of the loop.
                switch(currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }
                // Set the tile to the coordinates determined above
                tile[xCoord][yCoord] = TileType.Floor;
            }
        }
    }

    private void roomTileValueSetter()
    {
        // Go through all the rooms in the array
        for(int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];

            //Check for each rooms width
            for(int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoordinate = currentRoom.xPosition = j;

                for(int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoordinate = currentRoom.yPosition = k;

                    // Make sure that the coordinates in the jagged array are based on the rooms position, width and height
                    tile[xCoordinate][yCoordinate] = TileType.Floor;
                }
            }
        }
    }

    void instantiateTiles()
    {
        // Go through all tiles in the jagged array
        for (int i = 0; i < tile.Length; i++)
        {
            for(int j = 0; j < tile[i].Length; j++)
            {
                InstantiateFromArray(floorTiles, i, j);

                if (tile[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }

    private void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Take the starting value for Y and start the loop there... at the start.
        float currentY = startingY;

        // While the Y value is less than the end value
        while (currentY <= endingY)
        {
            // Do the ol'instantiate on the outer wall tile at the x coordinate and the current Y coordinate
            InstantiateFromArray(outerWalls, xCoord, currentY);

            currentY++;
        }
    }

    private void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X
        float currentX = startingX;

        // While the X value is less than the ending value
        while(currentX <= endingX)
        {
            InstantiateFromArray(outerWalls, currentX, yCoord);

            currentX++;
        }

    }

    private void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {

        // Create the arrays random index.
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

        // Instantiate the position based on the coordinates
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab based on the random index given.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tiles parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }
}
