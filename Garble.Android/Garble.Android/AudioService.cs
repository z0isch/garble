using System;
using System.Threading.Tasks;
using Android.Media;

namespace Garble.Android
{
	public static class AudioService
	{
		static byte[] audioBuffer = null;
		static AudioRecord audioRecord = null;
		static AudioTrack audioTrack = null;

		static bool endRecording = false;
		static Task Recording;

		public static bool IsRecording = false;


		static void ReadAudio()
		{
			var writtenBytes = 0;
			Recording = Task.Run( () => {
				while (true) {
					if (endRecording) {
						endRecording = false;
						break;
					}
					try {
						int numBytes = audioRecord.Read(audioBuffer, writtenBytes, audioBuffer.Length);
						writtenBytes += numBytes;
					} catch (Exception ex) {
						Console.Out.WriteLine (ex.Message);
						break;
					}
				}
				audioRecord.Stop ();
				audioRecord.Release ();
				IsRecording = false;
			});
		}

		public static void StartRecorder()
		{
			endRecording = false;
			IsRecording = true;
			audioBuffer = new Byte[100000];
			audioRecord = new AudioRecord (
				// Hardware source of recording.
				AudioSource.Mic,
				// Frequency
				11025,
				// Mono or stereo
				ChannelIn.Mono,
				// Audio encoding
				Encoding.Pcm16bit,
				// Length of the audio clip.
				audioBuffer.Length
			);

			audioRecord.StartRecording ();

			ReadAudio();
		}

		public static async Task StopRecorderAsync(){
			endRecording = true;
			await Recording;
		}
		public static async Task PlayAtHalf(){
			audioTrack = new AudioTrack (
				// Stream type
				Stream.Music,
				// Frequency
				11025,
				// Mono or stereo
				ChannelConfiguration.Mono,
				// Audio encoding
				Encoding.Pcm16bit,
				// Length of the audio clip.
				audioBuffer.Length,
				// Mode. Stream or static.
				AudioTrackMode.Stream);

			audioTrack.Play ();

			byte[] newBuffer = Garble.Core.GarbleService.HalfVolume (audioBuffer);
			await audioTrack.WriteAsync (newBuffer, 0, newBuffer.Length);
		}
		public static async Task Play()
		{
			audioTrack = new AudioTrack (
				// Stream type
				Stream.Music,
				// Frequency
				11025,
				// Mono or stereo
				ChannelConfiguration.Mono,
				// Audio encoding
				Encoding.Pcm16bit,
				// Length of the audio clip.
				audioBuffer.Length,
				// Mode. Stream or static.
				AudioTrackMode.Stream);

			audioTrack.Play ();

			byte[] newBuffer = Garble.Core.GarbleService.DoNothing (audioBuffer);
			await audioTrack.WriteAsync (newBuffer, 0, newBuffer.Length);
		}

		public static void StopPlay()
		{
			if (audioTrack != null) {
				audioTrack.Stop ();
				audioTrack.Release ();
				audioTrack = null;
			}
		}
	}
}

