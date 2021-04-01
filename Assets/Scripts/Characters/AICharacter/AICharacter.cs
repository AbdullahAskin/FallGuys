public class AICharacter : Character, ICanMove
{
    void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (!runOver)
            Movement();
    }
}
