using Android.App;
using Android.Widget;
using Android.OS;

namespace Garble.Android
{
	[Activity (Label = "Garble.Android", MainLauncher = true)]
	public class Record : Activity
	{
		Button startRecord =null;
		Button stopRecord = null;
		Button playback = null;
		Button playbackHalf = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Record);

			startRecord = FindViewById<Button> (Resource.Id.recordButton);
			startRecord.Click += delegate {
				DisableButtons();
				Garble.Android.AudioService.StartRecorder();
				stopRecord.Enabled=true;
			};

			stopRecord = FindViewById<Button> (Resource.Id.stopRecordButton);
			stopRecord.Click += async delegate(object sender, System.EventArgs e) {
				DisableButtons();
				await Garble.Android.AudioService.StopRecorderAsync();
				startRecord.Enabled=true;
				playback.Enabled = true;
				playbackHalf.Enabled=true;
			};

			playback = FindViewById<Button> (Resource.Id.playbackButton);
			playback.Click += async delegate(object sender, System.EventArgs e) {
				DisableButtons();
				await Garble.Android.AudioService.Play();
				startRecord.Enabled=true;
				playback.Enabled = true;
				playbackHalf.Enabled=true;
			};

			playbackHalf = FindViewById<Button> (Resource.Id.playbackHalfButton);
			playbackHalf.Click += async delegate(object sender, System.EventArgs e) {
				DisableButtons();
				await Garble.Android.AudioService.PlayAtHalf();
				startRecord.Enabled=true;
				playback.Enabled = true;
				playbackHalf.Enabled=true;
			};

			DisableButtons ();
			startRecord.Enabled = true;
		}

		void DisableButtons(){
			startRecord.Enabled = false;
			stopRecord.Enabled = false;
			playback.Enabled = false;
			playbackHalf.Enabled = false;
		}
	}
}


