using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;
        public float parallaxFactor;
        private Vector3 initialPosition;

        public void Init()
        {
            if (layerTransform != null)
                initialPosition = layerTransform.position;
        }

        public void UpdateLayer(float camX)
        {
            if (layerTransform != null)
            {
                float deltaX = camX * parallaxFactor;
                layerTransform.position = new Vector3(initialPosition.x + deltaX, initialPosition.y, initialPosition.z);
            }
        }
    }

    public Transform cameraTransform;
    public ParallaxLayer[] layers;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        foreach (var layer in layers)
        {
            layer.Init();
        }
    }

    void Update()
    {
        float camX = cameraTransform.position.x;

        foreach (var layer in layers)
        {
            layer.UpdateLayer(camX);
        }
    }
}
