using System;
using SABI.SOA;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float _playerHealth = 10;
    [SerializeField] private FloatValueSO healthRegenarationAmount;
    [SerializeField] private FloatValueSO maxHealth;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float invulnarabilityAfterTakingDamage;

    [SerializeField] private ActionSO OnGameFailed;
    private bool _canTakeDamage = true;
    private bool _isOnDontTakeDamageAbility = false;

    public void SetIsOnDontTakeDamageAbility(bool value) => _isOnDontTakeDamageAbility = value;

    private void Start()
    {
        SetPlayerHealthToMax();
    }

    public void SetPlayerHealthToMax() => SetHealth(maxHealth.GetValue());

    void Update()
    {
        HealthAutoRegenaration();
    }

    public void TakeDamage(float value)
    {
        if (!_canTakeDamage) return;
        if (_isOnDontTakeDamageAbility) return;

        _canTakeDamage = true;
        SetHealth(_playerHealth - value);
        if (_playerHealth == 0) PlayerDead();
        Invoke(nameof(SetCanTakeDamageToTrue), invulnarabilityAfterTakingDamage);
    }

    void SetCanTakeDamageToTrue() => _canTakeDamage = true;

    private void PlayerDead()
    {
        OnGameFailed.action.Invoke();
    }

    void SetHealth(float value)
    {
        _playerHealth = Mathf.Clamp(value, 0, maxHealth.GetValue());
        text.text = string.Format("HP: {0:#.0} ", _playerHealth);
        healthSlider.value = _playerHealth / maxHealth.GetValue();
    }

    void HealthAutoRegenaration()
    {
        SetHealth(_playerHealth + healthRegenarationAmount.GetValue() * Time.deltaTime);
    }
}