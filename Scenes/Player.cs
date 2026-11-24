
using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float Speed = 100f;

    private AnimatedSprite2D animation;

    // Håller reda på vilken riktning spelaren tittar åt
    private string facingDirection = "down";

    public override void _Ready()
    {
        animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        // Startar spelarens stillastående animation
        animation.Play("idle_down");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Läser av WASD
        Vector2 direction = Input.GetVector(
            "move_left",
            "move_right",
            "move_up",
            "move_down"
        );

        // Bestämmer spelarens hastighet
        Velocity = direction * Speed;

        // Flyttar spelaren
        MoveAndSlide();

        UpdateAnimation(direction);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        // Om spelaren står stilla
        if (direction == Vector2.Zero)
        {
            animation.Play("idle_" + facingDirection);
            return;
        }

        // Bestämmer vilken riktning spelaren rör sig åt
        if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
        {
            facingDirection = direction.X > 0 ? "right" : "left";
        }
        else
        {
            facingDirection = direction.Y > 0 ? "down" : "up";
        }

        // Spelar gånganimationen
        animation.Play("run_" + facingDirection);
    }
}
