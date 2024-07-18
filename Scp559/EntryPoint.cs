using System;
using System.IO;
using Exiled.API.Features;

namespace Scp559;

// ReSharper disable ClassNeverInstantiated.Global
public class EntryPoint : Plugin<Config>
{
    public override string Author { get; } = "xNexusACS";

    public override string Name { get; } = "SCP-559";

    public override string Prefix { get; } = "scp_559";

    public override Version Version { get; } = new(0, 0, 1);

    public override Version RequiredExiledVersion { get; } = new(8, 9, 6);

    internal static EntryPoint Instance;

    private Scp559Manager _scp559Manager;

    public override void OnEnabled()
    {
        if (!File.Exists(Path.Combine(Paths.Plugins, "MapEditorReborn.dll")))
        {
            Log.Error("MapEditorReborn is missing!, aborting plugin startup.");
            return;
        }

        if (Config.CakeConfig.SpawnPoints.IsEmpty() || Config.CakeConfig.SchematicName.IsEmpty())
        {
            Log.Error("The SpawnPoints or SchematicName Configuration is Empty!, aborting plugin startup.");
            return;
        }

        Instance = this;

        _scp559Manager = new Scp559Manager(this);
        Exiled.Events.Handlers.Player.UsedItem += _scp559Manager.OnUsedItem;
        Exiled.Events.Handlers.Player.TogglingNoClip += _scp559Manager.OnToggleNoClip;
        Exiled.Events.Handlers.Player.VoiceChatting += _scp559Manager.OnVoiceChatting;
        Exiled.Events.Handlers.Player.Dying += _scp559Manager.OnDying;
        Exiled.Events.Handlers.Server.RoundStarted += _scp559Manager.OnRoundStart;
        Exiled.Events.Handlers.Server.EndingRound += _scp559Manager.OnEndingRound;
        
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.UsedItem -= _scp559Manager.OnUsedItem;
        Exiled.Events.Handlers.Player.TogglingNoClip -= _scp559Manager.OnToggleNoClip;
        Exiled.Events.Handlers.Player.VoiceChatting -= _scp559Manager.OnVoiceChatting;
        Exiled.Events.Handlers.Player.Dying -= _scp559Manager.OnDying;
        Exiled.Events.Handlers.Server.RoundStarted -= _scp559Manager.OnRoundStart;
        Exiled.Events.Handlers.Server.EndingRound -= _scp559Manager.OnEndingRound;

        _scp559Manager = null;
        Instance = null;
        base.OnDisabled();
    }
}