using UnityEngine;
using Random = UnityEngine.Random;

public class Screenshake : SingletonBehaviour<Screenshake>
{
    private float shakeElapsed;
    private float shakeDuration;
    private float shakeMagnitude;

    private Vector3 originalPos;

    public Camera Cam;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void LateUpdate()
    {
        if (shakeElapsed < shakeDuration)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(x, y, originalPos.z);
        }
        else
        {
            transform.position = originalPos;
        }

        shakeElapsed += Time.deltaTime;
    }

    public void ScreenShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeElapsed = 0f;
    }
}