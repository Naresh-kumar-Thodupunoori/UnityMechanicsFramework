using UnityEngine;
using GameplayMechanicsUMFOSS.Movement;

namespace GameplayMechanicsUMFOSS.Samples.Jump
{
    /// <summary>
    /// Minimal 3D player controller demonstrating ModularJumpSystem usage.
    ///
    /// Setup:
    /// 1. Create a 3D capsule with Rigidbody and CapsuleCollider
    /// 2. Attach ModularJumpSystem_UMFOSS (set DimensionMode to Mode3D)
    /// 3. Attach this script
    /// 4. Create a ground plane with a collider on the "Ground" layer
    /// 5. Assign the Ground layer in ModularJumpSystem's groundLayer field
    /// 6. Assign a Jump InputActionReference — or this script uses legacy Input as fallback
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ModularJumpSystem_UMFOSS))]
    public class JumpDemo3DController_UMFOSS : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;

        private Rigidbody rb;
        private ModularJumpSystem_UMFOSS jumpSystem;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumpSystem = GetComponent<ModularJumpSystem_UMFOSS>();

            // Lock rotation so the capsule doesn't topple
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            jumpSystem.OnJumpStart += () => Debug.Log("Jump started!");
            jumpSystem.OnJumpEnd += () => Debug.Log("Landed!");
        }

        private void Update()
        {
            // Horizontal movement on XZ plane with air control
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            float speed = moveSpeed * jumpSystem.AirControlMultiplier;

            Vector3 move = new Vector3(horizontal, 0f, vertical).normalized * speed;
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            // Fallback input for jump (when not using InputActionReference)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpSystem.OnJumpPressed();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpSystem.OnJumpReleased();
            }
        }
    }
}
