public class AICharacter : Character, ICanMove
{
    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (!runOver)
            Movement();
    }
}
