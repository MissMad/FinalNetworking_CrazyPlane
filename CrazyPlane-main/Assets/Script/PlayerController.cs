using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool CanShoot = true;
    public bool TestShoot = false;
    public Vector3 Direction = new Vector3(0, 0, 0);
    public GameObject plane;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(PlayerData player, GameObject objectSpawn, Quaternion rotation)
    {
        Animator animator = player.PlayerSkin.GetComponent<Animator>();


        Debug.Log("Shoot");
        Debug.Log("INFO" + this.transform.position);
        GameObject newPlane = Instantiate(plane,
                              player.Mains.transform.position,
                              rotation);
        PaperPlaneTest techDirectionChange = newPlane.GetComponentInChildren<PaperPlaneTest>();
        Debug.Log("NJHUSOMZBUJMI  " + player.ForceEnvoi);
        techDirectionChange.Controllerdirection(player.directionplane, player.ForceEnvoi);
    }
}
