using System;
using UnityEngine;

namespace Game.Utility
{
    public class LevelStart : MonoBehaviour
    {
        public GameObject ratPrefab;

        public Transform spawnLocation;

        private void Awake()
        {
            var rat = Instantiate(ratPrefab);

            rat.transform.position = spawnLocation.position;
        }
    }
}