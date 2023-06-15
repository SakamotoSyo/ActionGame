using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;

public class InputLifeTimeScope : LifetimeScope
{
    [SerializeField] private Transform _playerInsPos;
    [SerializeField] private ActorGenerator _actorGenerator;

    protected override void Configure(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();

        builder.RegisterMessageBroker<InputSendData>(options);
        builder.RegisterComponent<PlayerController>(_actorGenerator.PlayerGeneration());
        builder.RegisterEntryPoint<InputEventProvider>(Lifetime.Singleton);
        //builder.RegisterBuildCallback(resoler => resoler.Instantiate(_playerCon, _playerInsPos.transform.position, _playerInsPos.transform.rotation));
    }
}
