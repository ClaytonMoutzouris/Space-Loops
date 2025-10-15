using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipMovement : MonoBehaviour
{
    public float amplitude = 2f; // Controls the height of the oscillation
    private Vector3 initialPosition;
    public ShipController controller;
    public int direction = 1;
    public float horzMovementRange = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        FlyAround();

        if (controller.transform.position.x > initialPosition.x + horzMovementRange)
        {
            controller.rb.linearVelocity = new Vector2(Mathf.Min(controller.rb.linearVelocity.x, 0), controller.rb.linearVelocity.y);

        }

        if (controller.transform.position.x < initialPosition.x - horzMovementRange)
        {
            controller.rb.linearVelocity = new Vector2(Mathf.Max(controller.rb.linearVelocity.x, 0), controller.rb.linearVelocity.y);

        }
    }

    public void FlyAround()
    {
        // Calculate the value of the cosine wave based on time and frequency
        //float sineValue = Mathf.Sin(Time.fixedTime * controller.stats.GetStat(StatType.MoveSpeed).GetValue());
        //float targetY = initialPosition.y + (sineValue * amplitude);
        //float forceY = (targetY - transform.position.y) * controller.rb.mass *10;

        if(direction > 0 && transform.position.y > initialPosition.y + amplitude)
        {
            direction = -1;
        } else if (direction < 0 && transform.position.y < initialPosition.y - amplitude)
        {
            direction = 1;
        }
        controller.rb.linearVelocity = Vector2.Lerp(controller.rb.linearVelocity, new Vector2(controller.rb.linearVelocity.x, direction * controller.stats.GetStat(ShipStatType.MoveSpeed).GetValue()), Time.fixedDeltaTime);
        //controller.rb.AddForce();

    }

    public void FlyAvoid()
    {
        //detect projectiles
    }


    public void MoveLeft()
    {
            //controller.transform.position = Vector3.Lerp(controller.transform.position, -controller.transform.position.normalized* controller.stats.GetStat(StatType.MoveSpeed).GetValue(),);
            controller.rb.linearVelocity = new Vector2(-1 * controller.stats.GetStat(ShipStatType.MoveSpeed).GetValue(), controller.rb.linearVelocity.y);

    }

    public void MoveRight()
    {
            //controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position.normalized * controller.stats.GetStat(StatType.MoveSpeed).GetValue(), controller.stats.GetStat(StatType.MoveSpeed).GetValue());
            controller.rb.linearVelocity = new Vector2(1 * controller.stats.GetStat(ShipStatType.MoveSpeed).GetValue(), controller.rb.linearVelocity.y);
        
    }

    public void StopMove()
    {
        //controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position.normalized * controller.stats.GetStat(StatType.MoveSpeed).GetValue(), controller.stats.GetStat(StatType.MoveSpeed).GetValue());
        controller.rb.linearVelocity = new Vector2(1 * controller.stats.GetStat(ShipStatType.MoveSpeed).GetValue(), controller.rb.linearVelocity.y);

    }
}
