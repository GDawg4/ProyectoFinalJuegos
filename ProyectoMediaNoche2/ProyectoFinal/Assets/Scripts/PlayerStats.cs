using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int maxHP = 100;
    public float RegenRate = 2f;
    public float movementSpeed = 10f;

    private int _curHP;
    public int curHP
    {
        get { return _curHP; }
        set
        {
            _curHP = Mathf.Clamp(value, 0, maxHP);
        }
    }

    public void Awake()
    {
        curHP = maxHP;

        if (instance == null)
        {
            instance = this;
        }
    }

}
