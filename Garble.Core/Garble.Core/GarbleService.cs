using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Garble.Core
{
	public static class GarbleService
	{

		public static byte[] HalfVolume(byte[] bytes){
			var halfVolume = new List<short> ();
			var bList = new List<byte> ();

			/*			if (BitConverter.IsLittleEndian)
				Array.Reverse (bytes);*/

			for (var i = 0; i < bytes.Length; i = i + 2) {
				short half = Convert.ToInt16 (BitConverter.ToInt16 (bytes, i)/4);
				halfVolume.Add (half);
			}

			for (var i = 0; i < halfVolume.Count; i++) {
				bList.AddRange (BitConverter.GetBytes (halfVolume [i]));
			}
			return bList.ToArray (); 
		}
		public static byte[] DoNothing(byte[] bytes){
			var halfVolume = new List<short> ();
			var bList = new List<byte> ();

			/*if (BitConverter.IsLittleEndian)
				Array.Reverse (bytes);*/

			for (var i = 0; i < bytes.Length; i = i + 2) {
				short half = Convert.ToInt16 (BitConverter.ToInt16 (bytes, i));
				halfVolume.Add (half);
			}

			for (var i = 0; i < halfVolume.Count; i++) {
				bList.AddRange (BitConverter.GetBytes (halfVolume [i]));
			}
			return bList.ToArray (); 
		}
		public static async Task<byte[]> DoNothingAsync(byte[] bytes){
			return await Task.Run (() => DoNothing(bytes));
		}
	}
}

