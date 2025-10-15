using UnityEngine;

public class BattleAreaPanelUI : MonoBehaviour
{


    public void ShipLeft()
    {
        if(GameManager.instance.playerShip)
        {
            GameManager.instance.playerShip.shipMovement.MoveLeft();
        }
    }

    public void ShipRight()
    {
        if (GameManager.instance.playerShip)
        {
            GameManager.instance.playerShip.shipMovement.MoveRight();
        }
    }
    
    public void ShipStop()
    {
        if (GameManager.instance.playerShip)
        {
            GameManager.instance.playerShip.shipMovement.StopMove();
        }
    }
}
