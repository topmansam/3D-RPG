using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SO{
public class PlayerManager : CharacterManager
{
    PlayerLocomotionManager playerLocomotionManager;
    protected override void Awake(){
        base.Awake();

        //DO MORE STUFF< ONLY FOR THE PLAYER
       playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }
    
    protected override void Update(){
        base.Update();
            if (!IsOwner)
                return;
        //HANDLE MOVEMENT
        playerLocomotionManager.HandleAllMovement();
    }
        protected override void LateUpdate()
        {
            if (!IsOwner) return;
            base.LateUpdate();
            PlayerCamera.instance.HandleAllCameraActions();
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
            }
        }

         
}
}