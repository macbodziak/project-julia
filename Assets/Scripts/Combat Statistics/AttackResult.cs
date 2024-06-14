public struct AttackResult
{
    public bool Hit { get; private set; }
    public int Damage { get; private set; }
    public bool IsKillingBlow { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageType Type { get; private set; }


    public AttackResult(bool hit, int damage, DamageType damageType, bool isCritical, bool isKillingBlow)
    {
        Hit = hit;
        Damage = damage;
        IsKillingBlow = isKillingBlow;
        IsCritical = isCritical;
        Type = damageType;
    }
}