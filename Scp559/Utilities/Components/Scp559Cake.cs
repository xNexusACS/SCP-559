using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace Scp559.Utilities.Components;

public class Scp559Cake : MonoBehaviour
{
    private MapEditorObject _cakeModel;

    private bool _isSpawned;

    internal void InitializeComponent(MapEditorObject cakeObject) => _cakeModel = cakeObject;

    private void Update()
    {
        
    }
}