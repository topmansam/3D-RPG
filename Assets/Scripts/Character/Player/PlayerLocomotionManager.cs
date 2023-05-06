using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SO{
public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    public float verticalMovement; 
    public float horizontalMovement;
    public float moveAmount;
private Vector3 moveDirection;
private Vector3 targetRotationDiretion;      
[SerializeField] float walkingSpeed = 2;
[SerializeField] float runningSpeed = 5;
[SerializeField] float rotationSpeed = 15;
        protected override void Awake(){
        base.Awake();
        player = GetComponent<PlayerManager>();
    }
   public void HandleAllMovement(){
    HandleGroundedMovement();
            HandleRotation();
   }
   private void GetVerticalAndHorizontalInputs(){
    verticalMovement = PlayerInputManager.instance.verticalInput;
    horizontalMovement = PlayerInputManager.instance.horizontalInput;
   }
   private void HandleGroundedMovement(){
    GetVerticalAndHorizontalInputs();
    //OUR MOVE DIRECTION IS BASED ON OUR CAMERAS FACING PERSEPCTIVE AND OUR MOVEMENT INPUT
    moveDirection= PlayerCamera.instance.transform.forward *verticalMovement;
    moveDirection=moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
    moveDirection.Normalize();
    moveDirection.y = 0;

    if(PlayerInputManager.instance.moveAmount>0.5f){
        // move at a running speed
        player.characterController.Move(moveDirection*runningSpeed* Time.deltaTime);
    }
        else if (PlayerInputManager.instance.moveAmount<=0.5f){
// move at a walking speed
player.characterController.Move(moveDirection*walkingSpeed*Time.deltaTime);
        }
    }
   private void HandleRotation()
        {
             targetRotationDiretion = Vector3.zero;
            targetRotationDiretion = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDiretion = targetRotationDiretion + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDiretion.Normalize();
            targetRotationDiretion.y = 0;
            if(targetRotationDiretion == Vector3.zero)
            {
                targetRotationDiretion = transform.forward;
            }
            Quaternion newRotation = Quaternion.LookRotation(targetRotationDiretion);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

   }
}
