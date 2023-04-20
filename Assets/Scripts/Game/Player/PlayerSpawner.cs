using UnityEngine;

namespace Game.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject spawnPosition;
        public GameObject cameraPrefab;
        public GameObject ratPrefab;

        private bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;
            hasSpawned = true;


            var rat = Instantiate(ratPrefab);
            var camera = Instantiate(cameraPrefab);

            rat.transform.position = spawnPosition.transform.position;
            camera.transform.position = rat.transform.position;

            camera.GetComponent<CameraController>().SetTarget(rat);
        }
    }
}
