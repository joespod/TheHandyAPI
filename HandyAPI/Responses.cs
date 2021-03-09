using System;
using System.Collections.Generic;
using System.Text;

namespace HandyAPI
{
    public class ResponseServerTime
    {
        public long serverTime;
    }

    public class ResponseBase
    {
        public bool success;
        public bool connected;
    }

    public class ResponseCmd : ResponseBase
    {
        public string cmd;
    }

    public class ResponseVersion : ResponseBase
    {
        public string version;      //-- current version of the handy being queried
        public string latest;       //-- latest available firmware
    }

    public class ResponseSettings : ResponseCmd
    {
        public int mode;
        public int speed;
        public float position;
        public float stroke;
    }

    public class ResponseStatus : ResponseBase
    {
        public int mode;
        public int speed;
        public float position;
        public int setSpeedPercent;
        public int setStrokePercent;
        public long serverTime;
    }

    public class ResponseSetMode : ResponseCmd
    {
        public int mode;
    }

    public class ResponseSetSpeed: ResponseCmd
    {
        public int currentPosition;
        public int speed;
        public float speedPercent;
    }

    public class ResponseSetStroke : ResponseCmd
    {
        public float strokePercent;
        public int stroke;
        public int currentPosition;
    }

    public class ResponseStepSpeed : ResponseCmd
    {
        public float speedPercent;
        public int speed;
    }

    public class ResponseStepStroke : ResponseCmd
    {
        public float strokePercent;
        public int stroke;
    }

    public class ResponseSyncPrepare : ResponseBase
    {
        public bool downloaded;
    }

    public class ResponseSyncPlay : ResponseCmd
    {
        public int setOffset;
        public long serverTimeDelta;
        public bool playing;
    }

    public class ResponseSyncOffset: ResponseCmd
    {
        public long offset;
    }
}
