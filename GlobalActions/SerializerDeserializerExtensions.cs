using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GlobalActions {
	public static class SerializerDeserializerExtensions {
		public static byte[] Serialize(this object data) {
			using var memoryStream = new MemoryStream();
			IFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, data);
			byte[] bytes = memoryStream.ToArray();
			return bytes;
		}

		public static T Deserializer<T>(this byte[] byteArray) {
			using var memoryStream = new MemoryStream(byteArray);
			IFormatter binaryFormatter = new BinaryFormatter();
			var returnValue = (T) binaryFormatter.Deserialize(memoryStream);
			return returnValue;
		}
	}
}