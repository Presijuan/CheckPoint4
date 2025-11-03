using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Strength, Shield }
    public PowerUpType type;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUp playerPower = other.GetComponent<PlayerPowerUp>();

            if (playerPower != null)
            {
                playerPower.ActivatePowerUp(type, duration);
            }

            Destroy(gameObject);
        }
    }
}