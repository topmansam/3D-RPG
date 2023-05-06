using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
namespace SO{
public class CharacterManager : NetworkBehaviour
{
    public CharacterController characterController;
        CharacterNetworkManager characterNetworkManager;
    protected virtual void Awake(){
        DontDestroyOnLoad(this);
        characterController=GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
    }
protected virtual void Update(){
            /*if character is beign controlled from our side then
             * assign its netowrk position to the position of our transform.*/
if(IsOwner){
    characterNetworkManager.networkPosition.Value = transform.position;
     characterNetworkManager.networkRotation.Value = transform.rotation;
}
/*If this character is being controlled from else where, then assign its position here locally.*/
else{
                //Position
                transform.position = Vector3.SmoothDamp(transform.position,
                    characterNetworkManager.networkPosition.Value,
                    ref characterNetworkManager.networkPositionVelcoity,
                    characterNetworkManager.networkPositionSmoothTime);

                //Rotation
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    characterNetworkManager.networkRotation.Value,
                    characterNetworkManager.networkRotationSmoothTime);
                    
}
}
        protected virtual void LateUpdate()
        {

        }
}
}