using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    //Alt her er bascially unødvendigt, med mindre vi finder en grund til at skulle styre grandma med keyboard:

    InputAction moveAction;
    Rigidbody2D rb;
    private float _moveSpeed = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Slidey movement
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector2 targetVelocity = input.normalized * _moveSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.3f);

        //Animation Logic
        if (rb.linearVelocity.magnitude > 0.05f)
        {
            _animator.SetBool("IsMoving", true);
            if (rb.linearVelocity.x > 0.05f)
                _spriteRenderer.flipX = false;
            else if (rb.linearVelocity.x < -0.05f)
                _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
