package crc646113692ae1c8472e;


public class BTNGamePieceRender
	extends crc643f46942d9dd1fff9.ImageButtonRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Slots_Shuffle.Droid.CustomRenders.BTNGamePieceRender, Vulcan_Shuffle.Android", BTNGamePieceRender.class, __md_methods);
	}


	public BTNGamePieceRender (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BTNGamePieceRender.class)
			mono.android.TypeManager.Activate ("Slots_Shuffle.Droid.CustomRenders.BTNGamePieceRender, Vulcan_Shuffle.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public BTNGamePieceRender (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BTNGamePieceRender.class)
			mono.android.TypeManager.Activate ("Slots_Shuffle.Droid.CustomRenders.BTNGamePieceRender, Vulcan_Shuffle.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public BTNGamePieceRender (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BTNGamePieceRender.class)
			mono.android.TypeManager.Activate ("Slots_Shuffle.Droid.CustomRenders.BTNGamePieceRender, Vulcan_Shuffle.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
