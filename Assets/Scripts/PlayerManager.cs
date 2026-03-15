using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    private PhotonView pnView;

    private void Awake()
    {
        pnView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(pnView.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PlayerController"), new Vector3(0, 2f, 0), Quaternion.identity);
    }
}
