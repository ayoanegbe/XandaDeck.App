using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Enums;

namespace XandaApp.Data.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public SubscriptionType Subscription { get; set; } = SubscriptionType.Standard;
        public int DeviceQuota { get; set; }
        public int Devices { get; set; }
        public int RegisteredDevices { get; set; }
        public int OnlineDevices { get; set; }
        public bool DeviceOfflineNotice { get; set; } = true;
        public bool DeviceOnlineNotice { get; set; } = false;
        public bool NewLocationLoginNotice { get; set; } = false;
        public bool Use2FA { get; set; } = false;

    }
}
