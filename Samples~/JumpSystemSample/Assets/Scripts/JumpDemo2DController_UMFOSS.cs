using UnityEngine;
using GameplayMechanicsUMFOSS.Movement;

namespace GameplayMechanicsUMFOSS.Samples.Jump
{
    /// <summary>
    /// Minimal 2D player controller demonstrating ModularJumpSystem usage.
    ///
    /// Setup:
    /// 1. Create a 2D sprite with Rigidbody2D and BoxCollider2D
    /// 2. Attach ModularJumpSystem_UMFOSS (set DimensionMode to Mode2D)
    /// 3. Attach this script
    /// 4. Create a ground plane with a collider on the "Ground" layer
    /// 5. Assign the Ground layer in ModularJumpSystem's groundLayer field
    /// 6. Assign a Jump InputActionReference — or this script uses legacy Input as fallback
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ModularJumpSystem_UMFOSS))]
    public class JumpDemo2DController_UMFOSS : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 7f;

        private Rigidbody2D rb;
        private ModularJumpSystem_UMFOSS jumpSystem;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpSystem = GetComponent<ModularJumpSystem_UMFOSS>();

            jumpSystem.OnJumpStart += () => Debug.Log("Jump started!");
            jumpSystem.OnJumpEnd += () => Debug.Log("Landed!");
        }

        private void Update()
        {
            // Horizontal movement with air control
            float horizontal = Input.GetAxisRaw("Horizontal");
            float speed = moveSpeed * jumpSystem.AirControlMultiplier;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

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
