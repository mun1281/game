using System;
using System.Collections.Generic;

namespace Assets.game.Scripts.Game.State.cmd
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IGameStateProvider _gameStateProvider;
        
        private readonly Dictionary<Type, object> _handlesMap = new();

        public CommandProcessor(IGameStateProvider gameStateProvider)
        {// Получаем ссылку на класс упровляющий состоянием (Данными) игры.
            _gameStateProvider = gameStateProvider;
        }

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {// Регестрируем какойто Handler который принимает какую-то команду реализующею интерфейс ICommand.
            _handlesMap[typeof(TCommand)] = handler;// Сохроняем этот Handler под типом принимаймой в него команды.
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {// Получаем эту команду с данными.
            if (_handlesMap.TryGetValue(typeof(TCommand), out var handle))
            {// Достаем Handler для этой команды.
                var typedHandker = (ICommandHandler<TCommand>)handle;
                var result = typedHandker.Handle(command);// Кладем в него эту команду

                if (result)
                {
                    _gameStateProvider.SaveGameState();
                }

                return result;
            }

            return false;
        }
    }
}
