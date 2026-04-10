using UnityEngine;

namespace YourNamespaceName
{
    public class CloudSpawner : MonoBehaviour
    {
       
        public GameObject spherePrefab;
        public int density = 100; // Number of spheres
        public float spread = 5f; // Spread of the spheres
        public float maxDistance = 10f; // Maximum distance from the central point
        public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // Minimum scale for spheres
        public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Maximum scale for spheres

        void Start()
        {
            SpawnClouds();
        }

        void SpawnClouds()
        {
            for (int i = 0; i < density; i++)
            {
                // Generate a random angle
                float angle = Random.Range(0f, Mathf.PI * 2f);

                // Calculate position on the circular flat area with variation
                float radius = Mathf.Sqrt(Random.value) * spread; // Random value for radius
                Vector3 randomPosition = new Vector3(Mathf.Cos(angle) * radius, Random.Range(-1f, 1f) * spread * 0.2f, Mathf.Sin(angle) * radius);

                // Instantiate the sphere at the random position
                GameObject sphere = Instantiate(spherePrefab, transform.position + randomPosition, Quaternion.identity);

                // Assign a random scale to the sphere
                float randomScaleX = Random.Range(minScale.x, maxScale.x);
                float randomScaleY = Random.Range(minScale.y, maxScale.y);
                float randomScaleZ = Random.Range(minScale.z, maxScale.z);
                sphere.transform.localScale = new Vector3(randomScaleX, randomScaleY, randomScaleZ);

                // Parent the sphere to this object for better scene organization
                sphere.transform.parent = transform;
            }
        }
    }
}
