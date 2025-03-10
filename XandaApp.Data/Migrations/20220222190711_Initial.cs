﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XandaApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    Subscription = table.Column<int>(nullable: false),
                    DeviceQuota = table.Column<int>(nullable: false),
                    Devices = table.Column<int>(nullable: false),
                    RegisteredDevices = table.Column<int>(nullable: false),
                    OnlineDevices = table.Column<int>(nullable: false),
                    DeviceOfflineNotice = table.Column<bool>(nullable: false),
                    DeviceOnlineNotice = table.Column<bool>(nullable: false),
                    NewLocationLoginNotice = table.Column<bool>(nullable: false),
                    Use2FA = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccounId = table.Column<int>(nullable: true),
                    DeviceLogin = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTimeOffset>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    HostIP = table.Column<string>(nullable: true),
                    PortNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    CcEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpConfig",
                columns: table => new
                {
                    SmtpSettingId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    HostIP = table.Column<string>(nullable: true),
                    PortNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpConfig", x => x.SmtpSettingId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Revoked = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegistrationCode = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Online = table.Column<bool>(nullable: false),
                    LastOnline = table.Column<DateTimeOffset>(nullable: false),
                    Registered = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ScreenShotURL = table.Column<string>(maxLength: 255, nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false),
                    EnableWiFi = table.Column<bool>(nullable: false),
                    WiFiNetworkName = table.Column<string>(maxLength: 30, nullable: true),
                    WiFiMode = table.Column<int>(nullable: false),
                    WiFiKey = table.Column<string>(maxLength: 30, nullable: true),
                    WiFiUsername = table.Column<string>(maxLength: 30, nullable: true),
                    WiFiPassword = table.Column<string>(maxLength: 20, nullable: true),
                    NetworkHidden = table.Column<bool>(nullable: false),
                    Checksum = table.Column<string>(maxLength: 9, nullable: true),
                    UseDongle = table.Column<bool>(nullable: false),
                    SIMPin = table.Column<string>(maxLength: 4, nullable: true),
                    APN = table.Column<string>(maxLength: 20, nullable: true),
                    DongleUsername = table.Column<string>(maxLength: 20, nullable: true),
                    DonglePassword = table.Column<string>(maxLength: 20, nullable: true),
                    AdvancedConfig = table.Column<string>(maxLength: 255, nullable: true),
                    EnableProxy = table.Column<bool>(nullable: false),
                    WiFiStaticConfig = table.Column<bool>(nullable: false),
                    WiFiIPAddress = table.Column<string>(maxLength: 15, nullable: true),
                    WiFiNetmask = table.Column<string>(maxLength: 15, nullable: true),
                    WiFiGateway = table.Column<string>(maxLength: 15, nullable: true),
                    WiFiDNS = table.Column<string>(maxLength: 15, nullable: true),
                    WiFiSecondaryDNS = table.Column<string>(maxLength: 15, nullable: true),
                    LANStaticConfig = table.Column<bool>(nullable: false),
                    LANIPAddress = table.Column<string>(maxLength: 15, nullable: true),
                    LANNetmask = table.Column<string>(maxLength: 15, nullable: true),
                    LANGateway = table.Column<string>(maxLength: 15, nullable: true),
                    LANDNS = table.Column<string>(maxLength: 15, nullable: true),
                    LANSecondaryDNS = table.Column<string>(maxLength: 15, nullable: true),
                    GoogleDNS = table.Column<bool>(nullable: false),
                    NTPServers = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleItem",
                columns: table => new
                {
                    ScheduleItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<int>(nullable: false),
                    Content = table.Column<int>(nullable: false),
                    FirstStart = table.Column<DateTimeOffset>(nullable: false),
                    FirstStop = table.Column<DateTimeOffset>(nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    Monday = table.Column<bool>(nullable: true),
                    Tuesday = table.Column<bool>(nullable: true),
                    Wednesday = table.Column<bool>(nullable: true),
                    Thursday = table.Column<bool>(nullable: true),
                    Friday = table.Column<bool>(nullable: true),
                    Saturday = table.Column<bool>(nullable: true),
                    Sunday = table.Column<bool>(nullable: true),
                    RepeatUnitl = table.Column<DateTimeOffset>(nullable: true),
                    Forever = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleItem", x => x.ScheduleItemId);
                    table.ForeignKey(
                        name: "FK_ScheduleItem_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_AccountId",
                table: "Device",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_ScheduleId",
                table: "Device",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_AccountId",
                table: "Schedule",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItem_ScheduleId",
                table: "ScheduleItem",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "ScheduleItem");

            migrationBuilder.DropTable(
                name: "SmtpConfig");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
