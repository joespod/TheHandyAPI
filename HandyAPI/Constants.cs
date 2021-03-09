using System;
using System.Collections.Generic;
using System.Text;

namespace HandyAPI
{
    public partial class TheHandy
    {
        public enum Mode
        {
            off = 0,        //-- stops the handy
            automatic = 1,  //-- "start"
            position = 2,   //-- ???
            calibration = 3,    //-- reserved for internal use
            sync = 4        //-- set the handy synchronizing to a video source
        }

        private const long DEFAULT_TIMEOUT = 5000;
        
        public const string ENDPOINT_DEFAULT = "https://www.handyfeeling.com/api/v1";
        public const string ENDPOINT_STAGING = "https://staging.handyfeeling.com/api/v1";
        public const string ENDPOINT_VIRTUAL = "https://virtserver.swaggerhub.com/alexandera/handy-api/v1";

        public const string TEST_CONNECTION_KEY = "mDU0dPC";

        private const string CMD_GETSERVERTIME = "getServerTime";
        private const string CMD_GETSETTINGS = "getSettings";
        private const string CMD_GETSTATUS = "getStatus";
        private const string CMD_GETVERSION = "getVersion";
        private const string CMD_SETMODE = "setMode";
        private const string CMD_SETSPEED = "setSpeed";
        private const string CMD_SETSTROKE = "setStroke";
        private const string CMD_STEPSPEED = "stepSpeed";
        private const string CMD_STEPSTROKE = "stepStroke";
        private const string CMD_SYNCOFFSET = "syncOffset";
        private const string CMD_SYNCPLAY = "syncPlay";
        private const string CMD_SYNCPREPARE = "syncPrepare";
        private const string CMD_TOGGLEMODE = "toggleMode";


        private const string PARAM_MODE = "mode";
        private const string PARAM_NAME = "name";
        private const string PARAM_OFFSET = "offset";
        private const string PARAM_PLAY = "play";
        private const string PARAM_SERVERTIME = "serverTime";
        private const string PARAM_SIZE = "size";
        private const string PARAM_SPEED = "speed";
        private const string PARAM_STEP = "step";
        private const string PARAM_STROKE = "stroke";
        private const string PARAM_TIME = "time";
        private const string PARAM_TIMEOUT = "timeout";
        private const string PARAM_TYPE = "type";
        private const string PARAM_URL = "url";        
        
        private const string PARAMTYPE_MM = "mm";
        private const string PARAMTYPE_MMS = "mm/s";
        private const string PARAMTYPE_PERCENT = "%";
    }
}
