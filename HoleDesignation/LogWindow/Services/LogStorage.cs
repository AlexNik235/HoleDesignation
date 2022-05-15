namespace LogWindow.Services
{
    using System;
    using System.Collections.Generic;
    using Abstractions;
    using CSharpFunctionalExtensions;
    using Extensions;
    using Models;

    /// <inheritdoc />
    public class LogStorage : ILogStorage
    {
        private readonly HashSet<BaseMessage> _sourceItems = new ();
        private ResultMessage _resultMessage;

        /// <inheritdoc />
        public event EventHandler ElementStorageChanged;

        /// <inheritdoc />
        public void AddMessage<T>(T message)
            where T : BaseMessage
        {
            if (message is ResultMessage resultMessage)
            {
                _resultMessage = resultMessage;
                EventChanged();
                return;
            }

            HasMessageToUnion(message)
            .Match(
                parent =>
                {
                    _sourceItems.Add(parent.UnionMessages(message));
                    EventChanged();
                },
                _ =>
                {
                    if (_sourceItems.TryAdd(message))
                    {
                        EventChanged();
                    }
                });
        }

        /// <inheritdoc />
        public IEnumerable<BaseMessage> GetMessages()
        {
            foreach (var baseMessage in _sourceItems)
                yield return baseMessage;

            if (_resultMessage is not null)
            {
                yield return _resultMessage;
            }
        }

        /// <inheritdoc />
        public bool HasMessages() => _sourceItems.Count > 0;

        /// <inheritdoc />
        public void Clear()
        {
            _sourceItems.Clear();
            EventChanged();
        }

        private Result<ICanBeUnion<T>> HasMessageToUnion<T>(T message)
            where T : BaseMessage
        {
            foreach (var baseMessage in _sourceItems)
            {
                if (baseMessage is ICanBeUnion<T> parent && parent.CanUnionWith(message))
                {
                    _sourceItems.Remove(baseMessage);
                    return Result.Success(parent);
                }
            }

            return Result.Failure<ICanBeUnion<T>>("UnionError");
        }

        private void EventChanged() => ElementStorageChanged?.Invoke(this, null);
    }
}
