using Game.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Game.Runtime
{
    public class ExecuteHandler : MonoBehaviour
    {
        private Dictionary<Type, List<IExecutable>> _executables = new Dictionary<Type, List<IExecutable>>();
        private bool _isUpdateCompleted;

        private void Update()
        {
            _isUpdateCompleted = false;
            foreach (var executables in _executables.Values)
            {
                foreach (var executable in executables)
                {
                    executable.Execute();
                }
            }
            _isUpdateCompleted = true;
        }

        public void AddToUpdate(IExecutable executable)
        {
            if (_executables.TryGetValue(executable.GetType(), out var executables))
            {
                if (!executables.Contains(executable))
                {
                    executables.Add(executable);
                }
            }
            else if (!_executables.ContainsKey(executable.GetType()))
            {
                var list = new List<IExecutable>() { executable };
                _executables.Add(executable.GetType(), list);
            }
        }

        public async void RemoveFromUpdate(IExecutable executable)
        {
            await UniTask.WaitWhile(() => _isUpdateCompleted, PlayerLoopTiming.LastTimeUpdate);
            if (_executables.ContainsKey(executable.GetType()) == false) return;
            if (_executables[executable.GetType()].Contains(executable) == false) return;
            _executables[executable.GetType()].Remove(executable);
        }
    }
}