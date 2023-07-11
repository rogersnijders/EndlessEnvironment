using System.Numerics;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] backgroundLayers; // Array of background layers
    public float[] parallaxFactors; // Array of parallax movement factors for each layer
    public float smoothing = 1f; // Smoothing factor for parallax movement

    private Transform cameraTransform;
    private UnityEngine.Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        UnityEngine.Vector3 cameraDeltaMovement = cameraTransform.position - previousCameraPosition;

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            float parallaxMovement = cameraDeltaMovement.x * parallaxFactors[i];

            UnityEngine.Vector3 backgroundTargetPosition = backgroundLayers[i].position + UnityEngine.Vector3.right * parallaxMovement;
            backgroundLayers[i].position = UnityEngine.Vector3.Lerp(backgroundLayers[i].position, backgroundTargetPosition, smoothing * Time.deltaTime);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
