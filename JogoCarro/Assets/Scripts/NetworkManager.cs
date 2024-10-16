using UnityEngine;
using Photon.Pun; 
using Photon.Realtime; 

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Singleton

    public static NetworkManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }
    }
    #endregion


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected Successful"); 

        MenuManager.instance.Connected(); 
    }

    public void JoinRoom(string roomName, string nickname)
    {
        PhotonNetwork.NickName = nickname; 
        PhotonNetwork.JoinRoom(roomName); 
    }

    public void CreateRoom(string roomName, string nickname)
    {
        PhotonNetwork.NickName = nickname; 
        PhotonNetwork.CreateRoom(roomName); 
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); 
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player " + newPlayer.NickName + " joined room"); 
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room"); 
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); 
        MenuManager.instance.SetStartButton(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player " + PhotonNetwork.NickName + " joined room"); 
        MenuManager.instance.UpdatePlayerList(GetPlayerList()); 
        MenuManager.instance.SetStartButton(PhotonNetwork.IsMasterClient); 
    }

    public void LoadScene(string sceneName)
    {
        photonView.RPC("LoadSceneRPC", RpcTarget.All, sceneName); 
    }

    [PunRPC]
    private void LoadSceneRPC(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName); 
    }

    public GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(prefabName, position, rotation);
    }

    public string GetPlayerList()
    {
        string list = ""; 

        foreach (var player in PhotonNetwork.PlayerList)
        {
            list += player + "\n"; 
        }

        return list; 
    }
}
