using Godot;
using System;

public class PlayerOne : KinematicBody2D
{
    const int walkSpeed = 200;

    Vector2 velocity;
    float timer = 40.0f;

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionPressed("ui_a"))
        {
            velocity.x = -walkSpeed;
        }
        else if (Input.IsActionPressed("ui_d"))
        {
            velocity.x = walkSpeed;
        }
        else
        {
            velocity.x = 0;
        }
        if (Input.IsActionPressed("ui_w"))
        {
            velocity.y = -walkSpeed;
        }
        else if (Input.IsActionPressed("ui_s"))
        {
            velocity.y = walkSpeed;
        }
        else
        {
            velocity.y = 0;
        }
        // We don't need to multiply velocity by delta because "MoveAndSlide" already takes delta time into account.

        // The second parameter of "MoveAndSlide" is the normal pointing up.
        // In the case of a 2D platformer, in Godot, upward is negative y, which translates to -1 as a normal.
        MoveAndSlide(velocity, new Vector2(0, -1));
        for (int i = 0; i < GetSlideCount(); i++)
        {
            var collision = GetSlideCollision(i);
            GD.Print("I collided with ", ((Node)collision.Collider).Name);
            if (((Node)collision.Collider).Name == "PlayerTwo" && timer == 0)
            {
                if(GetNode("/root/Node2D/SpritePL2").Get("visible") is true)
                {
                    GetNode("/root/Node2D/SpritePL2").Set("visible", false);
                    GetNode("/root/Node2D/SpritePL1").Set("visible", true);
                }
                else
                {
                    GetNode("/root/Node2D/SpritePL1").Set("visible", false);
                    GetNode("/root/Node2D/SpritePL2").Set("visible", true);
                }
                timer = 40.0f;
            }
        }
        if (timer < 0)
        {
            timer = 0.1f;
        }
        else 
        {
            timer -= 0.1f;
        }

    }
}