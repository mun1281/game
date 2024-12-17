using Assets.game.Scripts.Game.Gameplay.Commands;
using Assets.game.Scripts.Game.Gameplay.View.GameResources;
using Assets.game.Scripts.Game.State.cmd;
using Assets.game.Scripts.Game.State.GameResources;
using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;


namespace Assets.game.Scripts.Game.Gameplay.Services
{
    public class ResourcesService
    {
        public readonly ObservableList<ResourceViewModel> Resources = new();

        private readonly Dictionary<ResourceType, ResourceViewModel> _resourcesMap = new();
        private readonly ICommandProcessor _cmd;

        public ResourcesService(ObservableList<Resource> resources, ICommandProcessor cmd)
        {
            _cmd = cmd;

            resources.ForEach(CreateResourceViewModel);
            resources.ObserveAdd().Subscribe(e => CreateResourceViewModel(e.Value));
            resources.ObserveRemove().Subscribe(e => RemoveResourceViewModel(e.Value));
        }

        public bool AddResources(ResourceType resourceType, int amount)
        {
            var command = new CmdResourcesAdd(resourceType, amount);

            return _cmd.Process(command);
        }

        public bool TrySpendResources(ResourceType resourceType, int amount)
        {
            var command = new CmdResourcesSpend(resourceType, amount);

            return _cmd.Process(command);
        }

        public bool IsEnoughResources(ResourceType resourceType, int amount)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amaint.CurrentValue >= amount;
            }

            return false;
        }

        public Observable<int> ObserveResource(ResourceType resourceType)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amaint;
            }

            throw new Exception($"Resource of type {resourceType} doesn't exist");
        }

        private void CreateResourceViewModel(Resource resource)
        {
            var resourceViewModel = new ResourceViewModel(resource);
            _resourcesMap[resource.ResourceType] = resourceViewModel;

            Resources.Add(resourceViewModel);
        }

        private void RemoveResourceViewModel(Resource resource)
        {
            if (_resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel))
            {
                Resources.Remove(resourceViewModel);
                _resourcesMap.Remove(resource.ResourceType);
            }
        }
    }
}
