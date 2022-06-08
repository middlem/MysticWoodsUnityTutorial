﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovementDriver
{
    Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;

    public float moveSpeed = 1.0f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float collisionOffset = 0.05f;

    public float CollisionOffset
    {
        get { return collisionOffset; }
        set { collisionOffset = value; }
    }

    public MovementDriver(Rigidbody2D rb_p)
    {
        this.rb = rb_p;
    }

    // Moves away from toPosition by distance amount
    // Attempts to move on one axis if collision is detected
    public bool TryMoveAwayFromPositionByDistance(Vector3 myPos, Vector3 toPosition, float distance)
    {
        var vectorSum = toPosition - myPos;

        // Invert all directions to move away
        Vector2 direction = new Vector2(
            vectorSum.x > 0 ? -1 : vectorSum.x == 0 ? 0 : 1,
            vectorSum.y > 0 ? -1 : vectorSum.y == 0 ? 0 : 1);

        bool success = TryMoveByDistance(direction, distance);

        if (!success)
        {
            success = TryMoveByDistance(new Vector2(direction.x, 0), distance);
        }

        if (!success)
        {
            success = TryMoveByDistance(new Vector2(0, direction.y), distance);
        }

        return success;
    }

    // Moves towards position with collision
    // Attempts to move on one axis if collision is detected
    public bool TryMoveTowardsPosition(Vector3 myPos, Vector3 toPosition)
    {
        var vectorSum = toPosition - myPos;

        Vector2 direction = new Vector2(
            vectorSum.x > 0 ? 1 : vectorSum.x == 0 ? 0 : -1,
            vectorSum.y > 0 ? 1 : vectorSum.y == 0 ? 0 : -1);

        bool success = TryMove(direction);

        if (!success)
        {
            success = TryMove(new Vector2(direction.x, 0));
        }

        if (!success)
        {
            success = TryMove(new Vector2(0, direction.y));
        }

        return success;
    }

    // Checks for collision
    // Distanced moved based on move speed
    // Return if movement was successful
    public bool TryMove(Vector2 movementVector)
    {
        return TryMoveByDistance(movementVector, moveSpeed);
    }

    public bool TryMoveByDistance(Vector2 movementVector, float distance)
    {
        if (movementVector != Vector2.zero && canMove == true)
        {
            int count = rb.Cast(movementVector, movementFilter, castCollisions, distance * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementVector * distance * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else { return false; }
    }

    public void LockMovement()
    {
        canMove = false;
    }


    public void UnlockMovement()
    {
        canMove = true;
    }
}
