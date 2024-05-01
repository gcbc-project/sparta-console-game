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
        public int Atk { get; set; }
        public bool IsDead { get; set; }
        public int MaxHp { get; set; }

        protected Random random;
        public BaseEnemy()
        {
            random = new Random();
        }
        public virtual string GetEnemyInfo()
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
                sb.Append($"HP {Hp}");
            }
            return sb.ToString();
        }
        public virtual string Hit(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Die();
                return "Dead";
            }
            return Hp.ToString();
        }
        public virtual int Attack()
        {
            return random.Next((int)Math.Round(Atk * 0.9f), (int)Math.Round(Atk * 1.1f));
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
            newEnemy.Atk = this.Atk;
            newEnemy.IsDead = this.IsDead;

            // Deep Copy를 위해 Random 객체는 공유되지 않도록 새로 생성
            newEnemy.random = new Random();

            return newEnemy;
        }
    }
}