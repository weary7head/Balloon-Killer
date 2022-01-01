using Information;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [Header("Player settings")]
        [SerializeField] private int _health;
        [Header("Reference for Information View")]
        [SerializeField] private InformationView _informationView;

        private int _points;
        private int _bestPoints;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _saveSystem = new SaveSystem();
            _points = 0;
        }

        private void Start()
        {
            _bestPoints = _saveSystem.LoadResult();
            _informationView.SetHealth(_health);
            _informationView.SetBestResult(_bestPoints);
        }

        public void GetDamage(int damage)
        {
            if (_health > 0)
            {
                _health -= damage;
                if (_health < 0)
                {
                    _health = 0;
                }
                _informationView.SetHealth(_health);
            }

            if (_health == 0)
            {
                Die();
            }
        }

        public void AddPoints(int points)
        {
            _points += points;
            _informationView.SetPoints(_points);
        }

        private void Die()
        {
            if (_bestPoints < _points)
            {
                _saveSystem.SaveResult(_points);
            }
            SceneManager.LoadSceneAsync("Level", LoadSceneMode.Single);
        }
    }
}
