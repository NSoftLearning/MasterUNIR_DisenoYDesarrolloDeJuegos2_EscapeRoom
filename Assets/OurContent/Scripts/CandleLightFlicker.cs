using UnityEngine;

public class CandleLightFlicker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light targetLight;

    [Header("Intensity")]
    [SerializeField] private float minIntensity = 0.9f;
    [SerializeField] private float maxIntensity = 1.3f;

    [Header("Flicker")]
    [Tooltip("Velocidad general del parpadeo.")]
    [SerializeField] private float flickerSpeed = 3f;

    [Tooltip("Añade pequeñas variaciones rápidas sobre el movimiento principal.")]
    [SerializeField] private float detailAmount = 0.25f;

    [Tooltip("Cuánto más rápido se mueve el ruido secundario.")]
    [SerializeField] private float detailSpeedMultiplier = 3f;

    [Tooltip("Velocidad con la que la luz sigue el valor generado. " +
             "Más alto = más nervioso, más bajo = más suave.")]
    [SerializeField] private float responseSpeed = 10f;

    private float noiseOffset;

    private void Awake()
    {
        // Hace que varias velas no parpadeen exactamente igual.
        noiseOffset = Random.Range(0f, 1000f);
    }

    private void Update()
    {
        if (targetLight == null)
            return;

        float time = Time.time;

        // Variación principal, lenta y suave.
        float mainNoise = Mathf.PerlinNoise(
            noiseOffset,
            time * flickerSpeed
        );

        // Pequeñas variaciones más rápidas.
        float detailNoise = Mathf.PerlinNoise(
            noiseOffset + 100f,
            time * flickerSpeed * detailSpeedMultiplier
        );

        // Centramos el ruido secundario alrededor de 0.
        float noise =
            mainNoise +
            (detailNoise - 0.5f) * detailAmount;

        noise = Mathf.Clamp01(noise);

        float targetIntensity =
            Mathf.Lerp(minIntensity, maxIntensity, noise);

        // Suavizado independiente aproximadamente del framerate.
        float lerpFactor =
            1f - Mathf.Exp(-responseSpeed * Time.deltaTime);

        targetLight.intensity = Mathf.Lerp(
            targetLight.intensity,
            targetIntensity,
            lerpFactor
        );
    }
}