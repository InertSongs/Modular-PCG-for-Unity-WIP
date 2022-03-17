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
        //Build each room's origin
        makeOrigins();
        for(int i=0;i<maxRooms;i++)
        {
            GameObject newRoom = Instantiate(room);
            newRoom.GetComponent<IRoom>().BuildRoom(origins[i], dimensions[i]);
        }
    }
    void makeOrigins()
    {
        //Add origin 0 with dimensions
        origins.Add(new Vector3(0, 0, 0));
        dimensions.Add(DimShuffle());
        //Add subsequent origins with dimensions
        for (int i = 1; i < maxRooms; i++)
        {
            //Get new dimensions for this room
            List<int> newDims = DimShuffle();
            //Decide which wall of the previous room the origin of the new room is based on
            List<int> flips = new List<int>() { Random.Range(0, 1), Random.Range(0, 1) }; 
            //Initialize origin vector
            Vector3 origin = Vector3.zero;
            if (flips[0] == 0)
            {
                //Choose random height value along previous room's east or west wall
                origin.x = Random.Range((int)(origins[i - 1].x + 1 - newDims[0]), (int)(origins[i - 1].x + dimensions[i - 1][0] - 1));
                //Adjust origin's z value to accomodate whether it's on the east or west wall
                origin.z = flips[1] == 0 ? -newDims[1] : origin.z = dimensions[i - 1][1];
            }
            else
            {
                //Choose random width value along previous room's north or south wall
                origin.z = Random.Range((int)(origins[i - 1].z + 1 - newDims[1]), (int)(origins[i - 1].z + dimensions[i - 1][1] - 1));
                //Adjust origin's x value to accomodate whether it's on the north or south wall
                origin.x = flips[1] == 0 ? -newDims[0] : dimensions[i - 1][0];
            }
            dimensions.Add(newDims);
            origins.Add(origin);
        }
    }
    //Function to generate new random dimensions in a range
    List<int> DimShuffle() => new List<int>() { Random.Range(minH, maxH), Random.Range(minW, maxW) };
}
