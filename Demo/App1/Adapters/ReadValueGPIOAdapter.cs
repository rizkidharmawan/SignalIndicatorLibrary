using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1
{
    public class ReadValueGPIOAdapter : RecyclerView.Adapter
    {
        private List<string> equipmentTrackingList;
        private Context context;

        public ReadValueGPIOAdapter(List<string> equipmentTrackingList, Context context)
        {
            this.equipmentTrackingList = equipmentTrackingList;
            this.context = context;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.row_read_value_gpio, parent, false);

            return new ViewHolder(v);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                var viewHolder = holder as ViewHolder;
                if (viewHolder != null)
                {
                    var s = equipmentTrackingList[position];
                    viewHolder.ReadValue.Text = "Value: " + s;
                }
            }
            catch (Exception)
            {
                
            }
        }

        public override int ItemCount
        {
            get { return equipmentTrackingList.Count; }
        }

        public void UpdateList(List<string> stringList)
        {
            equipmentTrackingList = stringList;
            NotifyDataSetChanged();
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView ReadValue { get; private set; }

            public ViewHolder(View v) : base(v)
            {
                ReadValue = v.FindViewById<TextView>(Resource.Id.tvReadValue);
            }
        }
    }
}