using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BattleGame
{
    public class BattleCore
    {
        private readonly int ArmyCount = 5;
        public List<Player> Players { get; private set; }
        public BattleCore()
        {
            this.Players = new List<Player>();
        }
        public void MakeArmy(Player player)
        {
            player.Army.Clear();
            while (player.Army.Count < this.ArmyCount)
            {
                player.AddUnit();
            }
        }
        public IEnumerable<string> ShowArmy(Player player)
        {
            List<string> army = new List<string>();
            player.Army.ForEach(i => army.Add(i.ToString()));
            return army;
        }
        public void BattleProcess()
        {
            throw new NotImplementedException();
        }
    }
    public class Player
    {
        public string Name { get; private set; }
        public Player(string name)
        {
            this.Army = new List<UnitWrapper>();
            this.Name = name;
        }
        public List<UnitWrapper> Army { get; private set; }
        public void AddUnit()
        {
            Type unitType = Definitions.UnitsDefinitions[new Random().Next(0, 3)];
            Thread.Sleep(new Random().Next(5, 10));
            Type unitStateType = Definitions.UnitSatesDefinitions[new Random().Next(0, 5)];
            Thread.Sleep(new Random().Next(5, 10));
            Type unitWrapperType = Definitions.UnitWrappersDefinitions[new Random().Next(0, 5)];
            Unit unit = (Unit)Activator.CreateInstance(unitType, new object[] { unitStateType } );
            UnitWrapper unitWrapper = (UnitWrapper)Activator.CreateInstance(unitWrapperType, new object[] { unit });
            this.Army.Add(unitWrapper);
        }
    }
    public static class Definitions
    {
        public static Dictionary<int, Type> UnitsDefinitions = new Dictionary<int, Type>
        {
            { 0, typeof(Hero) }, { 1, typeof(Knight) }, { 2, typeof(Warrior) },
        };
        public static Dictionary<int, Type> UnitSatesDefinitions = new Dictionary<int, Type>
        {
            { 0, typeof(Inspired) }, { 1, typeof(Fresh) }, { 2, typeof(Regular) }, { 3, typeof(Indifferent) }, { 4, typeof(Scared) },
        };
        public static Dictionary<int, Type> UnitWrappersDefinitions = new Dictionary<int, Type>
        {
            { 0, typeof(Stronger) }, { 1, typeof(Strong) }, { 2, typeof(Normal) }, { 3, typeof(Weak) }, { 4, typeof(Weaker) },
        };
    }
    public class Resourse
    {
        public int FaceId { get; private set; }
        public int ArmorId { get; private set; }
        public int EquipmentId { get; private set; }
        public static Dictionary<int, string> Face { get; private set; }
        public static Dictionary<int, string> Armor { get; private set; }
        public static Dictionary<int, string> Equipment { get; private set; }
        static Resourse()
        {
            Face = new Dictionary<int, string> { { 0, "c:\\face1.png" }, { 1, "c:\\face2.png" }, { 2, "c:\\face3.png" } };
            Armor = new Dictionary<int, string> { { 0, "c:\\armor1.png" }, { 1, "c:\\armor2.png" }, { 2, "c:\\armor3.png" } };
            Equipment = new Dictionary<int, string> { { 0, "c:\\equipment1.png" }, { 1, "c:\\equipment2.png" }, { 2, "c:\\equipment3.png" } };
        }
        public Resourse()
        {
            this.ArmorId = new Random().Next(0, Armor.Count);
            Thread.Sleep(new Random().Next(5, 10));
            this.FaceId = new Random().Next(0, Face.Count);
            Thread.Sleep(new Random().Next(5, 10));
            this.EquipmentId = new Random().Next(0, Equipment.Count);
        }
    }
    public abstract class UnitBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public bool IsAlive { get; protected set; }
        public abstract int Attack();
        public abstract void Defend(int damage);
        public UnitBase()
        {
            this.Name = this.GetType().ToString().Split('.').Last();
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public abstract class Unit : UnitBase
    {
        public UnitState State { get; protected set; }
        public int Health { get; set; }
        public int InitialHealth { get; set; }
        public int Damage { get; set; }
        public int InitialDamage { get; set; }
        public int Resourses { get; protected set; }
        public override int Attack()
        {
            return this.State.Attack();
        }
        public override void Defend(int damage)
        {
            this.State.Defend(damage);
            if (this.Health <= 0)
                this.IsAlive = false;
        }
        public Unit(Type stateType, int initialHealth, int initialDamage)
        {
            this.InitialDamage = this.Damage = initialDamage;
            this.InitialHealth = this.Health = initialHealth;
            this.SetState(stateType);
        }
        public void ChangeState(Type stateType)
        {
            this.SetState(stateType);
        }
        private void SetState(Type stateType)
        {
            UnitState state = (UnitState)Activator.CreateInstance(stateType, new object[] { this });
            this.State = state;
        }
        public override string ToString()
        {
            return base.ToString() + " " + this.State.ToString();
        }
    }
    public abstract class UnitState : UnitBase
    {
        private Unit Unit { get; set; }
        public int HealthModifier { get; protected set; }
        public int DamageModifier { get; protected set; }
        public UnitState (Unit unit, int healthModifier, int damageModifier)
        {
            this.HealthModifier = healthModifier;
            this.DamageModifier = damageModifier;
            this.Unit = unit;
        }
        public override int Attack()
        {
            return this.Unit.Damage + this.DamageModifier;
        }
        public override void Defend(int damage)
        {
            this.Unit.Health -= damage + this.HealthModifier;
        }
    }
    public abstract class UnitWrapper : UnitBase
    {
        private Unit Unit { get; set; }
        public int InitialHealthModifier { get; protected set; }
        public int InitialDamageModifier { get; protected set; }
        public UnitWrapper(Unit unit, int initialHealthModifier, int initialDamageModifier)
        {
            this.InitialHealthModifier = initialHealthModifier;
            this.InitialDamageModifier = initialDamageModifier;
            this.Unit = unit;
            this.Unit.InitialHealth += this.InitialHealthModifier;
            this.Unit.Health += this.InitialHealthModifier;
            this.Unit.InitialDamage += this.InitialDamageModifier;
            this.Unit.Damage += this.InitialDamageModifier;
        }
        public override int Attack()
        {
            return this.Unit.Attack();
        }
        public override void Defend(int damage)
        {
            this.Unit.Defend(damage);
        }
        public override string ToString()
        {
            return base.ToString() + " " + this.Unit.ToString() + " Health: " + this.Unit.Health.ToString()
                   + " Damage: " + this.Unit.Damage.ToString() + " HealthBonus: " + this.Unit.State.HealthModifier.ToString()
                   + " DamageBonus: " + this.Unit.State.DamageModifier.ToString();
        }
    }
    public class Stronger : UnitWrapper
    {
        public Stronger(Unit unit) : base(unit, 50, 50) { } 
    }
    public class Strong : UnitWrapper
    {
        public Strong(Unit unit) : base(unit, 25, 25) { }
    }
    public class Normal : UnitWrapper
    {
        public Normal(Unit unit) : base(unit, 0, 0) { }
    }
    public class Weak : UnitWrapper
    {
        public Weak(Unit unit) : base(unit, -25, -25) { }
    }
    public class Weaker : UnitWrapper
    {
        public Weaker(Unit unit) : base(unit, -50, -50) { }
    }
    public class Hero : Unit
    {
        public Hero(Type stateType) : base(stateType, 200, 200) { }
    }
    public class Knight : Unit
    {
        public Knight(Type stateType) : base(stateType, 100, 100) { }
    }
    public class Warrior : Unit
    {
        public Warrior(Type stateType) : base(stateType, 50, 50) { }
    }
    public class Inspired : UnitState
    {
        public Inspired(Unit unit) : base(unit, 20, 20) { }
    }
    public class Fresh : UnitState
    {
        public Fresh(Unit unit) : base(unit, 10, 10) { }
    }
    public class Regular : UnitState
    {
        public Regular(Unit unit) : base(unit, 0, 0) { }
    }
    public class Indifferent : UnitState
    {
        public Indifferent(Unit unit) : base(unit, -10, -10) { }
    }
    public class Scared : UnitState
    {
        public Scared(Unit unit) : base(unit, -20, -20) { }
    }
}