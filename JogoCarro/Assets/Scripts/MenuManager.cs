// Importa as bibliotecas necess�rias para collections, Unity Engine e UI.
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Define a classe MenuManager que herda de MonoBehaviour, permitindo que seja usada como um componente em GameObjects.
public class MenuManager : MonoBehaviour
{
    // Define vari�veis privadas que podem ser configuradas no Inspector do Unity.
    // Essas vari�veis s�o refer�ncias para elementos de UI.
    [SerializeField] TextMeshProUGUI nicknameUI, roomNameUI, playerList;
    [SerializeField] Button joinButton, createButton, leaveButton, startButton;
    [SerializeField] GameObject menu, lobby;

    // Declara uma inst�ncia est�tica da classe MenuManager
    public static MenuManager instance;

    // M�todo chamado quando o script � inicializado.
    private void Awake()
    {
        #region Singleton
        // Verifica se a inst�ncia � nula
        if (instance == null)
        {
            instance = this; // Define a inst�ncia para este objeto
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroi o objeto se j� houver uma inst�ncia existente
        }
        #endregion

        // Adiciona listeners aos bot�es, associando m�todos aos eventos de clique.
        joinButton.onClick.AddListener(JoinRoom);
        createButton.onClick.AddListener(CreateRoom);

        // Define os bot�es joinButton e createButton como n�o interativos inicialmente.
        joinButton.interactable = false;
        createButton.interactable = false;

        // Chama o m�todo SwitchWindow para exibir o menu inicial.
        SwitchWindow(false);
    }

    // M�todo p�blico que habilita os bot�es joinButton e createButton quando conectado.
    public void Connected()
    {
        joinButton.interactable = true;
        createButton.interactable = true;
    }

    // M�todo p�blico que atualiza a lista de jogadores na interface.
    public void UpdatePlayerList(string list)
    {
        playerList.text = list;
    }

    // M�todo privado que � chamado quando joinButton � clicado.
    private void JoinRoom()
    {
        // Chama o m�todo JoinRoom no NetworkManager para se juntar a uma sala.
        NetworkManager.instance.JoinRoom(roomNameUI.text, nicknameUI.text);
        // Alterna para a janela do lobby.
        SwitchWindow(true);
    }

    // M�todo privado que � chamado quando createButton � clicado.
    private void CreateRoom()
    {
        // Chama o m�todo CreateRoom no NetworkManager para criar uma sala.
        NetworkManager.instance.CreateRoom(roomNameUI.text, nicknameUI.text);
        // Alterna para a janela do lobby.
        SwitchWindow(true);
    }

    // M�todo privado que � chamado quando leaveButton � clicado.
    public void LeaveRoom()
    {
        // Chama o m�todo LeaveRoom no NetworkManager para sair da sala.
        NetworkManager.instance.LeaveRoom();
        // Alterna para a janela do menu.
        SwitchWindow(false);
    }

    // M�todo p�blico que inicia o jogo, carregando a cena especificada.
    public void StartGame(string sceneName)
    {
        NetworkManager.instance.LoadScene(sceneName);
    }

    // M�todo p�blico que define se o bot�o startButton est� interativo com base no valor de isMaster.
    public void SetStartButton(bool isMaster)
    {
        startButton.interactable = isMaster;
    }

    // M�todo p�blico que alterna entre o menu e a janela do lobby.
    public void SwitchWindow(bool onLobby)
    {
        // Define a visibilidade dos GameObjects menu e lobby.
        menu.SetActive(!onLobby);
        lobby.SetActive(onLobby);
    }
}
