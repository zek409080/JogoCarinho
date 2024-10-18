using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nicknameUI, roomNameUI, playerList;
    [SerializeField] Button joinButton, createButton, leaveButton, startButton;
    [SerializeField] GameObject menu, lobby;
    public static MenuManager instance;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this; 
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }
        #endregion
        joinButton.onClick.AddListener(JoinRoom);
        createButton.onClick.AddListener(CreateRoom);

        joinButton.interactable = false;
        createButton.interactable = false;

        SwitchWindow(false);
    }
    public void Connected()
    {
        joinButton.interactable = true;
        createButton.interactable = true;
    }
    public void UpdatePlayerList(string list)
    {
        playerList.text = list;
    }
    private void JoinRoom()
    {
        NetworkManager.instance.JoinRoom(roomNameUI.text, nicknameUI.text);
        SwitchWindow(true);
    }
    private void CreateRoom()
    {
        NetworkManager.instance.CreateRoom(roomNameUI.text, nicknameUI.text);
        SwitchWindow(true);
    }
    public void LeaveRoom()
    {
        NetworkManager.instance.LeaveRoom();
        SwitchWindow(false);
    }
    public void StartGame(string sceneName)
    {
        NetworkManager.instance.LoadScene(sceneName);
    }
    public void SetStartButton(bool isMaster)
    {
        startButton.interactable = isMaster;
    }
    public void SwitchWindow(bool onLobby)
    {
        menu.SetActive(!onLobby);
        lobby.SetActive(onLobby);
    }
}
