using System;
using HexCode.Common;

namespace HexCode.Engine
{
    public class RobotType : IRobotType
    {

        public static RobotType Default { get; private set; } = new RobotType();
        public RobotType()
        {
            MoveRange = 1;
            ScannerRange = 5;
            AttackRange = 3;
            MaxHealth = 50;
            HealthRegeneration = 1;
            Damage = 5;
            MaxEnergy = 100;
            EnergyRegeneration = 25;
            MoveEnergyCost = 20;
            AttackEnergyCost = 20;
            WallEnergyCost = 20;
            WallPartsCost = 20;
        }

        public int MoveRange { get; set; }
        public int ScannerRange { get; set; }
        public int AttackRange { get; set; }
        public int MaxHealth { get; set; }
        public int HealthRegeneration { get; set; }
        public int MaxEnergy { get; set; }
        public int EnergyRegeneration { get; set; }
        public int MoveEnergyCost { get; set; }
        public int AttackEnergyCost { get; set; }
        public int Damage { get; set; }
        public int WallEnergyCost { get; set; }
        public int WallPartsCost { get; set; }
    }
}