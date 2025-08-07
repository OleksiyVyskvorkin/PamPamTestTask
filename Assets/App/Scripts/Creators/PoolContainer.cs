using Game.Data;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Runtime.Characters;
using Game.Services;
using Game.Runtime;
using Game.UI;
using Game.Infrastructure;

namespace Game.Creators
{
    public class PoolContainer : MonoBehaviour
    {
        public List<Enemy> ActiveEnemies { get; private set; } = new List<Enemy>();
        public PlayerCharacter Character { get; private set; }

        private Dictionary<UnitType, Queue<Enemy>> _enemies = new Dictionary<UnitType, Queue<Enemy>>();      
        private Queue<Projectile> _projectiles = new Queue<Projectile>();

        private CreatableConfig _creatableConfig;
        private ParentsHolder _parentsHolder;
        private Factory _factory;
        private ExecuteHandler _executeHandler;
        private ScoreHandler _scoreHandler;
        private GameController _gameController;

        [Inject]
        public void Construct(Factory factory,
            MainConfig config,
            ParentsHolder parentsHolder,
            ExecuteHandler executeHandler,
            ScoreHandler scoreHandler,
            GameController gameController)
        {
            _factory = factory;
            _creatableConfig = config.CreatableConfig;
            _parentsHolder = parentsHolder;
            _executeHandler = executeHandler;
            _scoreHandler = scoreHandler;
            _gameController = gameController;
            _gameController.OnLoseGame += ReturnAllEnemies;
        }

        private void Start()
        {
            CreateCharacter();
        }

        private void CreateCharacter()
        {
            Character = _factory.Create<PlayerCharacter>(_creatableConfig.Character);
            Character.HealthSystem.OnDie += _gameController.LoseGame;
            Character.transform.SetParent(_parentsHolder.EntityContainer, true);
            _gameController.OnStartPlay += Character.HealthSystem.ResetHealth;
        }

        public Enemy GetEnemy(UnitType enemyType)
        {
            Enemy enemy = null;
            if (!_enemies.ContainsKey(enemyType)) _enemies.Add(enemyType, new Queue<Enemy>());

            if (_enemies[enemyType].Count > 0) enemy = _enemies[enemyType].Dequeue();
            else
            {
                enemy = _factory.Create<Enemy>(_creatableConfig.GetEnemy(enemyType));
                enemy.OnReturnToPool += ReturnEnemy;
                enemy.HealthSystem.OnDie += _scoreHandler.AddScore;
                enemy.transform.SetParent(_parentsHolder.EntityContainer, true);
            }

            _executeHandler.AddToUpdate(enemy);
            ActiveEnemies.Add(enemy);
            return enemy;
        }

        public Projectile GetProjectile()
        {
            Projectile projectile = null;
            if (_projectiles.Count > 0) projectile = _projectiles.Dequeue();
            else
            {
                projectile = _factory.Create<Projectile>(_creatableConfig.Projectile);
                projectile.OnReturnToPool += ReturnProjectile;
            }

            _executeHandler.AddToUpdate(projectile);
            return projectile;
        }

        private void ReturnProjectile(Projectile projectile)
        {
            _executeHandler.RemoveFromUpdate(projectile);
            _projectiles.Enqueue(projectile);
        }

        private void ReturnAllEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            enemies.AddRange(ActiveEnemies);
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
                ReturnEnemy(enemy);
            }
        }

        private void ReturnEnemy(Enemy enemy)
        {
            _executeHandler.RemoveFromUpdate(enemy);
            ActiveEnemies.Remove(enemy);
            _enemies[enemy.UnitType].Enqueue(enemy);
        }
    }
}
