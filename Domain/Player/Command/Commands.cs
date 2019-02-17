using System;
using Domain.Common;
using Domain.Player.Domain;

namespace Domain.Player.Command
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerId Id { get; }
        public string Name { get; }
        public PlayerSkill Skill { get; }
        public DateTimeOffset OccuredOn { get; }

        public PlayerCreated(PlayerId id, string name, PlayerSkill skill)
        {
            Id = id;
            Name = name;
            Skill = skill;
            OccuredOn = DateTimeOffset.UtcNow;
        }
    }

    public class PlayerRenamed : IDomainEvent
    {
        public PlayerRenamed(string name)
        {
            Name = name;
            OccuredOn = DateTimeOffset.UtcNow;
        }

        public string Name { get; }

        public DateTimeOffset OccuredOn { get; }
    }

    public class PlayerSkillChanged : IDomainEvent
    {
        public PlayerSkillChanged(PlayerSkill skill)
        {
            Skill = skill;
            OccuredOn = DateTimeOffset.UtcNow;
        }

        public PlayerSkill Skill { get; }

        public DateTimeOffset OccuredOn { get; }
    }
}
