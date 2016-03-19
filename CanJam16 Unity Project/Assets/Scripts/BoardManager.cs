using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns;
    public int rows;

    public Count wallCount = new Count(5, 9);

    public GameObject exit;

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


    // Clear out the grid positions and prepares for a new board to be generated.

    void initList()
    {
        // Empty the grid position list.
        gridPositions.Clear();

        // Loop through the x axis.
        for(int x = 1; x < columns-1; x++)
        {
            // Now loop through the y axis
            for(int y = 1; y < rows-1; y++)
            {
                // At each index add a new vector three to the list with X and Y coords of the position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        // Instantiate the board and set the boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        // Loop along the x axis starting from -1 to place the floor to outerwall tiles.
        for(int x = -1; x < rows + 1; x++)
        {
            for(int y = -1; y < rows + 1; y++)
            {
                // Choosing a random tile from the array of floor tile prefabs and prepare it for instantiating.
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // Check to see if the current position is at the boards edge, if it is choose an outer wall prefab.
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosition = gridPositions[randomIndex];

        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < objectCount; i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector3 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        // Create the outer walls and floor.
        BoardSetup();

        // Reset the list.
        initList();

        // Instantiate a random number of wall.
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
    }
}