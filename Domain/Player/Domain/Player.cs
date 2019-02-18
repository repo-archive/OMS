using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Player.Command;

namespace Domain.Player.Domain
{
    public class Player : EventSourcedRootEntity
    {
        /// <summary> List of uncommitted changes </summary>
        public PlayerId Id { get; private set; }

    readonly PlayerState m_state;

        public Player(IEnumerable<IDomainEvent> events)
        {
            m_state = new PlayerState(events);
        }

        public void Create(PlayerId id, string name)
        {
            if (m_state.Created)
                throw new InvalidOperationException("Player already exist");

            Id = id;
            Apply(new PlayerCreated(Id, name, new Beginner()));
        }

        public void Renamed(string name)
        {
            if (m_state.Name == name)
                return;

            Apply(new PlayerRenamed(name));
        }

        public void SkillChanged(PlayerSkill newSkill)
        {
            if (m_state.Skill == newSkill)
                return;

            Apply(new PlayerSkillChanged(newSkill));
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }
    }
}