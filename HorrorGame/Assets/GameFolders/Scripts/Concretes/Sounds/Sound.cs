
using UnityEngine;

public class Sound 
{
	public readonly SoundType Type;
    public readonly LayerMask Layer;
    public readonly Vector3 Pos;
	public readonly float Range;
	public Sound(Vector3 pos, float range, SoundType type, LayerMask layer)
	{
		Layer= layer;
		Type= type;
		Pos = pos;
		Range = range;
	}
}
