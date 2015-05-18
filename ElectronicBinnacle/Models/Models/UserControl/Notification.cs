using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicBinnacle.Models.Models.UserControl
{
    public enum NotificationType
    {
        NonState,
        GpsState,
        Sms,
        Mms,
        Call,
        OffOn,
        SamplerIsyUpdate,
        LowBattery,
        SamplerIsyStop,
        InstalledPackage, 
        SoundProfile,
        SendedRecievedOrder,
    }

    public class Notification
    {
        #region Properties

        public int NotificationId { get; set; }
        public string NOTIFICATION_MSG { get; set; }
        public NotificationType NOTIFICATION_TYPE { get; set; }
        public double LONGITUDE { get; set; }
        public double LATITUDE { get; set; }
        public float ACCURACY { get; set; }
        public float SPEED { get; set; }
        public int NOTIFICATION_CATEGORY { get; set; }
        public long DATETIME { get; set; }
        public bool Old { get; set; }

        public string SamplerName { get; set; }

        #endregion
        #region Methods


        public Notification Clone()
        {
            return new  Notification
                         {
                             NotificationId = this.NotificationId,
                             NOTIFICATION_MSG = this.NOTIFICATION_MSG,
                             NOTIFICATION_TYPE = this.NOTIFICATION_TYPE,
                             LONGITUDE = this.LONGITUDE,
                             LATITUDE = this.LATITUDE,
                             ACCURACY = this.ACCURACY,
                             SPEED = this.SPEED,
                             NOTIFICATION_CATEGORY = this.NOTIFICATION_CATEGORY,
                             DATETIME = this.DATETIME,
                             Old = this.Old,
                             SamplerName = this.SamplerName
                         };
        }

        #endregion
    }
}