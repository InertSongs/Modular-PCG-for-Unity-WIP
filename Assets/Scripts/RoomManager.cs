using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject room;
    List<GameObject> allRooms = new List<GameObject>();
    int maxH = 10, maxW = 10, minH = 5, minW = 5;
    List<Vector3> origins = new List<Vector3>();
    List<List<int>> dimensions = new List<List<int>>();
    int maxRooms = 1;
    void Start()
    {
        for(int i=0;i<maxRooms;i++)
        {
            GameObject newRoom = Instantiate(room);
            newRoom.GetComponent<GridBrain>().BuildRoom(origins[i],dimensions[i]);
        }
    }
    void makeOrigins()
    {
        origins.Add(new Vector3(0, 0, 0));
        dimensions.Add(DimShuffle());
        for (int i = 1; i <= maxRooms; i++)
        {
            List<int> prevDims = dimensions[i - 1];
            Vector3 prevCenter = new Vector3(prevDims[0], 0, prevDims[1]) / 2;
            List<Vector3> mayDoor = new List<Vector3>()
            {
                new Vector3(Random.Range(origins[i-1].x-prevCenter.x+1,prevDims[0]-prevCenter.x-1),0,FlipSign()*prevCenter.z)+ prevCenter
            };
            List<int> newDims = DimShuffle();
            
        }
        //Make a random height and width
        //pick a mayDoor point in last room
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
