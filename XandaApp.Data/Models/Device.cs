using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Enums;

namespace XandaApp.Data.Models
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string RegistrationCode { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public bool Online { get; set; } = false;
        public DateTimeOffset LastOnline { get; set; }
        public bool Registered { get; set; } = false;
        public DateTimeOffset CreatedAt { get; set; }
        [StringLength(255)]
        public string ScreenShotURL { get; set; }
        public bool Enabled { get; set; } = true;
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        #region Network
        [Display(Name = "Enable WiFi")]
        public bool EnableWiFi { get; set; }
        [Display(Name = "WiFi Network Name")]
        [StringLength(30)]
        public string WiFiNetworkName { get; set; }
        [Display(Name = "WiFi Mode")]
        public WiFiMode WiFiMode { get; set; }
        [Display(Name = "WiFi Key")]
        [StringLength(30)]
        public string WiFiKey { get; set; }
        [Display(Name = "Enterprise User Name")]
        [StringLength(30)]
        public string WiFiUsername { get; set; }
        [Display(Name = "Enterprise WiFi Password")]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string WiFiPassword { get; set; }
        [Display(Name = "WiFi network is hidden")]
        public bool NetworkHidden { get; set; }
        [StringLength(9)]
        public string Checksum { get; set; }
        public bool UseDongle { get; set; }
        [StringLength(4)]
        public string SIMPin { get; set; }
        [StringLength(20)]
        public string APN { get; set; }
        [StringLength(20)]
        public string DongleUsername { get; set; }
        [StringLength(20)]
        public string DonglePassword { get; set; }
        [StringLength(255)]
        public string AdvancedConfig { get; set; }
        #region Advance Networking
        [Display(Name = "Enable Proxy")]
        public bool EnableProxy { get; set; }
        [Display(Name = "WiFi Static Configuration")]
        public bool WiFiStaticConfig { get; set; }
        [Display(Name = "IP Address")]
        [StringLength(15)]
        public string WiFiIPAddress { get; set; }
        [Display(Name = "Netmask")]
        [StringLength(15)]
        public string WiFiNetmask { get; set; }
        [Display(Name = "Gateway")]
        [StringLength(15)]
        public string WiFiGateway { get; set; }
        [Display(Name = "DNS")]
        [StringLength(15)]
        public string WiFiDNS { get; set; }
        [Display(Name = "Secondary DNS")]
        [StringLength(15)]
        public string WiFiSecondaryDNS { get; set; }
        [Display(Name = "LAN Static Configuration")]
        public bool LANStaticConfig { get; set; }
        [Display(Name = "IP Address")]
        [StringLength(15)]
        public string LANIPAddress { get; set; }
        [Display(Name = "Netmask")]
        [StringLength(15)]
        public string LANNetmask { get; set; }
        [Display(Name = "Gateway")]
        [StringLength(15)]
        public string LANGateway { get; set; }
        [Display(Name = "DNS")]
        [StringLength(15)]
        public string LANDNS { get; set; }
        [Display(Name = "Secondary DNS")]
        [StringLength(15)]
        public string LANSecondaryDNS { get; set; }
        [Display(Name = "Use Google DNS as primary")]
        public bool GoogleDNS { get; set; }
        [Display(Name = "NTP Servers (comma separated)")]
        [StringLength(50)]
        public string NTPServers { get; set; }
        #endregion
        #endregion
    }
}
