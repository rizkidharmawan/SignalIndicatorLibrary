package crc64ea6faeecbff60522;


public class GetSatellites
	extends android.location.GnssStatus.Callback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSatelliteStatusChanged:(Landroid/location/GnssStatus;)V:GetOnSatelliteStatusChanged_Landroid_location_GnssStatus_Handler\n" +
			"";
		mono.android.Runtime.register ("App1.Utils.GetSatellites, SampleGpioPort", GetSatellites.class, __md_methods);
	}


	public GetSatellites ()
	{
		super ();
		if (getClass () == GetSatellites.class)
			mono.android.TypeManager.Activate ("App1.Utils.GetSatellites, SampleGpioPort", "", this, new java.lang.Object[] {  });
	}


	public void onSatelliteStatusChanged (android.location.GnssStatus p0)
	{
		n_onSatelliteStatusChanged (p0);
	}

	private native void n_onSatelliteStatusChanged (android.location.GnssStatus p0);

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
