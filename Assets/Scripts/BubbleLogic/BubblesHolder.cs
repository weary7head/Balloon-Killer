using PlayerLogic;
using Settings;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleLogic
{
    public class BubblesHolder : MonoBehaviour
    {
        [Header("Speed settings")]
        [SerializeField] private SettingsScriptableObject _settings;
        [Header("Reference for Player")]
        [SerializeField] private Player _player;
        [Header("Reference for Bubble Spawner")]
        [SerializeField] private BubbleSpawner _bubbleSpawner;

        private float _speed;
        private float _nextTimeToChangeSpeed;
        private List<Bubble> _bubbles;

        private void Awake()
        {
            _speed = 0;
            _nextTimeToChangeSpeed = 1;
            _bubbles = new List<Bubble>();
        }

        private void OnEnable()
        {
            _bubbleSpawner.Spawned += AddBubble;
        }

        private void Update()
        {
            if (CheckTimeForChangeSpeed())
            {
                ChangeSpeed();
            }
        }

        private void OnDisable()
        {
            _bubbleSpawner.Spawned -= AddBubble;
        }

        private void AddBubble(Bubble bubble)
        {
            bubble.Clicked += BubbleClicked;
            bubble.Destroyed += BubbleDestroyed;
            bubble.ChangeSpeed(_speed);
            _bubbles.Add(bubble);
        }

        private void BubbleDestroyed(Bubble bubble)
        {
            _player.GetDamage(bubble.Damage);
            _bubbles.Remove(bubble);
            bubble.Clicked -= BubbleClicked;
            bubble.Destroyed -= BubbleDestroyed;
        }

        private void BubbleClicked(Bubble bubble)
        {
            _player.AddPoints(bubble.Points);
            _bubbles.Remove(bubble);
            bubble.Clicked -= BubbleClicked;
            bubble.Destroyed -= BubbleDestroyed;
        }

        private void ChangeSpeed()
        {
            _speed += _settings.ValueOfSpeedIncrease;
            foreach (Bubble bubble in _bubbles)
            {
                bubble.ChangeSpeed(_speed);
            }
        }

        private bool CheckTimeForChangeSpeed()
        {
            if (Time.time > _nextTimeToChangeSpeed)
            {
                _nextTimeToChangeSpeed = Time.time + 1f / _settings.ChangeSpeedRate;
                return true;
            }

            return false;
        }
    }
}