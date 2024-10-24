using System;
using System.Collections.Generic;

namespace DI
{
    public class DIContainer
    {
        // Контейнер родитель. 
        private readonly DIContainer _parentContainer;
        // Список зависимостей.
        private readonly Dictionary<(string, Type), DIRegistration> _registrations = new();
        // Кэш зависимостей.
        private readonly HashSet<(string, Type)> _resolutions = new(); 

        public DIContainer(DIContainer parentContainer = null)
        {
            _parentContainer = parentContainer; // Установка родительского контейнера
        }

        public void RegisterSingleton<T>(Func<DIContainer, T> factory)
        {
            RegisterSingleton(null, factory);
        }

        public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T));
            Register(key, factory, true);
        }

        public void RegisterTransient<T>(Func<DIContainer, T> factory)
        {
            RegisterTransient(null, factory);
        }

        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            var key = (tag, typeof(T)); // Создаем специальный ключ с определенным типом.
            Register(key, factory, false);
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }

        public void RegisterInstance<T>(string tag, T instance)
        {// Аналогично Register.
            var key = (tag, typeof(T));

            if (_registrations.ContainsKey(key))
            {
                throw new Exception(
                $"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} has already registered");
            }

            _registrations[key] = new DIRegistration
            {
                Instance = instance,
                IsSingleton = true
            };
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutions.Contains(key))
            {// Проверяет на циклическую зависимость.
                throw new Exception(
                $"DI: Cyclical dependency detected for tag {key.Item1} and type {key.Item2.FullName}");
            }
            // Кэшируем запрос
            _resolutions.Add(key);

            try
            {
                if (_registrations.TryGetValue(key, out var registration))
                {// Если есть значение по этому ключу то оно записывается в registration.
                    if (registration.IsSingleton)
                    {// Если синглтон то:
                        if (registration.Instance == null && registration.Factory != null)
                        {// Если нет Instance и есть фабрика то.
                            registration.Instance = registration.Factory(this);
                        }// Создаем Instance

                        return (T)registration.Instance;
                    }// Возврощаем в конвертированном варианте.

                    return (T)registration.Factory(this);// Не синглтон.
                }

                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutions.Remove(key);
            }

            throw new Exception(
                $"DI: No registration found for tag {key.Item1} and type {key.Item2.FullName}");
        }

        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (_registrations.ContainsKey(key))
            {// Существует ли заданный ключ в Dictionary.
                throw new Exception(
                    $"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} has already registered");
            }
            // Делаем регестрацию.
            _registrations[key] = new DIRegistration // Под этим ключом:
            {// Сохроняем эти данные.
                Factory = c => factory(c),
                IsSingleton = isSingleton
            };
        }
    }
}
