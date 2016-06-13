#if DEBUG
#else
using UnityEngine;
#endif
using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
#if SKY_PROTOBUF
using ProtoBuf;
#endif

namespace NET {
    public class UtilLog {
        public enum LogType {
            Log,
            Error,
            Warning,
        }

        public static void Log(String message, LogType type) {
            switch (type) {
                case LogType.Log:
                    Debug.Log(message);
                    break;
                case LogType.Error:
                    Debug.LogError(message);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;
            }

// #if SERVER
//             switch (type) {
//                 case LogType.Error:
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     break;
//                 case LogType.Warning:
//                     Console.ForegroundColor = ConsoleColor.Yellow;
//                     break;
//                 default:
//                     Console.ForegroundColor = ConsoleColor.White;
//                     break;
//             }
//             Console.WriteLine("{0}:{1}", type.ToString(), message);
// #elif DEBUG
//             Console.WriteLine("{0}:{1}", type.ToString(), message);
// #else
//             switch (type) {
//                 case LogType.Log:
//                     Debug.Log(message);
//                     break;
//                 case LogType.Error:
//                     Debug.LogError(message);
//                     break;
//                 case LogType.Warning:
//                     Debug.LogWarning(message);
//                     break;
//             }
//             GMConsole.Log(message, type.ToString());
// #endif
        }

        public static void Log(string format, params object[] args) {
            string str = "";
            try {
                str = string.Format(format, args);
            } catch (Exception) {
                str = "字符串格式化错误:" + format;
            }
            Log(str);
        }

        public static void LogError(string format, params object[] args) {
            string str = "";
            try {
                str = string.Format(format, args);
            } catch (Exception) {
                str = "字符串格式化错误:" + format;
            }
            LogError(str);
        }

        public static void LogWarning(string format, params object[] args) {
            string str = "";
            try {
                str = string.Format(format, args);
            } catch (Exception) {
                str = "字符串格式化错误:" + format;
            }
            LogWarning(str);
        }

        public static void LogError(string message) {
            Log(message, LogType.Error);
        }

        public static void LogWarning(string message) {
            Log(message, LogType.Warning);
        }

        public static void Log(string message) {
            Log(message, LogType.Log);
        }

        public static String ToXmlString<T>(T t) {
            using (StringWriter w = new StringWriter()) {
                XmlSerializer x = new XmlSerializer(t.GetType());
                x.Serialize(w, t);
                return w.ToString();
            }
        }

#if SKY_PROTOBUF
        public static byte[] ProtoToByte<T>(T game) {
            byte[] buf = new byte[1024];
            MemoryStream stream = new MemoryStream(buf);
            ProtoBuf.Serializer.Serialize<T>(stream, game);
            byte[] new_buf = new byte[stream.Position];
            Array.Copy(buf, new_buf, new_buf.Length);
            return new_buf;
        }

        public static T ByteToProto<T>(byte[] b) {
            MemoryStream stream = new MemoryStream(b);
            return ProtoBuf.Serializer.Deserialize<T>(stream);
        }

        public static byte[] ProtoToDB<T>(T game) {
            byte[] buf = new byte[1024];
            MemoryStream stream = new MemoryStream(buf);
            ProtoBuf.Serializer.Serialize<T>(stream, game);
            if (stream.Position == 0) {
                return null;
            } else {
                byte[] new_buf = new byte[stream.Position];
                Array.Copy(buf, new_buf, new_buf.Length);
                return new_buf;
            }
        }

        public static T DBToProto<T>(byte[] b) {
            if (b == null) {
                return default(T);
            }
            MemoryStream stream = new MemoryStream(b);
            return ProtoBuf.Serializer.Deserialize<T>(stream);
        }
#endif

        /*
		/// <summary>
		/// 计算文件的md5值，返回大写格式
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string GenerateFileMD5Upper(string url) {
			if (File.Exists(url) == false) {
				return string.Empty;
			}
			
			byte[] fileByte = File.ReadAllBytes(url);			
			if (fileByte == null) {
				return string.Empty;
			}
			
			byte[] hashByte = new MD5CryptoServiceProvider().ComputeHash(fileByte);			
			return ByteArrayToString(hashByte);
		}
        */

        /// <summary>
        /// 输出数据的十六进制字符串
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] arrInput) {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++) {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}

