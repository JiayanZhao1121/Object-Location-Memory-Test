using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading;

    public class FtpUpload
    {
	  


		//public bool triggerUpload = false; 

		protected static string baseURL = "ftp://ftp.ems.psu.edu/pub2/incoming/sfs5919_1MzqPch3qz";
		protected static string userName = "ftp";
		protected static string password = "sfs5919@psu.edu";
		
		protected static Thread uploadThread;

		private static string _file;
		private static string _uploadName;

		private static bool _finished = true;
		public static bool IsFinished {
			get { return _finished; }
		}

		private static bool _lastUploadSucceeded = false; 
		public static bool LastUploadSucceeded {
			get { return _lastUploadSucceeded; }
		}

		public static void uploadFile (string file, string uploadName)
		{
			if (_finished) {
				_file = file;
				_uploadName = uploadName;
				uploadThread = new Thread (uploadThreadFunction);
				_finished = false;
				_lastUploadSucceeded = false;
				uploadThread.Start ();
			}
		}


		public static void uploadThreadFunction() {
		// to test failure
//		Logging.logMessage ("Simulating failure of upload");
//		_lastUploadSucceeded = false;
//		_finished = true;
//		return;



		Debug.Log ("Attempting FTP upload of log file");
		bool success = false;

		// Get the object used to communicate with the server.
		FtpWebRequest request = (FtpWebRequest)WebRequest.Create (Path.Combine (baseURL, _uploadName));
		request.Method = WebRequestMethods.Ftp.UploadFile;

		// This example assumes the FTP site uses anonymous logon.
		request.Credentials = new NetworkCredential (userName, password);

		// Copy the contents of the file to the request stream.
		byte[] fileContents;

		using (var stream = GenerateStreamFromString(_file)) {

            StreamReader sourceStream = new StreamReader(stream);
            fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());


        }

  




        request.ContentLength = fileContents.Length;
		request.Timeout = 5000;

		using (Stream requestStream = request.GetRequestStream ()) {
			requestStream.Write (fileContents, 0, fileContents.Length);
		}

		using (FtpWebResponse response = (FtpWebResponse)request.GetResponse ()) {
			Debug.Log("Upload File Complete, status " + response.StatusDescription);
			if (response.StatusCode == FtpStatusCode.CommandOK || response.StatusCode == FtpStatusCode.ClosingControl)
				success = true;
		}
	
        
			_lastUploadSucceeded = success;
			_finished = true;
		}



    //		public void Update() {
    //		if (triggerUpload) {
    //			bool status = FtpUpload.uploadFile (Logging.logPath, "NationalParkStudy1_ " + "xyz" + "_" + DateTime.Now.ToString ().Replace ("\\", "-").Replace ("/", "-").Replace (":", "_") + ".txt");
    //			Debug.Log ("ftp upload result:" + status);	
    //			triggerUpload = false;
    //		}
    //		}
 
 
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }


}

