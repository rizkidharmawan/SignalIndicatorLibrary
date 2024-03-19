package crc646565c02039a9a03f;


public class ReadValueGPIOAdapter_ViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App1.ReadValueGPIOAdapter+ViewHolder, SampleGpioPort", ReadValueGPIOAdapter_ViewHolder.class, __md_methods);
	}


	public ReadValueGPIOAdapter_ViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == ReadValueGPIOAdapter_ViewHolder.class)
			mono.android.TypeManager.Activate ("App1.ReadValueGPIOAdapter+ViewHolder, SampleGpioPort", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
