using System;
using System.Collections.Generic;
using Game.Gameplay.EntitiesCore;
using Game.Gameplay.Features.TeamsFeatures;
using Game.UI.CommonViews;
using Game.UI.Core;
using Game.Utility.Reactive;
using UnityEngine;

namespace Game.UI.Gameplay.HealthDisplay
{
    public class EntityHealthPresenter : IPresenter
    {
        private BarWithText _bar;
        private Entity _entity;
        private ReactiveVariable<Teams> _team;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<float> _maxHealth;
        
        private List<IDisposable> _disposables = new();

        public EntityHealthPresenter(Entity entity, BarWithText bar)
        {
            _entity = entity;
            _bar = bar;
        }

        public BarWithText Bar => _bar;
        
        public void Initialize()
        {
            _health = _entity.CurrentHealth;
            _maxHealth = _entity.MaxHealth;
            _team = _entity.Team;
            
            _disposables.Add(_health.Subscribe(OnHealthChanged));
            _disposables.Add(_maxHealth.Subscribe(OnMaxHealthChanged));
            _disposables.Add(_team.Subscribe(OnTeamhChanged));

            UpdateHealth();
            UpdateFillerColorBy(_team.Value);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
        
        private void OnTeamhChanged(Teams oldTeam, Teams newTeam) => UpdateFillerColorBy(newTeam);

        private void OnHealthChanged(float arg1, float arg2) => UpdateHealth();
        private void OnMaxHealthChanged(float arg1, float arg2) => UpdateHealth();

        private void UpdateHealth()
        {
            _bar.UpdateText(_health.Value.ToString("0"));
            _bar.UpdateSlider(_health.Value / _maxHealth.Value);
        }
        
        private void UpdateFillerColorBy(Teams team)
        {
            if (team == Teams.MainHero)
                _bar.SetFillerColor(Color.green);
            else if (team == Teams.Enemies)
                _bar.SetFillerColor(Color.red);
        }
    }
}