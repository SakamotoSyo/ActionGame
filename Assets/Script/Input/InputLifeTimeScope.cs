using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;

public class InputLifeTimeScope : LifetimeScope
{
    [SerializeField] private PlayerController _playerCon;
    [SerializeField] private Transform _playerInsPos;

    protected override void Configure(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();

        builder.RegisterMessageBroker<InputSendData>(options);
        builder.RegisterComponent<PlayerController>(_playerCon);
        builder.RegisterEntryPoint<InputEventProvider>(Lifetime.Singleton);

        //builder.RegisterBuildCallback(resoler => resoler.Instantiate(_playerCon, _playerInsPos.transform.position, _playerInsPos.transform.rotation));
    }
}
