using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace SO{
public class TitleScreenManager : MonoBehaviour
{
  public void StartNetworkAsHost(){

    NetworkManager.Singleton.StartHost();
  }
  public void StartNewGame(){
    StartCoroutine (WorldGameSaveManager.instance.LoadNewGame()); 
  }
}
}