using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayableCharacter))]
public class PlayerController : MonoBehaviour
{
    private PlayableCharacter player;
    private Vector2 moveDirection;

    private void Start()
    {
        player = GetComponent<PlayableCharacter>();
    }
    // Update is called once per frame
    void Update()
    {
        if (moveDirection == Vector2.zero)
            return;
        transform.Translate(moveDirection * player.Stat.MoveSpeed * Time.deltaTime);
    }
    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        if (input != null)
        {
            moveDirection = input.normalized;

        }
    }
}
