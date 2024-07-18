using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace Scp559.Utilities.Components;

public class Scp559SizeEffect : MonoBehaviour
{
    internal void InitializeComponent(Player player) => _player = player;
    
    private Player _player;
    
    private void Update()
    {
        if (!(_player.Scale.x > EntryPoint.Instance.Config.CakeConfig.PlayerScaleUnderCakeEffect.x)) return;
        
        _player.EnableEffect(EffectType.Ensnared, duration: 1f);
        _player.Scale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;

        if (_player.Scale.x < EntryPoint.Instance.Config.CakeConfig.PlayerScaleUnderCakeEffect.x)
            _player.Scale = EntryPoint.Instance.Config.CakeConfig.PlayerScaleUnderCakeEffect;
    }

    private void OnDestroy() => _player = null;
}