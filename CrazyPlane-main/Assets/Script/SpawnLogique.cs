using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnitySocketIO;
using UnitySocketIO.Events;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "PlayerData", order = 1)]
public class SpawnLogique : MonoBehaviour
{
    [SerializeField] List<GameObject> PlacementPlayer = new List<GameObject>();
    [SerializeField] List<PlayerData> PlayerOutGame = new List<PlayerData>();
    [SerializeField] List<PlayerData> PlayerInGame = new List<PlayerData>();
    [SerializeField] List<Material> MaterialPlayer = new List<Material>();
    [SerializeField] GameObject planeAdd;
    [SerializeField] public Vector3 LastCommandeDirection = new Vector3 (0, 0, 0);
    public float LastForce = 0;
    private int QueueValue = 7;
    public PlayerController playercontroller;
    

    void Start()
    {

        foreach (GameObject placement in PlacementPlayer) {
            
            int NumberSeed = Random.Range(0, QueueValue);

            PlayerData PlayerIn = PlayerOutGame[NumberSeed];
            Canvas ZonePseudo = placement.GetComponentInChildren<Canvas>();
            TextMeshPro pseudo = ZonePseudo.GetComponentInChildren<TextMeshPro>();
            pseudo.text = PlayerIn.PlayerPseudo;
            PlayerIn.placement = placement;
            PlayerIn.Mains = placement.GetComponentInChildren<Mains>().gameObject;
            PlayerIn.ZonePlayer = placement.GetComponentInChildren<ZonePlaneDetect>();
            PlayerIn.ZonePlayer.player = PlayerIn;
            
            Debug.Log(pseudo.text);


            GameObject newPlayer = Instantiate(PlayerIn.PlayerSkin,
                                   placement.transform.position,
                                   placement.transform.rotation); 
            PlayerController logicplayer = newPlayer.GetComponent<PlayerController>();
            logicplayer.plane = planeAdd;
            

            PlayerInGame.Add(PlayerIn);
            PlayerOutGame.Remove(PlayerIn);
            QueueValue--;
        }

    }

    void Update()
    {
       
    }

    public void PaperCoord(SocketIOEvent e, Vector3 coord, float force)
    {
        foreach (PlayerData player in PlayerInGame)
        {
            Quaternion rotation = Quaternion.LookRotation(coord);
            rotation *= Quaternion.Euler(90f, 90f, 0f); // Rotation de 90 degrés autour de l'axe X

            var data = e.data.Trim('\\', '"');
            var param = data.Split('#');
            Debug.Log(param[0]);
            Debug.Log(player.PlayerPseudo);

            if (param[0] == player.PlayerPseudo)
            {
                /*if (player.CanShoot && player.planeInZone > 0)
                {
                    player.directionplane = coord;
                    player.ForceEnvoi = force;

                    Debug.Log(player.directionplane);
                    PlayerController playerController = player.PlayerSkin.GetComponentInChildren<PlayerController>();

                    player.planeInZone = player.planeInZone - 1;
                    Destroy(player.ZonePlayer.listAvions[0]);
                    playerController.Shoot(player, planeAdd, rotation);
                    player.CanShoot = false;
                    StartCoroutine(Chrono(player));
                }*/
                if (player.CanShoot /*&& player.planeInZone == 0*/)
                {
                    player.ForceEnvoi = force;
                    player.directionplane = coord;
                    Debug.Log(player.directionplane);
                    PlayerController playerController = player.PlayerSkin.GetComponentInChildren<PlayerController>();
                    playerController.Shoot(player, planeAdd, rotation);
                    player.CanShoot = false;
                    StartCoroutine(Chrono(player));
                }
            }
        }
    }

    private IEnumerator Chrono(PlayerData p)
    {
        yield return new WaitForSeconds(2);
        p.CanShoot = true;
    }
}
