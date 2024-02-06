using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Image;
    public Image Image2;
    public Image Background;
    public Text HpText;
    float maxHP = -1f;
    float HP = 1f;
    float newHealth = 1f;

    bool m_LockTemporaryHealth = false;

    public float GetCurrentHealth()
    {
        return newHealth;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public void ResetMaxHealth(int health)
    {
        maxHP = health;
        HP = health;
        newHealth = health;
        Image.fillAmount = Mathf.InverseLerp(0, maxHP, HP);
        Image2.fillAmount = Mathf.InverseLerp(0, maxHP, HP);
        if (HpText)
            HpText.text = newHealth.ToString() + " / " + maxHP.ToString();
    }

    public void SetMaxHealth(int health)
    {
        if(Image != null)
        {
            maxHP = health;
            SetHealth(health);
            HP = health;
        }
    }

    public void changeMaxHealth(int newHp) {
        maxHP = newHp;
        if (HpText)
            HpText.text = newHealth.ToString() + " / " + maxHP.ToString();
    }

    private void Update()
    {
        if(m_LockTemporaryHealth)
        {
            return;
        }

        if (HP > newHealth)
        {
            HP -= Time.deltaTime * (maxHP/1.5f);
            if (HP <= newHealth)
            {
                HP = newHealth;
            }
            Image2.fillAmount = Mathf.InverseLerp(0, maxHP, HP);
        }
        else if (HP < newHealth)
        {
            HP += Time.deltaTime * (maxHP/1.5f);
            if (HP >= newHealth)
            {
                HP = newHealth;
            }
            Image.fillAmount = Mathf.InverseLerp(0, maxHP, HP);
        }
    }

    public void SetHealth(int health)
    {
        SetHealth((float)health);
    }

    public void SetHealth(float health)
    {
        if(m_LockTemporaryHealth)
        {
            return;
        }

        if (Image != null)
        {
            Show();
            newHealth = health;
            if (newHealth > HP)
            {
                Image2.fillAmount = Mathf.InverseLerp(0, maxHP, newHealth);
            }
            else
            {
                Image.fillAmount = Mathf.InverseLerp(0, maxHP, newHealth);
            }
            if (HpText)
            HpText.text = newHealth.ToString() + " / " + maxHP.ToString();
        }
    }

    public void SetTemporyHealth(int health)
    {
        m_LockTemporaryHealth = true;
        if (Image != null)
        {
            Show();
            newHealth = health;
            if (newHealth > HP)
            {
                Image2.fillAmount = Mathf.InverseLerp(0, maxHP, newHealth);
            }
            else
            {
                Image.fillAmount = Mathf.InverseLerp(0, maxHP, newHealth);
            }
            if (HpText)
                HpText.text = newHealth.ToString() + " / " + maxHP.ToString();
        }
    }

    public void UnlockTemporary()
    {
        m_LockTemporaryHealth = false;
    }

    public void Hide()
    {
        Image.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
    }

    public void Show()
    {
        Image.gameObject.SetActive(true);
        Image2.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);
    }

}
