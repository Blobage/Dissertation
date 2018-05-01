using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Generation : MonoBehaviour
{
    [SerializeField]
    public int iterations;/// <summary>
    ///  Cannot be any less than 1 as this 
    /// </summary>

    public Object[] tiles;
    public List<GameObject> placedTiles;
    public List<GameObject> placedTemp;
    public GameObject startNode;
    public List<GameObject> exits;
    public List<GameObject> exitsTemp;
    // Use this for initialization
    void Start ()
    {
        tiles = Resources.LoadAll("Tiles");
        placedTiles.Add(startNode);
        if(iterations < 1)
        {
            iterations = 1;
            Vector3 lastNodePos = startNode.transform.position;

            int c_Count = 0;
            for (int i = 0; i < startNode.transform.childCount; i++)
            {
                if (startNode.transform.GetChild(i).tag == "Exit")
                {
                    c_Count += 1;
                }
            }
            TilePlace(c_Count, lastNodePos);
        }
        else
        {
            Vector3 lastNodePos = startNode.transform.position;

            int c_Count = 0;
            for (int i = 0; i < startNode.transform.childCount; i++)
            {
                if (startNode.transform.GetChild(i).tag == "Exit")
                {
                    c_Count += 1;
                }
            }
            TilePlace(c_Count, lastNodePos);
            ErrorChecking();
        }
    }
    /// <summary>
    /// This method starts by taking in the original nodes exits as a base.
    /// It then goes through the exits attached to the tile and depending on if
    /// the exit is not used and that the tile number is less than 2 it will 
    /// place a new tile
    /// </summary>
    /// <param name="exitCount"></param>
    /// <param name="l_N_Pos"></param>
	void TilePlace(int exitCount, Vector3 l_N_Pos)
    {
        //For each exit attached to a node, place a tile, if tile overlaps a old tile then remove node
        for (int i = 1; i <= exitCount; i++)
        {
            exits.Add(startNode.transform.GetChild(i).gameObject);
        }
        ///Set amount of iterations beforehand 
        for(int j = 0; j < iterations; j++)
        {
            Vector3 newPos = new Vector3(); // Stores the value of the new position temporarily
            foreach (GameObject exit in exits)
            {
                int number = Mathf.RoundToInt(Random.Range(0, tiles.Length));//Random tile to be placed
                
                int e_CountTemp = 0;
                if (exit.GetComponent<Exits>().used == true)
                {
                    continue;
                }
                else if (exit.GetComponent<Exits>().used != true)
                {
                    if (number != 0)
                    {
                        //Check the exits position in the world and place a tile based on that
                        if (exit.transform.position.z < exit.transform.parent.position.z)
                        {
                            newPos.z = exit.transform.position.z - 5f;
                            GameObject tile = (GameObject)Instantiate(tiles[number], newPos, transform.rotation);
                            placedTiles.Add(tile);
                            exit.GetComponent<Exits>().used = true;
                            //Repeat of the original finding exits
                            for (int i = 0; i < tile.transform.childCount; i++)
                            {
                                if (tile.transform.GetChild(i).tag == "Exit")
                                {
                                    e_CountTemp += 1;
                                }
                            }
                            for (int i = 1; i <= e_CountTemp; i++)
                            {
                                exitsTemp.Add(tile.transform.GetChild(i).gameObject);
                            }
                            if (tile == tiles[2])
                            {
                                while (tile.GetComponentInChildren<Exits>().used != true)
                                {
                                    tile.transform.Rotate(new Vector3(0, 90, 0));
                                }
                            }
                        }
                        else if (exit.transform.position.z > exit.transform.parent.position.z)
                        {
                            newPos.z = exit.transform.position.z + 5f;
                            GameObject tile = (GameObject)Instantiate(tiles[number], newPos, transform.rotation);
                            placedTiles.Add(tile);
                            exit.GetComponent<Exits>().used = true;
                            //Repeat of the original finding exits
                            for (int i = 0; i < tile.transform.childCount; i++)
                            {
                                if (tile.transform.GetChild(i).tag == "Exit")
                                {
                                    e_CountTemp += 1;
                                }
                            }
                            for (int i = 1; i <= e_CountTemp; i++)
                            {
                                exitsTemp.Add(tile.transform.GetChild(i).gameObject);
                            }
                            if (tile == tiles[2])
                            {
                                while (tile.GetComponentInChildren<Exits>().used != true)
                                {
                                    tile.transform.Rotate(new Vector3(0, 90, 0));
                                }
                            }
                        }
                        else if (exit.transform.position.x < exit.transform.parent.position.z)
                        {
                            newPos.x = exit.transform.position.x - 5f;
                            GameObject tile = (GameObject)Instantiate(tiles[number], newPos, transform.rotation);
                            placedTiles.Add(tile);
                            exit.GetComponent<Exits>().used = true;
                            //Repeat of the original finding exits
                            for (int i = 0; i < tile.transform.childCount; i++)
                            {
                                if (tile.transform.GetChild(i).tag == "Exit")
                                {
                                    e_CountTemp += 1;
                                }
                            }
                            for (int i = 1; i <= e_CountTemp; i++)
                            {
                                exitsTemp.Add(tile.transform.GetChild(i).gameObject);
                            }
                            if (tile == tiles[2])
                            {
                                while (tile.GetComponentInChildren<Exits>().used != true)
                                {
                                    tile.transform.Rotate(new Vector3(0, 90, 0));
                                }
                            }
                        }
                        else if (exit.transform.position.x > exit.transform.parent.position.z)
                        {
                            newPos.x = exit.transform.position.x + 5f;
                            GameObject tile = (GameObject)Instantiate(tiles[number], newPos, transform.rotation);
                            placedTiles.Add(tile);
                            exit.GetComponent<Exits>().used = true;
                            //Repeat of the original finding exits
                            for (int i = 0; i < tile.transform.childCount; i++)
                            {
                                if (tile.transform.GetChild(i).tag == "Exit")
                                {
                                    e_CountTemp += 1;
                                }
                            }
                            for (int i = 1; i <= e_CountTemp; i++)
                            {
                                exitsTemp.Add(tile.transform.GetChild(i).gameObject);
                            }
                            if (tile == tiles[2])
                            {
                                while (tile.GetComponentInChildren<Exits>().used != true)
                                {
                                    tile.transform.Rotate(new Vector3(0, 90, 0));
                                }
                            }
                        }
                    }
                }
            }
            l_N_Pos = newPos;
            exits.Clear();
            exits.AddRange(exitsTemp);
            exitsTemp.Clear();
        } 
            
       
    }	
    /// <summary>
    /// The Error handeling of the tiles placed in the world
    /// </summary>
    void ErrorChecking()
    {
        //Debug.Log("Placed Tiles" + placedTiles.Count);
        int temp = placedTiles.Count;
        placedTemp.AddRange(placedTiles);
        for (int i = 0; i < temp; i++)
        {
            foreach (GameObject tile in placedTiles)
            {
                for (int j = 0; j < placedTemp.Count-1; j++)
                {
                    if (tile.GetInstanceID() != placedTemp[j].GetInstanceID())
                    {
                        if (tile.transform.position == placedTemp[j].transform.position)
                        {
                            if (placedTiles[j].activeSelf == true && tile.activeSelf == true)
                            {
                                placedTiles[j].SetActive(false);
                            }
                            if(placedTiles[j].activeSelf == false && tile.activeSelf == false)
                            {
                                placedTiles[j].SetActive(true);
                            }
                        }
                    }                 
                }
            }
        }
    }
}
