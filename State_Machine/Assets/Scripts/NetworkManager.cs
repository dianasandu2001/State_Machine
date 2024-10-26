using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> roomsList; //This is a list of rooms we get from the cloud
    private const string roomNamePrefix = "MyRoom"; // e.g. MyRoom929347923 Every room needs a unique name.
    public GUIStyle myStyle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myStyle.fontSize = 18;
        myStyle.normal.textColor = Color.red;

        roomsList = new List<RoomInfo>(); //empty list of rooms


        //Let's connect to the cloud
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.IsConnectedAndReady.ToString(), myStyle);
        GUILayout.Label(PhotonNetwork.InLobby.ToString(), myStyle);
        if (PhotonNetwork.CurrentRoom != null)
        {
            GUILayout.Label(PhotonNetwork.CurrentRoom.Name.ToString(), myStyle);
        }

        if(PhotonNetwork.InRoom == false)
        {
            //if we are not in room, show all avaliable rooms and create room button
            if(GUI.Button(new Rect(200, 100, 250, 100), "Create Room"))
            {
                // we create room with unique name
                PhotonNetwork.CreateRoom(roomNamePrefix + System.Guid.NewGuid().ToString());
            }
        }

        // we list all avaliable rooms, we make list only if there are more than 0 rooms
        if(roomsList.Count != 0)
        {
            //make a button using for each
            int i = 0;
            foreach(RoomInfo room in roomsList)
            {
                if(GUI.Button(new Rect(200, 250 + 110 * i, 250, 100), "Join: " + room.Name))
                {
                    //join the room
                    PhotonNetwork.JoinRoom(room.Name);
                }
                i++;
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // we update local roomslist with the infor that's cimming from the cloud
        roomsList = roomList;
    }

    public override void OnConnectedToMaster()
    {
        // We connected to cloud, let's go to Lobby
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("PlayerBox", new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
    }
}
