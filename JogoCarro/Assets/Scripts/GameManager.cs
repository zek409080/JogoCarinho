using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]TextMeshProUGUI text;
    public static GameManager Instance;
    [SerializeField]  GameObject panel;
    public GameObject chegada;
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        Time.timeScale = 1f;
        StartCoroutine(AtivarLinhaDeChegada());
        chegada.SetActive(false);
    }
    const string playerPrefabPath = "Prefabs/Player";

    int playersInGame;
    List<PlayerControler> playerList = new List<PlayerControler>();
    PlayerControler playerLocal;

    private void Start()
    {
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
    }
    private void CreatePlayer()
    {
        PlayerControler player = NetworkManager.instance.Instantiate(playerPrefabPath, new Vector3(0, -3, 0), Quaternion.identity).GetComponent<PlayerControler>();
        player.photonView.RPC("Initialize", RpcTarget.All);
    }

    [PunRPC]
    private void AddPlayer()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }
    [PunRPC]
    public void GameOver() 
    {
        if (photonView.IsMine)
        {
            text.text = "parabéns você Ganhou"; 
        }
        else
        {
            text.text = "Infelizmente você Perdeu";
        }
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
    IEnumerator AtivarLinhaDeChegada()
    {
        yield return new WaitForSeconds(16f);
        chegada.SetActive(true);
    }

}
