using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace SO{
public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [Header("NETWORK JOIN")]
    [SerializeField] bool startGameAsClient;

    private void Awake(){
        if (instance==null) instance = this;
        else Destroy(gameObject);
    }
    
    private void Start(){
        DontDestroyOnLoad(gameObject);
    }
    private void Update(){
        if(startGameAsClient){
            startGameAsClient=false;
            // WE MUST FIRST SHUT DOWN BECAUSE WE HAVE STARTED AS A HOST DURRING THE TITLE SCREEN
            NetworkManager.Singleton.Shutdown();
            // WE THEN RESTART> AS A CLIENT

            NetworkManager.Singleton.StartClient();
        }
    }
}
}