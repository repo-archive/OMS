using System.Collections.Generic;
using Domain.Common;

namespace Domain.Player.Domain
{
    public class PlayerSkill : ValueObject
    {
        public int Id;
        public string Skill;

        public PlayerSkill()
        {
            Id = -1;
            Skill = string.Empty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Skill;
        }
    }

    public class Beginner : PlayerSkill
    {
        public Beginner()
        {
            Id = 1;
            Skill = "Beginner";
        }
    }

    public class Immediate : PlayerSkill
    {
        public Immediate()
        {
            Id = 2;
            Skill = "Immediate";
        }
    }

    public class Expert : PlayerSkill
    {
        public Expert()
        {
            Id = 3;
            Skill = "Expert";
        }
    }

}
