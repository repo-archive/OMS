using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Player.Command;

namespace Domain.Player.Domain
{
    public class PlayerState
    {
        public PlayerState(IEnumerable<IDomainEvent> events)
        {
            foreach (var e in events)
            {
                Mutate(e);
            }
        }

        public PlayerId Id { get; private set; }
        public string Name { get; private set; }
        public bool Created { get; private set; }
        public PlayerSkill Skill { get; private set; }

        public void Mutate(IDomainEvent e)
        {
            ((dynamic)this).When((dynamic)e);
        }

        public void When(PlayerAdded e)
        {
            Id = e.Id;
            Name = e.Name;
            Skill = e.Skill;
            Created = true;
        }

        public void When(PlayerRenamed e)
        {
            Name = e.Name;
        }

        public void When(PlayerSkillChanged e)
        {
            Skill = e.Skill;
        }
    }
}
