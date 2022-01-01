using TMPro;
using UnityEngine;

namespace Settings
{
    public class SettingsPanel : MonoBehaviour
    {
        [Header("References for input fields")]
        [SerializeField] private TMP_InputField _speedIncreaseInputField;
        [SerializeField] private TMP_InputField _speedRateInputField;
        [Header("Default speed settings")]
        [SerializeField] private SettingsScriptableObject _defaultSettings;
        [Header("User speed settings")]
        [SerializeField] private SettingsScriptableObject _userSettings;

        private void Awake()
        {
            _speedIncreaseInputField.text = _userSettings.ValueOfSpeedIncrease.ToString();
            _speedRateInputField.text = _userSettings.ChangeSpeedRate.ToString();
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void SaveSettings()
        {
            float temporaryRate = 0;
            float temporarySpeed = 0;
            float.TryParse(_speedRateInputField.text, out temporaryRate);
            float.TryParse(_speedIncreaseInputField.text, out temporarySpeed);
            if (temporaryRate <= 0 || temporarySpeed <= 0)
            {
                LoadSettings();
            }
            else
            {
                SetUserSettings(temporaryRate, temporarySpeed);
                gameObject.SetActive(false);
            }
        }

        public void LoadSettings()
        {
            SetUserSettings(_defaultSettings.ChangeSpeedRate, _defaultSettings.ValueOfSpeedIncrease);
            gameObject.SetActive(false);
        }

        private void SetUserSettings(float changeSpeedRate, float valueOfSpeedIncrease)
        {
            _userSettings.SetChangeSpeedRate(changeSpeedRate);
            _userSettings.SetValueOfSpeedIncrease(valueOfSpeedIncrease);
        }
    }
}