using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SO{ 
public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
        [Header("Movement Input")]
    [SerializeField] Vector2 movementInput;
        public float  horizontalInput;
    public float verticalInput;

        [Header("Camera rotation Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        public float moveAmount;
    PlayerControls playerControls;
    private void Awake(){
        if(instance ==null) instance = this;
        else Destroy(gameObject);
         
    }
    private void Start(){
        
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged+= OnSceneChange;
        instance.enabled = false;
     
    }
    private void OnSceneChange(Scene oldScene, Scene newScene){
        //IF WE ARE LOADING INTO OUR WORLD SCENE< ENABLE PLAYER CONTROLS
        if(newScene.buildIndex == WorldGameSaveManager.instance.GetWorldSceneIndex()){
            instance.enabled = true;
        }
        //EKSE DISABLE PLAYERS CONTROL SO OUR PLYER CANT MOVE IN PLAYER CREATION 
        else{
            instance.enabled=false;
        }
    }
        private void OnEnable(){
            if(playerControls==null) 
            {
                playerControls= new PlayerControls();
            playerControls.PlayerMovement.Movement.performed+=i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            }
            playerControls.Enable(); 
}
private void OnDestroy(){
SceneManager.activeSceneChanged-=OnSceneChange;
}
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }

        private void Update (){
    HandlePlayerMovementInput();
            HandleCameraMovementInput();
}

private void HandlePlayerMovementInput(){
    verticalInput = movementInput.y;
    horizontalInput=movementInput.x;

    //RETURN ABS NUMBER( NUMBER WITHOUT NEGATIVE SIGN SO ALWAYS POSTIVE)
    float moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        //CLAMP VALUES SO THEY ARE 0, 0.5 OR 1
    if(moveAmount <= 0.5 && moveAmount > 0){
         moveAmount = 0.5f;

    }
    else if(moveAmount > 0.5 && moveAmount <=1){
        moveAmount =1;
    }
}

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
         
    }

}