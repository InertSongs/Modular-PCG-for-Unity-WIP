using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject room;
    int maxH = 10, maxW = 10, minH = 5, minW = 5;
    List<Vector3> origins = new List<Vector3>();
    List<List<int>> dimensions = new List<List<int>>();
    int maxRooms = 2;
    void Start()
    {
        makeOrigins();
        for(int i=0;i<maxRooms;i++)
        {
            GameObject newRoom = Instantiate(room);
            newRoom.GetComponent<GridBrain>().BuildRoom(origins[i], dimensions[i]);
        }
    }
    void makeOrigins()
    {
        origins.Add(new Vector3(0, 0, 0));
        dimensions.Add(DimShuffle());
        for (int i = 1; i < maxRooms; i++)
        {
            List<int> newDims = DimShuffle();
            int flipOne = Random.Range(0,1);
            int flipTwo = Random.Range(0,1);
            Vector3 origin = new Vector3(0,0,0);
            if (flipOne == 0)
            {
                int minX = (int)(origins[i - 1].x + 1 - newDims[0]);
                int maxX = (int)(origins[i - 1].x + dimensions[i - 1][0] - 1);
                origin.x = Random.Range(minX,maxX);
                if(flipTwo == 0)
                {
                    origin.z = -newDims[1];
                }
                else
                {
                    origin.z = dimensions[i - 1][1];
                }
            }
            else
            {
                int minZ = (int)(origins[i - 1].z + 1 - newDims[1]);
                int maxZ = (int)(origins[i - 1].z + dimensions[i - 1][1] - 1);
                origin.z = Random.Range(minZ, maxZ);
                if (flipTwo == 0)
                {
                    origin.x = -newDims[0];
                }
                else
                {
                    origin.x = dimensions[i - 1][0];
                }
            }
            dimensions.Add(newDims);
            origins.Add(origin);
        }
    }
    List<int> DimShuffle()
    {
        return new List<int>() { Random.Range(minH, maxH), Random.Range(minW, maxW) };
    }
    int FlipSign()
    {
        List<int> source = new List<int>() { -1, 1 };
        return source[Random.Range(0, 1)];
    }
    
}
