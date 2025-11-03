using UnityEngine;
using System.Collections;

public class PlayerPowerUp : MonoBehaviour
{
    [Header("Componentes del jugador")]
    public Renderer playerRenderer;
    public float baseSpeed = 5f;
    private float currentSpeed;

    [Header("Estados del jugador")]
    private bool isSpeedBoosted = false;
    private bool isStrengthBoosted = false;
    private bool isShielded = false;

    [Header("VFX - Power Up Velocidad")]
    public GameObject speedTrail;
    public ParticleSystem speedLines;
    public ParticleSystem speedSparks;
    public ParticleSystem speedMist;
    public ParticleSystem speedBurst;
    public Material speedAuraMat;

    [Header("VFX - Power Up Fuerza")]
    public ParticleSystem powerCore;
    public ParticleSystem powerSparksL;
    public ParticleSystem powerSparksR;
    public ParticleSystem shockwaveAura;
    public ParticleSystem flameVeins;
    public Material strengthMaterial;

    [Header("VFX - Power Up Escudo")]
    public ParticleSystem shieldField;
    public ParticleSystem shieldSparks;
    public ParticleSystem impactWave;
    public ParticleSystem energyFlow;
    public Material shieldMaterial;

    private Color defaultColor;

    private void Start()
    {
        if (playerRenderer == null)
            playerRenderer = GetComponentInChildren<Renderer>();

        currentSpeed = baseSpeed;
        defaultColor = playerRenderer.material.color;
    }

    public void ActivatePowerUp(PowerUp.PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.Speed:
                StartCoroutine(SpeedBoost(duration));
                break;

            case PowerUp.PowerUpType.Strength:
                StartCoroutine(StrengthBoost(duration));
                break;

            case PowerUp.PowerUpType.Shield:
                StartCoroutine(ShieldBoost(duration));
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        if (isSpeedBoosted) yield break;
        isSpeedBoosted = true;
        currentSpeed = baseSpeed * 2f;

        Material originalMaterial = playerRenderer.material;
        playerRenderer.material = speedAuraMat;

        speedTrail?.SetActive(true);
        speedLines?.gameObject.SetActive(true);
        speedSparks?.gameObject.SetActive(true);
        speedMist?.gameObject.SetActive(true);
        speedBurst?.gameObject.SetActive(true);
        speedLines?.Play();
        speedSparks?.Play();
        speedMist?.Play();
        speedBurst?.Play();

        yield return new WaitForSeconds(duration);

        currentSpeed = baseSpeed;
        playerRenderer.material = originalMaterial;
        speedTrail?.gameObject.SetActive(false);
        speedLines?.gameObject.SetActive(false);
        speedSparks?.gameObject.SetActive(false);
        speedMist?.gameObject.SetActive(false);
        speedBurst?.gameObject.SetActive(false);
        isSpeedBoosted = false;
    }

    private IEnumerator StrengthBoost(float duration)
    {
        if (isStrengthBoosted) yield break;
        isStrengthBoosted = true;

        Material originalMaterial = playerRenderer.material;
        playerRenderer.material = strengthMaterial;

        powerCore?.gameObject.SetActive(true);
        powerSparksL?.gameObject.SetActive(true);
        powerSparksR?.gameObject.SetActive(true);
        flameVeins?.gameObject.SetActive(true);
        shockwaveAura?.gameObject.SetActive(true);
        powerCore?.Play();
        powerSparksL?.Play();
        powerSparksR?.Play();
        flameVeins?.Play();
        shockwaveAura?.Play();

        Debug.Log("Fuerza activada");

        yield return new WaitForSeconds(duration);

        powerSparksL?.gameObject.SetActive(false);
        powerSparksR?.gameObject.SetActive(false);
        flameVeins?.gameObject.SetActive(false);
        shockwaveAura?.gameObject.SetActive(false);
        powerCore?.gameObject.SetActive(false);

        playerRenderer.material = originalMaterial;
        isStrengthBoosted = false;
    }

    private IEnumerator ShieldBoost(float duration)
    {
        if (isShielded) yield break;
        isShielded = true;

        Material originalMaterial = playerRenderer.material;
        playerRenderer.material = shieldMaterial;

        shieldField?.gameObject.SetActive(true);
        shieldSparks?.gameObject.SetActive(true);
        energyFlow?.gameObject.SetActive(true);
        impactWave?.gameObject.SetActive(true);
        shieldField?.Play();
        shieldSparks?.Play();
        energyFlow?.Play();
        impactWave?.Play();

        Debug.Log("Escudo activado");

        yield return new WaitForSeconds(duration);

        shieldField?.Stop();
        shieldSparks?.Stop();
        energyFlow?.Stop();
        impactWave?.Stop();
        shieldField?.gameObject.SetActive(false);
        shieldSparks?.gameObject.SetActive(false);
        energyFlow?.gameObject.SetActive(false);
        impactWave?.gameObject.SetActive(false);

        playerRenderer.material = originalMaterial;
        isShielded = false;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}