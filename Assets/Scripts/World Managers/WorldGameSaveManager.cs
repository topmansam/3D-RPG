using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SO
{
public class WorldGameSaveManager : MonoBehaviour
{
 public static WorldGameSaveManager instance;

[SerializeField] int worldSceneIndex=1;
 private void Awake(){
    //CAN ONLY BE ONE INSTANCE AT A TIME
    if (instance == null) instance = this;
    else Destroy(gameObject);
 }

private void Start(){
    DontDestroyOnLoad(gameObject);
}
public IEnumerator LoadNewGame(){
    AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
    yield return null;
}
public int GetWorldSceneIndex(){
    return worldSceneIndex;
}
}
}