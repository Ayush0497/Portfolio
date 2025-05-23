
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHp = 100, currentHp;
    [SerializeField] UnityEngine.UI.Image healthBar;
    void Start()
    {
        currentHp = maxHp;
    }
    public void applyDamage(float value)
    {
        currentHp -= value;
        updateHealthBar();
        if (currentHp <= 0)
        {
            Events.KilledZombies.Invoke();
            currentHp = 0;
            gameObject.SetActive(false);
        }
    }

    private void updateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(currentHp / maxHp, 1, 1);
    }
}
