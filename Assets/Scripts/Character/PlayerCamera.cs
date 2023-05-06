using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SO{
public class PlayerCamera : MonoBehaviour
{
        public static PlayerCamera instance;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;
        public PlayerManager player;
        //change these to tweak camera performance
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1; // bigger number, longer camera will take to move to the player
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; // Lowest point you can look down
        [SerializeField] float maximumPivot = 60; // Highest point you can ook up
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayers;
        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; //Used for camera collisions ( moves the camera object to this postition upon coliding)
        [SerializeField] float leftAndRightLookAngle; 
        [SerializeField] float upAndDownLookAngle;

        private float cameraZPosition;  // values used for camera collsions
        private float targetCameraZPosition;
        private void Awake(){
        if(instance ==null) instance = this;
        else Destroy(gameObject);
    }
    private void Start(){
        DontDestroyOnLoad(gameObject);
            cameraZPosition = cameraObject.transform.localPosition.z;
    }
        public void HandleAllCameraActions()
        {
            
            if (player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandleCollisions();
            }
        }
        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position,
                player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }
        private void HandleRotations()
        {
            //if locked on force ortation towards target
            //else rotate normally

            //Normal rotations
            //rotate left and right baed on horizontal movemnt on righrt joytick

            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed)*Time.deltaTime;
            // rotate up and down based on vertical movement on the right joystick
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            // Clamp the up and down look agnle between a mix and max value
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            //Rorate this gameobject left and right
            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation= Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;



        }
        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            // direction for collision check
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // chcek if object in front our camera
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                    // if there is, we get our distance from it
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);

                    // we then equare our target z position to the following
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            //if our target position is less than our collision radius, we subtract our collision radius ( making it snap back)
            if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }
            //We then apply our final position using a lerp over a time of 0.2f
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
}
}