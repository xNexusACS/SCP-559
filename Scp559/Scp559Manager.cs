using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.Events.EventArgs;
using MEC;
using Scp559.Utilities.Components;
using Scp559.Utilities.Voice;
using UnityEngine;
using Random = System.Random;

namespace Scp559;

public class Scp559Manager
{
    private readonly EntryPoint _entryPoint;
    
    private readonly List<CoroutineHandle> _coroutines = new();
    
    public Scp559Manager(EntryPoint entryPoint) => _entryPoint = entryPoint;
    
    internal void OnSchematicSpawned(SchematicSpawnedEventArgs args)
    {
        if (args.Name != _entryPoint.Config.CakeConfig.SchematicName)
            return;
        
        args.Schematic.gameObject.AddComponent<Scp559Cake>().InitializeComponent(args.Schematic);
    }

    internal void OnUsedItem(UsedItemEventArgs args)
    {
        if (args.Item?.Type is not ItemType.SCP500)
            return;

        if (args.Player.Scale == Vector3.one)
            return;

        if (!args.Player.GameObject.TryGetComponent(out Scp559SizeEffect scp559Effect))
            return;
        
        Object.Destroy(scp559Effect);
        args.Player.GameObject.AddComponent<Scp559RestoreEffect>().InitializeComponent(args.Player);
    }

    internal void OnToggleNoClip(TogglingNoClipEventArgs args)
    {
        if (args.Player.IsNoclipPermitted || args.Player.IsScp)
            return;
        
        /*if (_cakeModel is null)
            return;
        
        args.IsAllowed = false;

        if (Vector3.Distance(args.Player.Position, _cakeModel.Position) >= 2.5f)
            return;*/
        
        if (!args.Player.GameObject.TryGetComponent(out Scp559SizeEffect _))
            args.Player.GameObject.AddComponent<Scp559SizeEffect>().InitializeComponent(args.Player);
    }

    internal void OnVoiceChatting(VoiceChattingEventArgs args)
    {
        if (!args.Player.GameObject.TryGetComponent(out Scp559SizeEffect _))
            return;

        args.VoiceMessage = VoicePitchUtilities.SetVoicePitch(args.VoiceMessage);
    }

    internal void OnDying(DyingEventArgs args)
    {
        if (!args.Player.GameObject.TryGetComponent(out Scp559SizeEffect scp559Effect))
            return;
        
        Object.Destroy(scp559Effect);
        args.Player.Scale = Vector3.one;
    }

    internal void OnRoundStart()
    {
        _coroutines.Add(Timing.RunCoroutine(CakeSpawnHandler()));
        _coroutines.Add(Timing.RunCoroutine(SlicePickupIndicator()));
    }

    internal void OnEndingRound(EndingRoundEventArgs _)
    {
        foreach (CoroutineHandle coroutine in _coroutines)
            Timing.KillCoroutines(coroutine);
        
        _coroutines.Clear();
    }

    private IEnumerator<float> CakeSpawnHandler()
    {
        yield return Timing.WaitForSeconds(_entryPoint.Config.CakeConfig.FirstCakeSpawnDelay);

        /*while (true)
        {
            if (Round.IsEnded)
                yield break;

            Room room = Room.Get(GetRandomRoom());
            Vector3 spawnPoint = _entryPoint.Config.CakeConfig.SpawnPoints[room.Type] + Vector3.down * 1.8f;

            yield return Timing.WaitForSeconds(5f);
            
            _cakeModel = ObjectSpawner.SpawnSchematic(_entryPoint.Config.CakeConfig.SchematicName, room.WorldPosition(spawnPoint), null, null, MapUtils.GetSchematicDataByName(_entryPoint.Config.CakeConfig.SchematicName));

            yield return Timing.WaitForSeconds(_entryPoint.Config.CakeConfig.DisappearDelay);
            
            _cakeModel.Destroy();
            _cakeModel = null;

            yield return Timing.WaitForSeconds(_entryPoint.Config.CakeConfig.NormalSpawnDelay);
        }*/
    }
    
    private IEnumerator<float> SlicePickupIndicator()
    {
        while (true)
        {
            if (Round.IsEnded)
                yield break;

            foreach (Player player in Player.List)
            {
                // TODO: Hint
            }

            yield return Timing.WaitForSeconds(1f);
        }
    }
    
    private RoomType GetRandomRoom()
    {
        Random random = new Random();
        
        List<RoomType> roomNames = _entryPoint.Config.CakeConfig.SpawnPoints.Keys.ToList();
        
        int index = random.Next(roomNames.Count);
        
        return roomNames[index];
    }
}