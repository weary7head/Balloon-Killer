using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsScriptableObject", order = 1)]
    public class SettingsScriptableObject : ScriptableObject
    {
        public float ValueOfSpeedIncrease => _valueOfSpeedIncrease;
        public float ChangeSpeedRate => _changeSpeedRate;

        [Header("Speed settings")]
        [SerializeField] private float _valueOfSpeedIncrease;
        [SerializeField] private float _changeSpeedRate;

        public void SetValueOfSpeedIncrease(float value)
        {
            _valueOfSpeedIncrease = value;
        }

        public void SetChangeSpeedRate(float value)
        {
            _changeSpeedRate = value;
        }
    }
}