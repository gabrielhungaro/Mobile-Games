using UnityEngine;
using System;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Utils;

//Smarfox Packages
using Sfs2X;
using Sfs2X.Core;

using Signals;

namespace AquelaFrameWork.Server
{
    public class AFServer : ASingleton<AFServer>
    {
        private SmartFox sfs;
        public Sfs2X.Util.ConfigData sfsConfig;
        private Sfs2X.Logging.LogLevel logLevel = Sfs2X.Logging.LogLevel.DEBUG;

        public delegate void OnSmartfoxResponse(BaseEvent evt);

        public OnSmartfoxResponse onConnection;
        public OnSmartfoxResponse onLogin;
        public OnSmartfoxResponse onLoginError;
        public OnSmartfoxResponse onLogout;

        public Signal<bool> onPause = new Signal<bool>();

        public AFServer()
        {
            InitSmarfoxServer();
            
            onPause.Add(OnSistemPause);
            onPause.Dispatch(true);
        }

        public void OnSistemPause(bool evt)
        {
            UnityEngine.Debug.Log("Que loucura funcionou");
        }


        public void InitSmarfoxServer()
        {
            UnityEngine.Debug.Log("Initializing the SmartFox");
            sfs = new SmartFox(true);

            UnityEngine.Debug.Log("Adding events");
            sfs.AddEventListener( SFSEvent.CONNECTION, OnSmarfoxConnection);
            sfs.AddEventListener( SFSEvent.CONNECTION_LOST, OnSmarfoxConnectionLost);

            sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.AddEventListener(SFSEvent.LOGIN, OnLogout);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);

            sfs.AddLogListener(logLevel, OnDebugLogMessage);
        }

        public void OnLogin( BaseEvent evt )
        {
            if (onLogin != null )
            {
                onLogin(evt);
                onLogin = null;
            }

            UnityEngine.Debug.Log("LOGIN SUCCESSFUL!!!!!!!!!!!!!!!!");
            UnityEngine.Debug.Log( (string)evt.Params["message"] );
        }

        public void OnLoginError( BaseEvent evt )
        {
            UnityEngine.Debug.LogError("Login failure: " + (string)evt.Params["errorMessage"]);
            if (onLoginError != null)
                onLoginError(evt);
        }


        public void OnLogout(BaseEvent evt)
        {
            if (onLogout != null)
            {
                onLogout(evt);
                onLogout = null;
            }
        }

        public void Connect(
           string host = "game01.mundodositio.globo.com",
           int port = 9933,
           string zone = "Matheus",
           bool debugMode = true,
            OnSmartfoxResponse onSFSConnection = null)
        {

            Sfs2X.Util.ConfigData config = new Sfs2X.Util.ConfigData();
            config.Debug = debugMode;
            config.Host = host;
            config.Port = port;
            config.Zone = zone;

            if (onSFSConnection != null)
                onConnection += onSFSConnection;

            Connect(config);
        }

        public void Update()
        {
            if (sfs != null)
                sfs.ProcessEvents();
        }

        public void Connect( Sfs2X.Util.ConfigData config )
        {
            if( !sfs.IsConnected )
            { 
                sfsConfig = config;
                UnityEngine.Debug.Log("Starting the connection request");
                sfs.Connect(config);
            }
            else
            {
                UnityEngine.Debug.LogWarning("The Smartfox already connected");    
            }
        }

        private void OnDebugLogMessage(BaseEvent evt)
        {
            string message = (string)evt.Params["message"];
            UnityEngine.Debug.Log("[SFS DEBUG] " + message);
        }

        private void OnSmarfoxConnection( BaseEvent evt )
        {
            string statusMessage = "";
            bool success = (bool)evt.Params["success"];

		    if (success) {
                
			    statusMessage = "Connection successful!";
		    } else {
			    statusMessage = "Can't connect to server!";
		    }

            UnityEngine.Debug.Log("Connection status: " + statusMessage);


            if (onConnection != null)
                onConnection(evt);
        }

        public void Login( string userName , string password, string zone = "sitio" )
        {
            Sfs2X.Entities.Data.SFSObject sfsobj = new Sfs2X.Entities.Data.SFSObject();
            sfsobj.PutUtfString("password", password);
            sfs.Send(new Sfs2X.Requests.LoginRequest(userName, password, zone, sfsobj));
        }


        private void OnSmarfoxConnectionLost(BaseEvent evt)
        {
            UnityEngine.Debug.Log("[SFS DEBUG] " + "CONNECTION LOSSSSSTTTTTTT");
        }

    }
}
