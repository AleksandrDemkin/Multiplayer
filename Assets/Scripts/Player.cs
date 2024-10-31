using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthAmountText;
    //[SerializeField] private GameManager _gameManager;
    private string _healthText = "Health: ";

    public float PlayerHealth { get; private set; } = 100f;


    private void Update()
    {
        DisplayHealth();
    }

    public void GotHit(float damage)
    {
        PlayerHealth -= damage;

        if (PlayerHealth <= 0)
        {
            if (PlayerHealth < 0)
            {
                PlayerHealth = 0f;
            }

            //_gameManager.();
        }
    }

    private void DisplayHealth()
    {
        _healthAmountText.text = _healthText + PlayerHealth;
    }
}