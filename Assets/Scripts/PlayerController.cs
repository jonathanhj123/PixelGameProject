using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    Rigidbody2D rb;
    private float _moveSpeed = 10f;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    //private LevelSelection _currentSelection;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        /* Snappy movement
        Vector2 input = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = input.normalized * _moveSpeed;
        */

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

/* Level selection detection
    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelSelection selection = other.GetComponent<LevelSelection>();
        if (selection != null)
        {
            _currentSelection = selection;
            Debug.Log("Enter trigger for " + selection.levelToLoad);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LevelSelection selection = other.GetComponent<LevelSelection>();
        if (selection == _currentSelection)
        {
            _currentSelection = null;
            Debug.Log("ExitLevelTrigger");
        }
    }
    */
}
