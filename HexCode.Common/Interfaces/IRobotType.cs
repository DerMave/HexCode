
namespace HexCode.Common
{
    public interface IRobotType
    {
        int MoveRange { get; }
        int ScannerRange { get; }
        int AttackRange { get; }
        int MaxHealth { get; }
        int HealthRegeneration { get; }
        int Damage { get; }
        int MaxEnergy { get;  }
        int EnergyRegeneration { get; }
        int MoveEnergyCost { get;  }
        int AttackEnergyCost { get; }
        int WallEnergyCost { get; }
        int WallPartsCost { get; }
    }
}