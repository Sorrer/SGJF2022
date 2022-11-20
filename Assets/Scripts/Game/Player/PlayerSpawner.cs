using UnityEngine;

namespace Game.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject spawnPosition;
        public GameObject ratPrefab;

        private bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;
            hasSpawned = true;


            var rat = Instantiate(ratPrefab);

            rat.transform.position = spawnPosition.transform.position;
        }
    }
}
