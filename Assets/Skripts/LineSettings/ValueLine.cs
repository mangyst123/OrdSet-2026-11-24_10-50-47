using UnityEngine;

public class ValueLine : MonoBehaviour
{
    [SerializeField] private short _value;

    public void SetValue(short value) => _value = value;
    public short GetValue() => _value;
}
