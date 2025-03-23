using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayableCharacter))]
public class PlayerController : MonoBehaviour
{
    private PlayableCharacter player;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;

    private void Start()
    {
        player = GetComponent<PlayableCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    public void ManualFixedUpdate()
    {
        if (moveDirection == Vector2.zero)
            return;
        transform.Translate(moveDirection * player.Stat.MoveSpeed * Time.deltaTime);
        if(moveDirection.x != 0)
        {
            bool temp = moveDirection.x < 0;
            spriteRenderer.flipX = temp;
            player.IsFlip = temp;
        }
    }
    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        if (input != null)
        {
            moveDirection = input.normalized;

        }
    }
    public void ResetInput()
    {
        moveDirection = new Vector2();
    }
}
