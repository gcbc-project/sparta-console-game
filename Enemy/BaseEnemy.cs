using SpartaConsoleGame.Enemy;
using SpartaConsoleGame.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public abstract class BaseEnemy : IEnemy
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public Stats Stats { get; set; }
        public List<ISkill> Skills { get; set; }
        public bool IsDead { get; set; }

        protected Random random;
        public BaseEnemy(Stats stats)
        {
            random = new Random();
            Stats = stats;
        }
        public virtual ConsoleBuilder GetEnemyInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Lv.{Level} ");
            sb.Append($"{Name} ");
            if (IsDead)
            {
                sb.Append("Dead");
            }
            else
            {
                sb.Append($"HP {Hp} / {Stats.Hp}");
            }
            return new ConsoleBuilder(sb.ToString(), () => IsDead, ConsoleColor.Red);
        }
        public virtual int Attack(out bool isCriticalHit)
        {
            int baseDamage = random.Next((int)Math.Round(Stats.Atk * 0.9f), (int)Math.Round(Stats.Atk * 1.1f));
            isCriticalHit = random.NextDouble() < (Stats.Crit - 1);
            if (isCriticalHit)
            {
                baseDamage = (int)(baseDamage * 1.6); // 치명타일 때 160% 데미지 적용
            }
            return baseDamage;
        }
        public string Hit(int damage, out bool isDodged, bool allowDodge = true)
        {
            isDodged = false;
            if (allowDodge && random.NextDouble() < (Stats.Eva - 1))
            {
                isDodged = true;
                return "Missed";
            }

            Hp -= damage;
            if (Hp <= 0)
            {
                Die();
                return "Dead";
            }
            return Hp.ToString();
        }

        public virtual void Die()
        {
            IsDead = true;
        }

        public virtual BaseEnemy DeepCopy()
        {
            // 새로운 인스턴스 생성
            BaseEnemy newEnemy = (BaseEnemy)Activator.CreateInstance(GetType());

            // 현재 객체의 속성을 복사하여 새로운 객체에 할당
            newEnemy.Name = this.Name;
            newEnemy.Level = this.Level;
            newEnemy.Hp = this.Hp;
            newEnemy.Stats = this.Stats.DeepCopy();
            newEnemy.IsDead = this.IsDead;

            // Deep Copy를 위해 Random 객체는 공유되지 않도록 새로 생성
            newEnemy.random = new Random();

            return newEnemy;
        }
    }
}