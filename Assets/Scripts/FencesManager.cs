using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencesManager : MonoBehaviour
{
    // Tilemaps with fences
    public GameObject tilemapFences1; 
    public GameObject tilemapFences2; 
    public GameObject tilemapFences3; 
    public GameObject tilemapFences4; 
    public GameObject tilemapFences5; 
    public GameObject tilemapFences6; 

    private int indexFence;

    // Witch
    public GameObject witch;

    // Possible witch positions
    private static Vector3[] witchPositions = 
    {
        new Vector3(-73, 23, -1),   //1     0
        new Vector3(-58, 18, -1),   //1     1
        new Vector3(-27, 21, -1),   //2     2
        new Vector3(-33, 10, -1),   //2     3
        new Vector3(-44, -2, -1),   //3     4
        new Vector3(-39, -21, -1),   //4    5
        new Vector3(-30, -26, -1),   //4   6
        new Vector3(-51, -26, -1),   //5    7
        new Vector3(-91, -9, -1),   //free  8
        new Vector3(-89, 5, -1)   //free    9
    };
    private static Vector3 witchStartPosition = new Vector3(-54, -8, -1);

    void Start()
    {
        indexFence = 0;
    }

    public void GetWitchToStartingPosition()
    {
        witch.transform.position = witchStartPosition;
        tilemapFences1.SetActive(false);
        tilemapFences2.SetActive(false);
        tilemapFences3.SetActive(false);
        tilemapFences4.SetActive(false);
        tilemapFences5.SetActive(false);
        tilemapFences6.SetActive(false);
    }

    // Sets the area to be fenced after 5 loops
    public void SetNewFenceArea()
    {
        indexFence = Random.Range(1, 7);

        if (indexFence == 1)
        {
            tilemapFences1.SetActive(true);
        }
        else if (indexFence == 2)
        {
            tilemapFences2.SetActive(true);
        }
        else if (indexFence == 3)
        {
            tilemapFences3.SetActive(true);
        }
        else if (indexFence == 4)
        {
            tilemapFences4.SetActive(true);
        }
        else if (indexFence == 5)
        {
            tilemapFences5.SetActive(true);
        }
        else if (indexFence == 6)
        {
            tilemapFences6.SetActive(true);
        }

        SetNewWitchPosition(indexFence);
    }

    private void SetNewWitchPosition(int indexWitch)
    {
        if (indexFence == 1)
        {
            indexWitch = Random.Range(2, 10);
        }
        else if (indexFence == 2)
        {
            indexWitch = Random.Range(4, 10);
        }
        else if (indexFence == 3)
        {
            indexWitch = Random.Range(5, 10);
        }
        else if (indexFence == 4)
        {
            indexWitch = Random.Range(0, 5);
        }
        else if (indexFence == 5)
        {
            indexWitch = Random.Range(0, 7);
        }

        witch.transform.position = witchPositions[indexWitch];
    }
}